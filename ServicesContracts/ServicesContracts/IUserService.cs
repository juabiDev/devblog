using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ServicesContracts.DTOs;

namespace ServicesContracts.ServicesContracts;

public interface IUserService
{
	Task<List<UserDTO>> GetAllUsersAsync();

	Task<UserDTO> GetUserByIdAsync(Guid id);

	Task DeleteUserAsync(Guid id);

	Task EditUserAsync(Guid userId, UserDTO user);

	Task<UserDTO> AddUserAsync(CreateUserRequest user);

	Task<UserDTO> Login(LoginRequest login);
}
