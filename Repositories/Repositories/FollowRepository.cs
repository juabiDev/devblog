using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Entities.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Polly;
using Repositories.DBContext;
using RepositoriesContracts.RepositoriesContracts;
using ServicesContracts.DTOs;

namespace Repositories.Repositories;

public class FollowRepository : IFollowRepository
{
	private readonly BlogDbContext _db;

	private readonly ResiliencePipeline _resiliencePipeline;

	public FollowRepository(BlogDbContext db, ResiliencePipeline resiliencePipeline)
	{
		_db = db;
		_resiliencePipeline = resiliencePipeline;
	}

	public async Task FollowUserAsync(FollowRequest followRequest)
	{
		try
		{
			await _resiliencePipeline.ExecuteAsync(async delegate(CancellationToken token)
			{
				User follower = await _db.User.FirstOrDefaultAsync((Expression<Func<User, bool>>)((u) => u.UserName == followRequest.FollowerUserName), token);
				User followed = await _db.User.FirstOrDefaultAsync((Expression<Func<User, bool>>)((u) => u.UserName == followRequest.FollowedUserName), token);
				if (follower == null || followed == null)
				{
					throw new ArgumentException("User not found");
				}
				Follow follow = new Follow
				{
					FollowerId = follower.Id,
					FollowedId = followed.Id
				};
				await _db.Follow.AddAsync(follow, token);
				await _db.SaveChangesAsync(token);
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
		catch (Exception)
		{
			throw new Exception("Ocurrió un error inesperado al agregar el usuario.");
		}
	}

	public async Task<IEnumerable<Follow>> GetFollowersAsync(string username)
	{
		try
		{
			return await _resiliencePipeline.ExecuteAsync(async delegate(CancellationToken token)
			{
				User user = await ((IIncludableQueryable<User, IEnumerable<Follow>>)(object)_db.User.Include((Expression<Func<User, ICollection<Follow>>>)((u) => u.Followers))).ThenInclude((Expression<Func<Follow, User>>)((f) => f.Follower)).AsSplitQuery().FirstOrDefaultAsync((Expression<Func<User, bool>>)((u) => u.UserName == username), token);
				if (user == null)
				{
					throw new ArgumentException("User not found");
				}
				return user.Followers;
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
		catch (ArgumentException ex3)
		{
			ArgumentException ex4 = ex3;
			throw new ArgumentException(ex4.Message);
		}
		catch (Exception ex5)
		{
			Exception ex6 = ex5;
			throw new Exception("Failed to get followers", ex6);
		}
	}

	public async Task<IEnumerable<Follow>> GetFollowingAsync(string username)
	{
		try
		{
			return await _resiliencePipeline.ExecuteAsync(async delegate
			{
				User user = await ((IIncludableQueryable<User, IEnumerable<Follow>>)(object)_db.User.Include((Expression<Func<User, ICollection<Follow>>>)((u) => u.Followers))).ThenInclude((Expression<Func<Follow, User>>)((f) => f.Followed)).AsSplitQuery().FirstOrDefaultAsync((Expression<Func<User, bool>>)((u) => u.UserName == username), default);
				if (user == null)
				{
					throw new ArgumentException("User not found");
				}
				return user.Followers;
			}, default(CancellationToken));
		}
		catch (DbUpdateException)
		{
			throw new Exception("Error al agregar el usuario en la base de datos.");
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
		catch (Exception ex5)
		{
			Exception ex6 = ex5;
			throw new Exception("Failed to get following", ex6);
		}
	}

	public async Task<bool> IsFollowingAsync(string followerUsername, string followedUsername)
	{
		try
		{
			return await _resiliencePipeline.ExecuteAsync(async delegate(CancellationToken token)
			{
				User follower = await _db.User.FirstOrDefaultAsync((Expression<Func<User, bool>>)((u) => u.UserName == followerUsername), token);
				User followed = await _db.User.FirstOrDefaultAsync((Expression<Func<User, bool>>)((u) => u.UserName == followedUsername), token);
				if (follower == null || followed == null)
				{
					throw new ArgumentException("User not found");
				}
				return await _db.Follow.AnyAsync((Expression<Func<Follow, bool>>)((f) => f.FollowerId == follower.Id && f.FollowedId == followed.Id), token);
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
		catch (ArgumentException ex3)
		{
			ArgumentException ex4 = ex3;
			throw new ArgumentException(ex4.Message);
		}
		catch (Exception ex5)
		{
			Exception ex6 = ex5;
			throw new Exception("Failed to check following status", ex6);
		}
	}

	public async Task UnfollowUserAsync(FollowRequest unfollowRequest)
	{
		try
		{
			await _resiliencePipeline.ExecuteAsync(async delegate(CancellationToken token)
			{
				User follower = await _db.User.FirstOrDefaultAsync((Expression<Func<User, bool>>)((u) => u.UserName == unfollowRequest.FollowerUserName), token);
				User followed = await _db.User.FirstOrDefaultAsync((Expression<Func<User, bool>>)((u) => u.UserName == unfollowRequest.FollowedUserName), token);
				if (follower == null || followed == null)
				{
					throw new ArgumentException("User not found");
				}
				Follow follow = await _db.Follow.FirstOrDefaultAsync((Expression<Func<Follow, bool>>)((f) => f.FollowerId == follower.Id && f.FollowedId == followed.Id), token);
				if (follow == null)
				{
					throw new ArgumentException("Follow relationship not found");
				}
				_db.Follow.Remove(follow);
				await _db.SaveChangesAsync(token);
			}, default);
		}
		catch (DbUpdateException)
		{
			throw new Exception("Error al eliminar el usuario de la base de datos.");
		}
		catch (SqlException)
		{
			throw new Exception("Error al conectar la base de datos");
		}
		catch (Exception)
		{
			throw new Exception("Ocurrió un error inesperado al eliminar el usuario.");
		}
	}
}
