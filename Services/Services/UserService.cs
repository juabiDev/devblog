using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RepositoriesContracts.RepositoriesContracts;
using ServicesContracts.DTOs;
using ServicesContracts.Mappers;
using ServicesContracts.ServicesContracts;

namespace Services.Services;

public class UserService : IUserService
{
	private readonly IUserRepository _userRepository;

	private readonly ILogger<UserService> _logger;

	public UserService(IUserRepository userRepository, ILogger<UserService> logger)
	{
		_logger = logger;
		_userRepository = userRepository;
	}

	public async Task<UserDTO> AddUserAsync(CreateUserRequest user)
	{
		if (user == null)
		{
			throw new ArgumentNullException("user", "User cannot be null.");
		}
		try
		{
			User userFoundedByEmail = await _userRepository.GetUserByEmailAsync(user.Email);

			if (userFoundedByEmail != null && userFoundedByEmail.Email == user.Email)
			{
				throw new ArgumentException("User email already exists");
			}
			User userFoundedByName = await _userRepository.GetUserByUsernameAsync(user.UserName);

			if (userFoundedByName != null && userFoundedByName.UserName == user.UserName)
			{
				throw new ArgumentException("Username already exists");
			}

			User userEntity = UserMapper.ToEntity(user);
			PasswordHasher<User> passwordHasher = new PasswordHasher<User>();

			userEntity.PasswordHash = passwordHasher.HashPassword(userEntity, user.Password);

			return UserMapper.ToDTO(await _userRepository.AddUserAsync(userEntity));
		}
		catch (ArgumentNullException ex)
		{
			_logger.LogError(ex.Message, ex);
			throw new ArgumentNullException("User cannot be null.");
		}
		catch (DbUpdateException ex2)
		{
			DbUpdateException ex3 = ex2;
			DbUpdateException ex4 = ex3;
			_logger.LogError(((Exception)(object)ex4).Message, ex4);
			throw new Exception("Error al agregar el usuario en la base de datos.");
		}
		catch (SqlException ex5)
		{
			SqlException ex6 = ex5;
			SqlException ex7 = ex6;
			_logger.LogError(((Exception)(object)ex7).Message, ex7);
			throw new Exception("Error al conectar la base de datos");
		}
		catch (Exception ex8)
		{
			_logger.LogError(ex8.Message, ex8);
			throw new Exception("Ocurrió un error inesperado al agregar el usuario.", ex8);
		}
	}

	public async Task DeleteUserAsync(Guid id)
	{
		try
		{
			await _userRepository.DeleteUserAsync(id);
		}
		catch (DbUpdateException ex)
		{
			DbUpdateException ex2 = ex;
			DbUpdateException ex3 = ex2;
			_logger.LogError(((Exception)(object)ex3).Message, ex3);
			throw new Exception("Error al eliminar el usuario en la base de datos.");
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
			throw new Exception("Ocurrió un error inesperado al eliminar el usuario.");
		}
	}

	public async Task EditUserAsync(Guid userId, UserDTO user)
	{
		try
		{
			if (user == null)
			{
				throw new ArgumentNullException("user", "User cannot be null.");
			}
			User userEntity = UserMapper.ToEntity(user);
			if (userEntity == null)
			{
				throw new ArgumentNullException("userEntity", "User cannot be null.");
			}
			await _userRepository.EditUserAsync(userId, userEntity);
		}
		catch (ArgumentNullException ex)
		{
			ArgumentNullException ex2 = ex;
			_logger.LogError(ex2.Message, ex2);
			throw new ArgumentNullException("User cannot be null.");
		}
		catch (DbUpdateException ex3)
		{
			DbUpdateException ex4 = ex3;
			DbUpdateException ex5 = ex4;
			_logger.LogError(((Exception)(object)ex5).Message, ex5);
			throw new Exception("Error al editar el usuario en la base de datos.");
		}
		catch (SqlException ex6)
		{
			SqlException ex7 = ex6;
			SqlException ex8 = ex7;
			_logger.LogError(((Exception)(object)ex8).Message, ex8);
			throw new Exception("Error al conectar la base de datos");
		}
		catch (ArgumentException ex9)
		{
			ArgumentException ex10 = ex9;
			_logger.LogError(ex10.Message, ex10);
			throw new ArgumentException(ex10.Message);
		}
		catch (Exception ex11)
		{
			Exception ex12 = ex11;
			_logger.LogError(ex12.Message, ex12);
			throw new Exception("Ocurrió un error inesperado al editar el usuario.");
		}
	}

	public async Task<List<UserDTO>> GetAllUsersAsync()
	{
		try
		{
			return (await _userRepository.GetUsersAsync()).Select((user) => UserMapper.ToDTO(user)).ToList();
		}
		catch (DbUpdateException ex)
		{
			DbUpdateException ex2 = ex;
			DbUpdateException ex3 = ex2;
			_logger.LogError(((Exception)(object)ex3).Message, ex3);
			throw new Exception("Error al obtener los usuarios en la base de datos.");
		}
		catch (SqlException ex4)
		{
			SqlException ex5 = ex4;
			SqlException ex6 = ex5;
			_logger.LogError(((Exception)(object)ex6).Message, ex6);
			throw new Exception("Error al conectar la base de datos");
		}
		catch (Exception ex7)
		{
			Exception ex8 = ex7;
			_logger.LogError(ex8.Message, ex8);
			throw new Exception("Ocurrió un error inesperado al obtener los usuarios.");
		}
	}

	public async Task<UserDTO> GetUserByIdAsync(Guid id)
	{
		try
		{
			User user = await _userRepository.GetUserByIdAsync(id);
			if (user == null)
			{
				throw new ArgumentException("User not found");
			}
			return UserMapper.ToDTO(user);
		}
		catch (ArgumentNullException ex)
		{
			ArgumentNullException ex2 = ex;
			_logger.LogError(ex2.Message, ex2);
			throw new ArgumentNullException("id", "User ID cannot be null.");
		}
		catch (ArgumentException ex3)
		{
			ArgumentException ex4 = ex3;
			_logger.LogError(ex4.Message, ex4);
			throw new ArgumentException(ex4.Message);
		}
		catch (DbUpdateException ex5)
		{
			DbUpdateException ex6 = ex5;
			DbUpdateException ex7 = ex6;
			_logger.LogError(((Exception)(object)ex7).Message, ex7);
			throw new Exception("Error al obtener el usuario en la base de datos.");
		}
		catch (SqlException ex8)
		{
			SqlException ex9 = ex8;
			SqlException ex10 = ex9;
			_logger.LogError(((Exception)(object)ex10).Message, ex10);
			throw new Exception("Error al conectar la base de datos");
		}
		catch (Exception ex11)
		{
			Exception ex12 = ex11;
			_logger.LogError(ex12.Message, ex12);
			throw new Exception("Ocurrió un error inesperado al obtener el usuario.");
		}
	}

    public async Task<UserDTO> Login(LoginRequest user)
    {
        try
        {
            User userFounded = await _userRepository.GetUserByEmailAsync(user.Email);

			if (userFounded == null || (user.Password != userFounded.PasswordHash))
			{
				return null;
			}

            return UserMapper.ToDTO(userFounded);
        }
        catch (ArgumentNullException ex)
        {
            ArgumentNullException ex2 = ex;
            _logger.LogError(ex2.Message, ex2);
            throw new ArgumentNullException("id", "User ID cannot be null.");
        }
        catch (ArgumentException ex3)
        {
            ArgumentException ex4 = ex3;
            _logger.LogError(ex4.Message, ex4);
            throw new ArgumentException(ex4.Message);
        }
        catch (DbUpdateException ex5)
        {
            DbUpdateException ex6 = ex5;
            DbUpdateException ex7 = ex6;
            _logger.LogError(((Exception)(object)ex7).Message, ex7);
            throw new Exception("Error al obtener el usuario en la base de datos.");
        }
        catch (SqlException ex8)
        {
            SqlException ex9 = ex8;
            SqlException ex10 = ex9;
            _logger.LogError(((Exception)(object)ex10).Message, ex10);
            throw new Exception("Error al conectar la base de datos");
        }
        catch (Exception ex11)
        {
            Exception ex12 = ex11;
            _logger.LogError(ex12.Message, ex12);
            throw new Exception("Ocurrió un error inesperado al obtener el usuario.");
        }
    }
}
