using DevBlog.DTOs;
using DevBlog.ServicesContract;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Polly.Retry;
using Polly;
using DevBlog.Entities;
using DevBlog.Mappers;

namespace DevBlog.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly BlogDbContext _db;
        private readonly ResiliencePipeline _resiliencePipeline;
        public CategoryService(BlogDbContext db)
        {
            _db = db;

            // Retry policy for database operations
            _resiliencePipeline = new ResiliencePipelineBuilder()
                .AddRetry(new RetryStrategyOptions
                {
                    ShouldHandle = new PredicateBuilder()
                        .Handle<DbUpdateException>()  // Handle DbUpdateException
                        .Handle<SqlException>(), // Handle SqlException
                    MaxRetryAttempts = 3, // Retry 3 times
                    Delay = TimeSpan.FromSeconds(2), // Delay 2 seconds between retries
                    OnRetry = retryArgs =>
                    {
                        return default;
                    }
                })
                .Build();
        }
        public async Task<IEnumerable<CategoryDTO>> GetCategoriesAsync()
        {
            try
            {
                return await _resiliencePipeline.ExecuteAsync(async token =>
                {
                    var categories = await _db.Category
                        .AsSplitQuery()
                        .ToListAsync(token);

                    return categories.Select(c => CategoryMapper.ToDTO(c)).ToList();
                });
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error inesperado al obtener las categorías.");
            }
        }
        public async Task<CategoryDTO> GetCategoryAsync(Guid id)
        {
            try
            {
                return await _resiliencePipeline.ExecuteAsync(async token =>
                {
                    var category = await _db.Category
                        .AsSplitQuery()
                        .FirstOrDefaultAsync(c => c.Id == id, token);

                    if (category == null)
                    {
                        throw new ArgumentException("La categoría no existe.");
                    }

                    return CategoryMapper.ToDTO(category);
                });
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error inesperado al obtener la categoría.");
            }
        }
    }
}
