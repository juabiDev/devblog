using DevBlog.DTOs;
using DevBlog.Entities;
using DevBlog.ServicesContract;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Polly.Retry;
using Polly;

namespace DevBlog.Services
{
    public class CommentService : ICommentService
    {
        private readonly BlogDbContext _db;
        private readonly ResiliencePipeline _resiliencePipeline;
        public CommentService(BlogDbContext Db)
        {
            _db = Db;

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
        public async Task AddCommentAsync(CreateCommentRequest comment)
        {
            try
            {
                await _resiliencePipeline.ExecuteAsync(async token =>
                {
                    var user = await _db.User
                        .FirstOrDefaultAsync(e => e.Email == comment.UserEmail, token);
                    
                    if (user == null)
                    {
                        throw new ArgumentException("User not found");
                    }

                    var post = await _db.Post
                        .Include(p => p.Comments)
                        .AsSplitQuery()
                        .FirstOrDefaultAsync(e => e.Id == comment.PostId, token);
                    
                    if (post == null)
                    {
                        throw new ArgumentException("Post not found");
                    }
                    
                    var newComment = new Comment
                    {
                        Author = user,
                        Post = post,
                        Content = comment.Content,
                        CreatedAt = DateTime.UtcNow
                    };
                    
                    await _db.Comment.AddAsync(newComment, token);
                    await _db.SaveChangesAsync(token);
                });
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Error al agregar el comentario en la base de datos.");
            }
            catch (SqlException ex)
            {
                throw new Exception("Error al conectar la base de datos");
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error inesperado al agregar el comentario.");
            }
        }

        public async Task DeleteCommentAsync(Guid id, string userEmail)
        {
            try
            {
                await _resiliencePipeline.ExecuteAsync(async token =>
                {
                    var comment = await _db.Comment
                        .Include(c => c.Author)
                        .AsSplitQuery()
                        .IgnoreQueryFilters()
                        .FirstOrDefaultAsync(e => e.Id == id, token);

                    if (comment == null)
                    {
                        throw new ArgumentException("Comment not found");
                    }

                    if (comment.Author.Email != userEmail)
                    {
                        throw new ArgumentException("User not authorized to delete this comment");
                    }

                    comment.DeletedAt = DateTime.UtcNow;
                    _db.Comment.Update(comment);
                    await _db.SaveChangesAsync(token);
                });
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Error al eliminar el comentario en la base de datos.");
            }
            catch (SqlException ex)
            {
                throw new Exception("Error al conectar la base de datos");
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error inesperado al eliminar el comentario.");
            }
        }

        public async Task<CommentDTO> GetCommentAsync(Guid id)
        {
            try
            {
                return await _resiliencePipeline.ExecuteAsync(async token =>
                {
                    var comment = await _db.Comment
                        .Include(c => c.Author)
                        .AsSplitQuery()
                        .IgnoreQueryFilters()
                        .FirstOrDefaultAsync(e => e.Id == id, token);

                    if (comment == null)
                    {
                        throw new ArgumentException("Comment not found");
                    }

                    return Mappers.CommentMapper.ToDTO(comment);
                });
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error inesperado al obtener el comentario.");
            }
        }

        public async Task<CommentDTO> GetCommentAsync(Guid id, Guid userId)
        {
            try
            {
                return await _resiliencePipeline.ExecuteAsync(async token =>
                {
                    var comment = await _db.Comment
                        .Include(c => c.Author)
                        .AsSplitQuery()
                        .IgnoreQueryFilters()
                        .FirstOrDefaultAsync(e => e.Id == id, token);

                    if (comment == null)
                    {
                        throw new ArgumentException("Comment not found");
                    }

                    return Mappers.CommentMapper.ToDTO(comment);
                });
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error inesperado al obtener el comentario.");
            }
        }

        public async Task<IEnumerable<CommentDTO>> GetCommentsAsync(Guid postId)
        {
            try
            {
                return await _resiliencePipeline.ExecuteAsync(async token =>
                {
                    var comments = await _db.Comment
                        .Include(c => c.Author)
                        .Where(c => c.PostId == postId)
                        .AsSplitQuery()
                        .IgnoreQueryFilters()
                        .ToListAsync(token);

                    return comments.Select(c => Mappers.CommentMapper.ToDTO(c));
                });
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error inesperado al obtener los comentarios.");
            }
        }

        public async Task UpdateCommentAsync(Guid id, CreateCommentRequest comment)
        {
            try
            {
                await _resiliencePipeline.ExecuteAsync(async token =>
                {
                    var user = await _db.User
                        .FirstOrDefaultAsync(e => e.Email == comment.UserEmail, token);

                    if (user == null)
                    {
                        throw new ArgumentException("User not found");
                    }

                    var commentEntity = await _db.Comment
                        .Include(c => c.Author)
                        .AsSplitQuery()
                        .IgnoreQueryFilters()
                        .FirstOrDefaultAsync(e => e.Id == id, token);

                    if (commentEntity == null)
                    {
                        throw new ArgumentException("Comment not found");
                    }

                    if (commentEntity.Author.Email != comment.UserEmail)
                    {
                        throw new ArgumentException("User not authorized to update this comment");
                    }

                    commentEntity.Content = comment.Content;
                    commentEntity.UpdatedAt = DateTime.UtcNow;
                    _db.Comment.Update(commentEntity);
                    await _db.SaveChangesAsync(token);
                });
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Error al actualizar el comentario en la base de datos.");
            }
            catch (SqlException ex)
            {
                throw new Exception("Error al conectar la base de datos");
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error inesperado al actualizar el comentario.");
            }
        }
    }
}
