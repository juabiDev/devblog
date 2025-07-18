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
using Repositories.DBContext;
using RepositoriesContracts.RepositoriesContracts;

namespace Repositories.Repositories;

public class UserRepository : IUserRepository
{
	private readonly ResiliencePipeline _resiliencePipeline;

	private readonly BlogDbContext _dbContext;

	public UserRepository(BlogDbContext _db, ResiliencePipeline resiliencePipeline)
	{
		_dbContext = _db;
		_resiliencePipeline = resiliencePipeline;
	}

	public async Task AddUserAsync(User user)
	{
		if (user == null)
		{
			throw new ArgumentNullException("user", "User cannot be null.");
		}
		try
		{
			await _resiliencePipeline.ExecuteAsync(async delegate(CancellationToken token)
			{
				await _dbContext.User.AddAsync(user, token);
				await _dbContext.SaveChangesAsync(token);
			}, default);
		}
		catch (DbUpdateException)
		{
			throw new Exception("Error al agregar el usuario en la base de datos.");
		}
		catch (SqlException)
		{
			throw new Exception("Error al conectar la base de datos");
		}
		catch (Exception value)
		{
			Console.WriteLine($"Error inesperado: {value}");
			throw new Exception("Ocurrió un error inesperado al agregar el usuario.");
		}
	}

	public async Task DeleteUserAsync(Guid id)
	{
		try
		{
			await _resiliencePipeline.ExecuteAsync(async delegate(CancellationToken token)
			{
				User user = await _dbContext.User.Include((Expression<Func<User, ICollection<Follow>>>)((p) => p.Followers)).Include((Expression<Func<User, ICollection<Post>>>)((p) => p.Posts)).AsSplitQuery().IgnoreQueryFilters().FirstOrDefaultAsync((Expression<Func<User, bool>>)((e) => e.Id == id), token);
				if (user == null)
				{
					throw new ArgumentException("User not found");
				}
				if (user.DeletedAt.HasValue)
				{
					throw new ArgumentException("User already deleted");
				}
				user.DeletedAt = DateTime.UtcNow;
				_dbContext.User.Update(user);
				await _dbContext.SaveChangesAsync(token);
			}, default);
		}
		catch (ArgumentNullException)
		{
			throw new ArgumentNullException("id", "User ID cannot be null.");
		}
		catch (DbUpdateException)
		{
			throw new Exception("Error al eliminar el usuario en la base de datos.");
		}
		catch (SqlException)
		{
			throw new Exception("Error al conectar la base de datos");
		}
		catch (ArgumentException ex4)
		{
			ArgumentException ex5 = ex4;
			throw new ArgumentException(ex5.Message);
		}
		catch (Exception ex6)
		{
			Exception ex7 = ex6;
			throw new Exception("Ocurrió un error inesperado al eliminar el usuario. ", ex7);
		}
	}

	public async Task<User> GetUserByEmailAsync(string email)
	{
		try
		{
			return await _resiliencePipeline.ExecuteAsync(async (token) => await _dbContext.User.Include((Expression<Func<User, ICollection<Post>>>)((u) => u.Posts)).Include((Expression<Func<User, ICollection<Follow>>>)((u) => u.Followers)).FirstOrDefaultAsync((Expression<Func<User, bool>>)((u) => u.Email == email), token), default);
		}
		catch (ArgumentException)
		{
			throw new ArgumentException("User not found");
		}
		catch (SqlException)
		{
			throw new Exception("Error al conectar la base de datos");
		}
		catch (Exception ex3)
		{
			Exception ex4 = ex3;
			throw new Exception("Ocurrió un error inesperado al obtener el usuario.", ex4);
		}
	}

	public async Task<User> GetUserByIdAsync(Guid id)
	{
		try
		{
			return await _resiliencePipeline.ExecuteAsync(async (token) => await _dbContext.User.FindAsync(new object[1] { id }), default(CancellationToken));
		}
		catch (ArgumentException)
		{
			throw new ArgumentException("User not found");
		}
		catch (DbUpdateException)
		{
			throw new Exception("Error al obtener el usuario en la base de datos.");
		}
		catch (SqlException)
		{
			throw new Exception("Error al conectar la base de datos");
		}
		catch (Exception)
		{
			throw new Exception("Ocurrió un error inesperado al obtener el usuario.");
		}
	}

	public async Task<User> GetUserByUsernameAsync(string username)
	{
		try
		{
			return await _resiliencePipeline.ExecuteAsync(async (token) => await _dbContext.User.FirstOrDefaultAsync((Expression<Func<User, bool>>)((u) => u.UserName == username), default), default(CancellationToken));
		}
		catch (ArgumentException)
		{
			throw new ArgumentException("User not found");
		}
		catch (DbUpdateException)
		{
			throw new Exception("Error al obtener el usuario en la base de datos.");
		}
		catch (SqlException)
		{
			throw new Exception("Error al conectar la base de datos");
		}
		catch (Exception)
		{
			throw new Exception("Ocurrió un error inesperado al obtener el usuario.");
		}
	}

	public async Task<List<User>> GetUsersAsync()
	{
		try
		{
			return await _resiliencePipeline.ExecuteAsync(async (token) => await _dbContext.User.ToListAsync(token), default);
		}
		catch (DbUpdateException)
		{
			throw new Exception("Error al obtener los usuarios en la base de datos.");
		}
		catch (SqlException)
		{
			throw new Exception("Error al conectar la base de datos");
		}
		catch (Exception)
		{
			throw new Exception("Ocurrió un error inesperado al obtener los usuarios.");
		}
	}

	public async Task EditUserAsync(Guid userId, User user)
	{
		try
		{
			await _resiliencePipeline.ExecuteAsync(async delegate(CancellationToken token)
			{
				if (_dbContext.User.Any((e) => e.UserName == user.UserName && e.Id != userId))
				{
					throw new ArgumentException("Username already exists");
				}
				if (_dbContext.User.Any((e) => e.Email == user.Email && e.Id != userId))
				{
					throw new ArgumentException("Email already exists");
				}
				User userFounded = await _dbContext.User.Where((e) => e.Email == user.Email && e.Id == userId).AsSplitQuery().FirstOrDefaultAsync(token);
				if (userFounded == null)
				{
					throw new ArgumentException("User not found");
				}
				userFounded.Name = user.Name;
				userFounded.UserName = user.UserName;
				userFounded.Email = user.Email;
				userFounded.ProfilePhoto = user.ProfilePhoto;
				userFounded.About = user.About;
				userFounded.UpdatedAt = DateTime.UtcNow;
				_dbContext.User.Update(userFounded);
				await _dbContext.SaveChangesAsync(token);
			}, default);
		}
		catch (DbUpdateException)
		{
			throw new Exception("Error al editar el usuario en la base de datos.");
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
			throw new Exception("Ocurrió un error inesperado al editar el usuario.");
		}
	}
}
