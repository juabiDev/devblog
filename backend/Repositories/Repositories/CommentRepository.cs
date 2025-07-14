using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Polly;
using RepositoriesContracts.RepositoriesContracts;
using ServicesContracts.DTOs;

namespace Repositories.Repositories;

public class CommentRepository : ICommentRepository
{
	private readonly BlogDbContext _db;

	private readonly ResiliencePipeline _resiliencePipeline;

	public CommentRepository(BlogDbContext dbContext, ResiliencePipeline resiliencePipeline)
	{
		_db = dbContext;
		_resiliencePipeline = resiliencePipeline;
	}

	public async Task AddComment(CreateCommentRequest comment)
	{
		try
		{
			await _resiliencePipeline.ExecuteAsync(async delegate(CancellationToken token)
			{
				User user = await _db.User.FirstOrDefaultAsync((Expression<Func<User, bool>>)((e) => e.Email == comment.UserEmail), token);
				if (user == null)
				{
					throw new ArgumentException("User not found");
				}
				Post post = await _db.Post.Include((Expression<Func<Post, ICollection<Comment>>>)((p) => p.Comments)).AsSplitQuery().FirstOrDefaultAsync((Expression<Func<Post, bool>>)((e) => e.Id == comment.PostId), token);
				if (post == null)
				{
					throw new ArgumentException("Post not found");
				}
				Comment newComment = new Comment
				{
					Author = user,
					Post = post,
					Text = comment.Content,
					CreatedAt = DateTime.UtcNow
				};
				await _db.Comment.AddAsync(newComment, token);
				await _db.SaveChangesAsync(token);
			}, default);
		}
		catch (DbUpdateException)
		{
			throw new Exception("Error al agregar el comentario en la base de datos.");
		}
		catch (SqlException)
		{
			throw new Exception("Error al conectar la base de datos");
		}
		catch (ArgumentException ex3)
		{
			ArgumentException ex4 = ex3;
			throw new ArgumentException(ex4.Message);
		}
		catch (Exception)
		{
			throw new Exception("Ocurrió un error inesperado al agregar el comentario.");
		}
	}

	public async Task DeleteComment(Guid id, string userEmail)
	{
		try
		{
			await _resiliencePipeline.ExecuteAsync(async delegate(CancellationToken token)
			{
				Comment comment = await _db.Comment.Include((Expression<Func<Comment, User>>)((c) => c.Author)).AsSplitQuery().FirstOrDefaultAsync((Expression<Func<Comment, bool>>)((e) => e.Id == id), token);
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
			}, default);
		}
		catch (DbUpdateException)
		{
			throw new Exception("Error al eliminar el comentario en la base de datos.");
		}
		catch (SqlException)
		{
			throw new Exception("Error al conectar la base de datos");
		}
		catch (ArgumentException ex3)
		{
			ArgumentException ex4 = ex3;
			throw new ArgumentException(ex4.Message);
		}
		catch (Exception)
		{
			throw new Exception("Ocurrió un error inesperado al eliminar el comentario.");
		}
	}

	public async Task<Comment> GetCommentAsync(Guid id)
	{
		try
		{
			return await _resiliencePipeline.ExecuteAsync(async (token) => await _db.Comment.Include((Expression<Func<Comment, User>>)((c) => c.Author)).AsSplitQuery().FirstOrDefaultAsync((Expression<Func<Comment, bool>>)((e) => e.Id == id), token), default);
		}
		catch (DbUpdateException)
		{
			throw new Exception("Error al obtener el comentario en la base de datos.");
		}
		catch (SqlException)
		{
			throw new Exception("Error al conectar la base de datos");
		}
		catch (ArgumentException ex3)
		{
			ArgumentException ex4 = ex3;
			throw new ArgumentException(ex4.Message);
		}
		catch (Exception)
		{
			throw new Exception("Ocurrió un error inesperado al obtener el comentario.");
		}
	}

	public async Task<Comment> GetCommentById(Guid id, Guid userId)
	{
		try
		{
			return await _resiliencePipeline.ExecuteAsync(async (token) => await _db.Comment.Include((Expression<Func<Comment, User>>)((c) => c.Author)).AsSplitQuery().FirstOrDefaultAsync((Expression<Func<Comment, bool>>)((e) => e.Id == id), token), default);
		}
		catch (DbUpdateException)
		{
			throw new Exception("Error al obtener el comentario en la base de datos.");
		}
		catch (SqlException)
		{
			throw new Exception("Error al conectar la base de datos");
		}
		catch (ArgumentException ex3)
		{
			ArgumentException ex4 = ex3;
			throw new ArgumentException(ex4.Message);
		}
		catch (Exception)
		{
			throw new Exception("Ocurrió un error inesperado al obtener el comentario.");
		}
	}

	public async Task<IEnumerable<Comment>> GetCommentsAsync(Guid postId)
	{
		try
		{
			return await _resiliencePipeline.ExecuteAsync(async (token) => await _db.Comment.Include((Expression<Func<Comment, User>>)((c) => c.Author)).Where((c) => c.PostId == postId).AsSplitQuery().ToListAsync(token), default);
		}
		catch (DbUpdateException)
		{
			throw new Exception("Error al obtener los comentarios en la base de datos.");
		}
		catch (SqlException)
		{
			throw new Exception("Error al conectar la base de datos");
		}
		catch (ArgumentException ex3)
		{
			ArgumentException ex4 = ex3;
			throw new ArgumentException(ex4.Message);
		}
		catch (Exception)
		{
			throw new Exception("Ocurrió un error inesperado al obtener los comentarios.");
		}
	}

	public async Task UpdateCommentAsync(Guid id, CreateCommentRequest comment)
	{
		try
		{
			await _resiliencePipeline.ExecuteAsync(async delegate(CancellationToken token)
			{
				if (await _db.User.FirstOrDefaultAsync((Expression<Func<User, bool>>)((e) => e.Email == comment.UserEmail), token) == null)
				{
					throw new ArgumentException("User not found");
				}
				Comment commentEntity = await _db.Comment.Include((Expression<Func<Comment, User>>)((c) => c.Author)).AsSplitQuery().IgnoreQueryFilters().FirstOrDefaultAsync((Expression<Func<Comment, bool>>)((e) => e.Id == id), token);
				if (commentEntity == null)
				{
					throw new ArgumentException("Comment not found");
				}
				if (commentEntity.Author.Email != comment.UserEmail)
				{
					throw new ArgumentException("User not authorized to update this comment");
				}
				commentEntity.Text = comment.Content;
				commentEntity.UpdatedAt = DateTime.UtcNow;
				_db.Comment.Update(commentEntity);
				await _db.SaveChangesAsync(token);
			}, default);
		}
		catch (DbUpdateException)
		{
			throw new Exception("Error al actualizar el comentario en la base de datos.");
		}
		catch (SqlException)
		{
			throw new Exception("Error al conectar la base de datos");
		}
		catch (ArgumentException ex3)
		{
			ArgumentException ex4 = ex3;
			throw new ArgumentException(ex4.Message);
		}
		catch (Exception)
		{
			throw new Exception("Ocurrió un error inesperado al actualizar el comentario.");
		}
	}
}
