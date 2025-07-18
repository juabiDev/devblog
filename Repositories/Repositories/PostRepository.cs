using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Entities.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Polly;
using Polly.Retry;
using Repositories.DBContext;
using RepositoriesContracts.RepositoriesContracts;
using ServicesContracts.DTOs;

namespace Repositories.Repositories;

public class PostRepository : IPostRepository
{
	private readonly BlogDbContext _db;

	private readonly ResiliencePipeline _resiliencePipeline;

	public PostRepository(BlogDbContext db, ResiliencePipeline resiliencePipeline)
	{
		_resiliencePipeline = resiliencePipeline;
		_db = db;
	}

	public async Task AddPost(CreatePostRequest post)
	{
		try
		{
			if (post == null)
			{
				throw new ArgumentNullException("post", "El post no puede ser nulo.");
			}
			await _resiliencePipeline.ExecuteAsync(async delegate(CancellationToken token)
			{
				if (await _db.Category.Where((c) => c.Id == post.CategoryId).AsSplitQuery().FirstOrDefaultAsync(token) == null)
				{
					throw new ArgumentException("La categoría no existe.");
				}
				List<Tag> tags = await _db.Tag.Where((t) => post.Tags.Contains(t.Id)).ToListAsync(token);
				if (tags.Count != post.Tags.Count)
				{
					throw new ArgumentException("Alguna etiqueta no existe.");
				}
				if (await _db.User.Where((u) => u.Id == post.AuthorId).AsSplitQuery().FirstOrDefaultAsync(token) == null)
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
			}, default);
		}
		catch (DbUpdateException ex)
		{
			DbUpdateException ex2 = ex;
			DbUpdateException ex3 = ex2;
			throw new DbUpdateException("Ocurrió un error al guardar el post en la base de datos.", (Exception)(object)ex3);
		}
		catch (ArgumentException ex4)
		{
			ArgumentException ex5 = ex4;
			throw new ArgumentException(ex5.Message);
		}
		catch (Exception)
		{
			throw new Exception("Ocurrió un error inesperado al agregar el post.");
		}
	}

	public async Task DeletePost(Guid id, string userEmail)
	{
		try
		{
			await _resiliencePipeline.ExecuteAsync(async delegate(CancellationToken token)
			{
				Post post = await _db.Post.Where((p) => p.Id == id).Include((Expression<Func<Post, User>>)((p) => p.Author)).AsSplitQuery().FirstOrDefaultAsync(token);
				if (post == null)
				{
					throw new ArgumentException("El post no existe.");
				}
				if (post.Author.Email != userEmail)
				{
					throw new ArgumentException("No tienes permiso para eliminar este post.");
				}
				post.DeletedAt = DateTime.Now;
				_db.Post.Update(post);
				await _db.SaveChangesAsync(token);
			}, default);
		}
		catch (DbUpdateException ex)
		{
			DbUpdateException ex2 = ex;
			DbUpdateException ex3 = ex2;
			throw new DbUpdateException("Ocurrió un error al eliminar el post en la base de datos.", (Exception)(object)ex3);
		}
		catch (ArgumentException ex4)
		{
			ArgumentException ex5 = ex4;
			throw new ArgumentException(ex5.Message);
		}
		catch (Exception)
		{
			throw new Exception("Ocurrió un error inesperado al eliminar el post.");
		}
	}

	public async Task<IEnumerable<Post>> GetAllPosts()
	{
		try
		{
			return await _resiliencePipeline.ExecuteAsync(async (token) => (await _db.Post.Include((Expression<Func<Post, User>>)((p) => p.Author)).Include((Expression<Func<Post, Category>>)((p) => p.Category)).Include((Expression<Func<Post, ICollection<Tag>>>)((p) => p.Tags)).Include((Expression<Func<Post, ICollection<Comment>>>)((p) => p.Comments)).ToListAsync(token)).ToList(), default);
		}
		catch (Exception)
		{
			throw new Exception("Ocurrió un error inesperado al obtener los posts.");
		}
	}

	public async Task<Post> GetPostById(Guid id)
	{
		try
		{
			return await _resiliencePipeline.ExecuteAsync(async (token) => await _db.Post.Include((Expression<Func<Post, User>>)((p) => p.Author)).Include((Expression<Func<Post, Category>>)((p) => p.Category)).Include((Expression<Func<Post, ICollection<Tag>>>)((p) => p.Tags)).Include((Expression<Func<Post, ICollection<Comment>>>)((p) => p.Comments)).FirstOrDefaultAsync((Expression<Func<Post, bool>>)((p) => p.Id == id), token), default);
		}
		catch (ArgumentException ex)
		{
			ArgumentException ex2 = ex;
			throw new ArgumentException(ex2.Message);
		}
		catch (Exception)
		{
			throw new Exception("Ocurrió un error inesperado al obtener el post.");
		}
	}

	public async Task<IEnumerable<Post>> GetPostsByAuthorId(Guid authorId)
	{
		try
		{
			return await _resiliencePipeline.ExecuteAsync(async delegate(CancellationToken token)
			{
				if (!await _db.User.AnyAsync((Expression<Func<User, bool>>)((u) => u.Id == authorId), default))
				{
					throw new ArgumentException("El autor no existe.");
				}
				return await _db.User.Where((u) => u.Id == authorId).SelectMany((u) => u.Posts).Include((Expression<Func<Post, ICollection<Tag>>>)((p) => p.Tags)).Include((Expression<Func<Post, ICollection<Comment>>>)((p) => p.Comments)).Include((Expression<Func<Post, Category>>)((p) => p.Category)).Include((Expression<Func<Post, User>>)((p) => p.Author)).ToListAsync(token);
			}, default);
		}
		catch (Exception)
		{
			throw new Exception("Ocurrió un error inesperado al obtener los posts.");
		}
	}

	public async Task<IEnumerable<Post>> GetPostsByCategoryId(Guid categoryId)
	{
		try
		{
			return await _resiliencePipeline.ExecuteAsync(async delegate(CancellationToken token)
			{
				if (!await _db.Category.AnyAsync((Expression<Func<Category, bool>>)((c) => c.Id == categoryId), default))
				{
					throw new ArgumentException("Category not found");
				}
				return await _db.Post.Include((Expression<Func<Post, User>>)((p) => p.Author)).Include((Expression<Func<Post, Category>>)((p) => p.Category)).Include((Expression<Func<Post, ICollection<Tag>>>)((p) => p.Tags)).Include((Expression<Func<Post, ICollection<Comment>>>)((p) => p.Comments)).Where((p) => p.CategoryId == categoryId).ToListAsync(token);
			}, default);
		}
		catch (Exception)
		{
			throw new Exception("Ocurrió un error inesperado al obtener los posts.");
		}
	}

	public async Task<IEnumerable<Post>> GetPostsByTagId(Guid tagId)
	{
		try
		{
			return await _resiliencePipeline.ExecuteAsync(async delegate(CancellationToken token)
			{
				if (!await _db.Tag.AnyAsync((Expression<Func<Tag, bool>>)((t) => t.Id == tagId), default))
				{
					throw new ArgumentException("La etiqueta no existe.");
				}
				return await _db.Post.Include((Expression<Func<Post, User>>)((p) => p.Author)).Include((Expression<Func<Post, Category>>)((p) => p.Category)).Include((Expression<Func<Post, ICollection<Tag>>>)((p) => p.Tags)).Include((Expression<Func<Post, ICollection<Comment>>>)((p) => p.Comments)).Where((p) => p.Tags.Any((t) => t.Id == tagId)).ToListAsync(token);
			}, default);
		}
		catch (Exception)
		{
			throw new Exception("Ocurrió un error inesperado al obtener los posts.");
		}
	}
}
