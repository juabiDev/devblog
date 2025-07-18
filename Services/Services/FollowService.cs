using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RepositoriesContracts.RepositoriesContracts;
using ServicesContracts.DTOs;
using ServicesContracts.Mappers;
using ServicesContracts.ServicesContracts;

namespace Services.Services;

public class FollowService : IFollowService
{
	private readonly IFollowRepository _followRepository;

	private readonly ILogger<FollowService> _logger;

	public FollowService(IFollowRepository FollowRepository, ILogger<FollowService> logger)
	{
		_logger = logger;
		_followRepository = FollowRepository;
	}

	public async Task FollowUserAsync(FollowRequest followRequest)
	{
		try
		{
			await _followRepository.FollowUserAsync(followRequest);
		}
		catch (DbUpdateException ex)
		{
			DbUpdateException ex2 = ex;
			DbUpdateException ex3 = ex2;
			_logger.LogError(((Exception)(object)ex3).Message, ex3);
			throw new Exception("Error al agregar el usuario en la base de datos.");
		}
		catch (SqlException ex4)
		{
			SqlException ex5 = ex4;
			SqlException ex6 = ex5;
			_logger.LogError(((Exception)(object)ex6).Message, ex6);
			throw new Exception("Error al conectar la base de datos");
		}
		catch (ArgumentException ex7)
		{
			ArgumentException ex8 = ex7;
			_logger.LogError(ex8.Message, ex8);
			throw new ArgumentException(ex8.Message);
		}
		catch (Exception ex9)
		{
			Exception ex10 = ex9;
			_logger.LogError(ex10.Message, ex10);
			throw new Exception("Failed to follow user", ex10);
		}
	}

	public async Task UnfollowUserAsync(FollowRequest followRequest)
	{
		try
		{
			await _followRepository.UnfollowUserAsync(followRequest);
		}
		catch (DbUpdateException ex)
		{
			DbUpdateException ex2 = ex;
			DbUpdateException ex3 = ex2;
			_logger.LogError(((Exception)(object)ex3).Message, ex3);
			throw new Exception("Error al eliminar el usuario de la base de datos.");
		}
		catch (SqlException ex4)
		{
			SqlException ex5 = ex4;
			SqlException ex6 = ex5;
			_logger.LogError(((Exception)(object)ex6).Message, ex6);
			throw new Exception("Error al conectar la base de datos");
		}
		catch (ArgumentException ex7)
		{
			ArgumentException ex8 = ex7;
			_logger.LogError(ex8.Message, ex8);
			throw new ArgumentException(ex8.Message);
		}
		catch (Exception ex9)
		{
			Exception ex10 = ex9;
			_logger.LogError(ex10.Message, ex10);
			throw new Exception("Failed to unfollow user", ex10);
		}
	}

	public async Task<List<UserDTO>> GetFollowersAsync(string userName)
	{
		try
		{
			IEnumerable<Follow> followers = await _followRepository.GetFollowersAsync(userName);
			if (followers == null)
			{
				throw new ArgumentException("User not found");
			}
			return followers.Select((f) => UserMapper.ToDTO(f.Follower)).ToList();
		}
		catch (DbUpdateException ex)
		{
			DbUpdateException ex2 = ex;
			DbUpdateException ex3 = ex2;
			_logger.LogError(((Exception)(object)ex3).Message, ex3);
			throw new Exception("Error al eliminar el usuario de la base de datos.");
		}
		catch (SqlException ex4)
		{
			SqlException ex5 = ex4;
			SqlException ex6 = ex5;
			_logger.LogError(((Exception)(object)ex6).Message, ex6);
			throw new Exception("Error al conectar la base de datos");
		}
		catch (ArgumentException ex7)
		{
			ArgumentException ex8 = ex7;
			_logger.LogError(ex8.Message, ex8);
			throw new ArgumentException(ex8.Message);
		}
		catch (Exception ex9)
		{
			Exception ex10 = ex9;
			_logger.LogError(ex10.Message, ex10);
			throw new Exception("Failed to get followers", ex10);
		}
	}

	public async Task<List<UserDTO>> GetFollowingAsync(string userName)
	{
		try
		{
			IEnumerable<Follow> following = await _followRepository.GetFollowingAsync(userName);
			if (following == null)
			{
				throw new ArgumentException("User not found");
			}
			return following.Select((f) => UserMapper.ToDTO(f.Followed)).ToList();
		}
		catch (DbUpdateException ex)
		{
			DbUpdateException ex2 = ex;
			DbUpdateException ex3 = ex2;
			_logger.LogError(((Exception)(object)ex3).Message, ex3);
			throw new Exception("Error al eliminar el usuario de la base de datos.");
		}
		catch (SqlException ex4)
		{
			SqlException ex5 = ex4;
			SqlException ex6 = ex5;
			_logger.LogError(((Exception)(object)ex6).Message, ex6);
			throw new Exception("Error al conectar la base de datos");
		}
		catch (ArgumentException ex7)
		{
			ArgumentException ex8 = ex7;
			_logger.LogError(ex8.Message, ex8);
			throw new ArgumentException(ex8.Message);
		}
		catch (Exception ex9)
		{
			Exception ex10 = ex9;
			_logger.LogError(ex10.Message, ex10);
			throw new Exception("Failed to get following", ex10);
		}
	}

	public async Task<int> GetFollowersCountAsync(string userName)
	{
		try
		{
			return (await _followRepository.GetFollowersAsync(userName))?.Count() ?? 0;
		}
		catch (DbUpdateException ex)
		{
			DbUpdateException ex2 = ex;
			DbUpdateException ex3 = ex2;
			_logger.LogError(((Exception)(object)ex3).Message, ex3);
			throw new Exception("Error al eliminar el usuario de la base de datos.");
		}
		catch (SqlException ex4)
		{
			SqlException ex5 = ex4;
			SqlException ex6 = ex5;
			_logger.LogError(((Exception)(object)ex6).Message, ex6);
			throw new Exception("Error al conectar la base de datos");
		}
		catch (ArgumentException ex7)
		{
			ArgumentException ex8 = ex7;
			_logger.LogError(ex8.Message, ex8);
			throw new ArgumentException(ex8.Message);
		}
		catch (Exception ex9)
		{
			Exception ex10 = ex9;
			_logger.LogError(ex10.Message, ex10);
			throw new Exception("Failed to get followers count", ex10);
		}
	}

	public async Task<int> GetFollowingCountAsync(string userName)
	{
		try
		{
			return (await _followRepository.GetFollowingAsync(userName))?.Count() ?? 0;
		}
		catch (DbUpdateException ex)
		{
			DbUpdateException ex2 = ex;
			DbUpdateException ex3 = ex2;
			_logger.LogError(((Exception)(object)ex3).Message, ex3);
			throw new Exception("Error al eliminar el usuario de la base de datos.");
		}
		catch (SqlException ex4)
		{
			SqlException ex5 = ex4;
			SqlException ex6 = ex5;
			_logger.LogError(((Exception)(object)ex6).Message, ex6);
			throw new Exception("Error al conectar la base de datos");
		}
		catch (ArgumentException ex7)
		{
			ArgumentException ex8 = ex7;
			_logger.LogError(ex8.Message, ex8);
			throw new ArgumentException(ex8.Message);
		}
		catch (Exception ex9)
		{
			Exception ex10 = ex9;
			_logger.LogError(ex10.Message, ex10);
			throw new Exception("Failed to get following count", ex10);
		}
	}

	public async Task<bool> IsFollowingAsync(FollowRequest followRequest)
	{
		try
		{
			return await _followRepository.IsFollowingAsync(followRequest.FollowerUserName, followRequest.FollowedUserName);
		}
		catch (DbUpdateException ex)
		{
			DbUpdateException ex2 = ex;
			DbUpdateException ex3 = ex2;
			_logger.LogError(((Exception)(object)ex3).Message, ex3);
			throw new Exception("Error al eliminar el usuario de la base de datos.");
		}
		catch (SqlException ex4)
		{
			SqlException ex5 = ex4;
			SqlException ex6 = ex5;
			_logger.LogError(((Exception)(object)ex6).Message, ex6);
			throw new Exception("Error al conectar la base de datos");
		}
		catch (ArgumentException ex7)
		{
			ArgumentException ex8 = ex7;
			_logger.LogError(ex8.Message, ex8);
			throw new ArgumentException(ex8.Message);
		}
		catch (Exception ex9)
		{
			Exception ex10 = ex9;
			_logger.LogError(ex10.Message, ex10);
			throw new Exception("Failed to check if following", ex10);
		}
	}
}
