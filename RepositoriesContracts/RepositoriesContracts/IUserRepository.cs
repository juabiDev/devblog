using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Entities;

namespace RepositoriesContracts.RepositoriesContracts;

public interface IUserRepository
{
	Task<User> GetUserByIdAsync(Guid id);

	Task<User> GetUserByEmailAsync(string email);

	Task<User> GetUserByUsernameAsync(string username);

	Task<User> AddUserAsync(User user);

	Task EditUserAsync(Guid userId, User user);

	Task DeleteUserAsync(Guid id);

	Task<List<User>> GetUsersAsync();
}
