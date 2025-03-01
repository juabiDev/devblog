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
                    var category = await _db.Category
                        .Where(c => c.Id == post.CategoryId)
                        .AsSplitQuery()
                        .FirstOrDefaultAsync(token);

                    if (category == null)
                    {
                        throw new ArgumentException("La categoría no existe.");
                    }

                    List<Tag> tags = await _db.Tag.Where(t => post.Tags.Contains(t.Id)).ToListAsync(token);

                    if (tags.Count != post.Tags.Count)
                    {
                        throw new ArgumentException("Alguna etiqueta no existe.");
                    }

                    var user = await _db.User
                        .Where(u => u.Id == post.AuthorId)
                        .AsSplitQuery()
                        .FirstOrDefaultAsync(token);

                    if (user == null)
                    {
                        throw new ArgumentException("El autor no existe.");
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
            catch(ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error inesperado al agregar el post.");
            }

        }

        public Task DeletePost(Guid id)
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

        public async Task<Post> GetPostById(Guid id)
        {
            try
            {
                return await _resiliencePipeline.ExecuteAsync(async token =>
                {
                    var post = await _db.Post
                        .Include(p => p.Author)
                        .Include(p => p.Category)
                        .Include(p => p.Tags)
                        .Include(p => p.Comments)
                        .FirstOrDefaultAsync(p => p.Id == id, token);

                    if (post == null)
                    {
                        throw new ArgumentException("El post no existe.");
                    }

                    return post;
                });
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error inesperado al obtener el post.");
            }
        }

        public async Task<List<PostDTO>> GetPostsByAuthor(Guid authorId)
        {
            if (!await _db.User.AnyAsync(u => u.Id == authorId))
            {
                throw new ArgumentException("La categoría no existe.");
            }

            try
            {
                return await _resiliencePipeline.ExecuteAsync(async token =>
                {
                    var posts = await _db.Post
                        .Include(p => p.Author)
                        .Include(p => p.Category)
                        .Include(p => p.Tags)
                        .Include(p => p.Comments)
                        .Where(p => p.AuthorId == authorId)
                        .ToListAsync(token);

                    return posts.Select(p => PostMapper.ToDTO(p)).ToList();
                });
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error inesperado al obtener los posts.");
            }
        }
        public async Task<List<PostDTO>> GetPostsByCategory(Guid categoryId)
        {

            if (!await _db.Category.AnyAsync(c => c.Id == categoryId))
            {
                throw new ArgumentException("La categoría no existe.");
            }

            try
            {
                return await _resiliencePipeline.ExecuteAsync(async token =>
                {
                    var posts = await _db.Post
                        .Include(p => p.Author)
                        .Include(p => p.Category)
                        .Include(p => p.Tags)
                        .Include(p => p.Comments)
                        .Where(p => p.CategoryId == categoryId)
                        .ToListAsync(token);

                    return posts.Select(p => PostMapper.ToDTO(p)).ToList();
                });
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error inesperado al obtener los posts.");
            }
        }
        public async Task<List<PostDTO>> GetPostsByTag(Guid tagId)
        {
            if (!await _db.Tag.AnyAsync(t => t.Id == tagId))
            {
                throw new ArgumentException("La etiqueta no existe.");
            }

            try
            {
                return await _resiliencePipeline.ExecuteAsync(async token =>
                {
                    var posts = await _db.Post
                        .Include(p => p.Author)
                        .Include(p => p.Category)
                        .Include(p => p.Tags)
                        .Include(p => p.Comments)
                        .Where(p => p.Tags.Any(t => t.Id == tagId))
                        .ToListAsync(token);

                    return posts.Select(p => PostMapper.ToDTO(p)).ToList();
                });
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error inesperado al obtener los posts.");
            }
        }

        public async Task<Post> UpdatePost(Post post)
        {
            throw new NotImplementedException();
        }
    }
}
