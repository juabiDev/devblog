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
using Polly.Retry;
using RepositoriesContracts.RepositoriesContracts;
using ServicesContracts.DTOs;

namespace Repositories;

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
			await _resiliencePipeline.ExecuteAsync((Func<CancellationToken, ValueTask>)async delegate(CancellationToken token)
			{
				if (await EntityFrameworkQueryableExtensions.FirstOrDefaultAsync<Category>(RelationalQueryableExtensions.AsSplitQuery<Category>(((IQueryable<Category>)_db.Category).Where((Category c) => c.Id == post.CategoryId)), token) == null)
				{
					throw new ArgumentException("La categoría no existe.");
				}
				List<Tag> tags = await EntityFrameworkQueryableExtensions.ToListAsync<Tag>(((IQueryable<Tag>)_db.Tag).Where((Tag t) => post.Tags.Contains(t.Id)), token);
				if (tags.Count != post.Tags.Count)
				{
					throw new ArgumentException("Alguna etiqueta no existe.");
				}
				if (await EntityFrameworkQueryableExtensions.FirstOrDefaultAsync<User>(RelationalQueryableExtensions.AsSplitQuery<User>(((IQueryable<User>)_db.User).Where((User u) => u.Id == post.AuthorId)), token) == null)
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
				await ((DbContext)_db).SaveChangesAsync(token);
			}, default(CancellationToken));
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
			await _resiliencePipeline.ExecuteAsync((Func<CancellationToken, ValueTask>)async delegate(CancellationToken token)
			{
				Post post = await EntityFrameworkQueryableExtensions.FirstOrDefaultAsync<Post>(RelationalQueryableExtensions.AsSplitQuery<Post>((IQueryable<Post>)EntityFrameworkQueryableExtensions.Include<Post, User>(((IQueryable<Post>)_db.Post).Where((Post p) => p.Id == id), (Expression<Func<Post, User>>)((Post p) => p.Author))), token);
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
				await ((DbContext)_db).SaveChangesAsync(token);
			}, default(CancellationToken));
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
			return await _resiliencePipeline.ExecuteAsync<List<Post>>((Func<CancellationToken, ValueTask<List<Post>>>)(async (CancellationToken token) => (await EntityFrameworkQueryableExtensions.ToListAsync<Post>((IQueryable<Post>)EntityFrameworkQueryableExtensions.Include<Post, ICollection<Comment>>((IQueryable<Post>)EntityFrameworkQueryableExtensions.Include<Post, ICollection<Tag>>((IQueryable<Post>)EntityFrameworkQueryableExtensions.Include<Post, Category>((IQueryable<Post>)EntityFrameworkQueryableExtensions.Include<Post, User>((IQueryable<Post>)_db.Post, (Expression<Func<Post, User>>)((Post p) => p.Author)), (Expression<Func<Post, Category>>)((Post p) => p.Category)), (Expression<Func<Post, ICollection<Tag>>>)((Post p) => p.Tags)), (Expression<Func<Post, ICollection<Comment>>>)((Post p) => p.Comments)), token)).ToList()), default(CancellationToken));
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
			return await _resiliencePipeline.ExecuteAsync<Post>((Func<CancellationToken, ValueTask<Post>>)(async (CancellationToken token) => await EntityFrameworkQueryableExtensions.FirstOrDefaultAsync<Post>((IQueryable<Post>)EntityFrameworkQueryableExtensions.Include<Post, ICollection<Comment>>((IQueryable<Post>)EntityFrameworkQueryableExtensions.Include<Post, ICollection<Tag>>((IQueryable<Post>)EntityFrameworkQueryableExtensions.Include<Post, Category>((IQueryable<Post>)EntityFrameworkQueryableExtensions.Include<Post, User>((IQueryable<Post>)_db.Post, (Expression<Func<Post, User>>)((Post p) => p.Author)), (Expression<Func<Post, Category>>)((Post p) => p.Category)), (Expression<Func<Post, ICollection<Tag>>>)((Post p) => p.Tags)), (Expression<Func<Post, ICollection<Comment>>>)((Post p) => p.Comments)), (Expression<Func<Post, bool>>)((Post p) => p.Id == id), token)), default(CancellationToken));
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
			return await _resiliencePipeline.ExecuteAsync<List<Post>>((Func<CancellationToken, ValueTask<List<Post>>>)async delegate(CancellationToken token)
			{
				if (!(await EntityFrameworkQueryableExtensions.AnyAsync<User>((IQueryable<User>)_db.User, (Expression<Func<User, bool>>)((User u) => u.Id == authorId), default(CancellationToken))))
				{
					throw new ArgumentException("El autor no existe.");
				}
				return await EntityFrameworkQueryableExtensions.ToListAsync<Post>((IQueryable<Post>)EntityFrameworkQueryableExtensions.Include<Post, User>((IQueryable<Post>)EntityFrameworkQueryableExtensions.Include<Post, Category>((IQueryable<Post>)EntityFrameworkQueryableExtensions.Include<Post, ICollection<Comment>>((IQueryable<Post>)EntityFrameworkQueryableExtensions.Include<Post, ICollection<Tag>>(((IQueryable<User>)_db.User).Where((User u) => u.Id == authorId).SelectMany((User u) => u.Posts), (Expression<Func<Post, ICollection<Tag>>>)((Post p) => p.Tags)), (Expression<Func<Post, ICollection<Comment>>>)((Post p) => p.Comments)), (Expression<Func<Post, Category>>)((Post p) => p.Category)), (Expression<Func<Post, User>>)((Post p) => p.Author)), token);
			}, default(CancellationToken));
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
			return await _resiliencePipeline.ExecuteAsync<List<Post>>((Func<CancellationToken, ValueTask<List<Post>>>)async delegate(CancellationToken token)
			{
				if (!(await EntityFrameworkQueryableExtensions.AnyAsync<Category>((IQueryable<Category>)_db.Category, (Expression<Func<Category, bool>>)((Category c) => c.Id == categoryId), default(CancellationToken))))
				{
					throw new ArgumentException("Category not found");
				}
				return await EntityFrameworkQueryableExtensions.ToListAsync<Post>(((IQueryable<Post>)EntityFrameworkQueryableExtensions.Include<Post, ICollection<Comment>>((IQueryable<Post>)EntityFrameworkQueryableExtensions.Include<Post, ICollection<Tag>>((IQueryable<Post>)EntityFrameworkQueryableExtensions.Include<Post, Category>((IQueryable<Post>)EntityFrameworkQueryableExtensions.Include<Post, User>((IQueryable<Post>)_db.Post, (Expression<Func<Post, User>>)((Post p) => p.Author)), (Expression<Func<Post, Category>>)((Post p) => p.Category)), (Expression<Func<Post, ICollection<Tag>>>)((Post p) => p.Tags)), (Expression<Func<Post, ICollection<Comment>>>)((Post p) => p.Comments))).Where((Post p) => p.CategoryId == categoryId), token);
			}, default(CancellationToken));
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
			return await _resiliencePipeline.ExecuteAsync<List<Post>>((Func<CancellationToken, ValueTask<List<Post>>>)async delegate(CancellationToken token)
			{
				if (!(await EntityFrameworkQueryableExtensions.AnyAsync<Tag>((IQueryable<Tag>)_db.Tag, (Expression<Func<Tag, bool>>)((Tag t) => t.Id == tagId), default(CancellationToken))))
				{
					throw new ArgumentException("La etiqueta no existe.");
				}
				return await EntityFrameworkQueryableExtensions.ToListAsync<Post>(((IQueryable<Post>)EntityFrameworkQueryableExtensions.Include<Post, ICollection<Comment>>((IQueryable<Post>)EntityFrameworkQueryableExtensions.Include<Post, ICollection<Tag>>((IQueryable<Post>)EntityFrameworkQueryableExtensions.Include<Post, Category>((IQueryable<Post>)EntityFrameworkQueryableExtensions.Include<Post, User>((IQueryable<Post>)_db.Post, (Expression<Func<Post, User>>)((Post p) => p.Author)), (Expression<Func<Post, Category>>)((Post p) => p.Category)), (Expression<Func<Post, ICollection<Tag>>>)((Post p) => p.Tags)), (Expression<Func<Post, ICollection<Comment>>>)((Post p) => p.Comments))).Where((Post p) => p.Tags.Any((Tag t) => t.Id == tagId)), token);
			}, default(CancellationToken));
		}
		catch (Exception)
		{
			throw new Exception("Ocurrió un error inesperado al obtener los posts.");
		}
	}
}
