using DevBlog.DTOs;
using DevBlog.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Polly.Retry;
using Polly;
using DevBlog.ServicesContract;
using DevBlog.Mappers;

namespace DevBlog.Services
{
    public class PostService : IPostService
    {
        private readonly BlogDbContext _db;
        private readonly ResiliencePipeline _resiliencePipeline;
        public PostService(BlogDbContext db)
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
        public async Task CreatePost(CreatePostRequest post)
        {
            if (post == null)
            {
                throw new ArgumentNullException(nameof(post), "El post no puede ser nulo.");
            }

            try
            {
                await _resiliencePipeline.ExecuteAsync(async token =>
                {
                    Category category = await _db.Category.FirstOrDefaultAsync(c => c.Id == post.CategoryId, token);

                    if (category == null)
                    {
                        throw new ArgumentNullException(nameof(category), "La categoría no existe.");
                    }

                    List<Tag> tags = await _db.Tag.Where(t => post.Tags.Contains(t.Id)).ToListAsync(token);

                    if (tags.Count != post.Tags.Count)
                    {
                        throw new ArgumentNullException(nameof(tags), "Alguna etiqueta no existe.");
                    }

                    Post postEntity = new Post
                    {
                        Title = post.Title,
                        Content = post.Content,
                        ImageUrl = post.ImageUrl,
                        CreatedAt = DateTime.Now,
                        AuthorId = post.AuthorId,
                        CategoryId = post.CategoryId,
                        Tags = tags
                    };

                    await _db.Post.AddAsync(postEntity, token);
                    await _db.SaveChangesAsync(token);
                });
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error inesperado al agregar el post.");
            }

        }

        public Task<Post> DeletePost(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<PostDTO>> GetAllPosts()
        {

            try
            {
                return await _resiliencePipeline.ExecuteAsync(async token =>
                {
                    List<Post> posts = await _db.Post
                        .Include(p => p.Author)
                        .Include(p => p.Category)
                        .Include(p => p.Tags)
                        .Include(p => p.Comments)
                        .ToListAsync(token);

                    return posts.Select(p => PostMapper.ToDTO(p)).ToList();
                });
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error inesperado al obtener los posts.");
            }
        }

        public Task<Post> GetPostById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Post>> GetPostsByAuthor(Guid authorId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Post>> GetPostsByCategory(Guid categoryId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Post>> GetPostsByTag(Guid tagId)
        {
            throw new NotImplementedException();
        }

        public Task<Post> UpdatePost(Post post)
        {
            throw new NotImplementedException();
        }

    }
}
