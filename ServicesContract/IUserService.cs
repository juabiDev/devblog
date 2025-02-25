using DevBlog.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace DevBlog.ServicesContract
{
    public interface IUserService
    {
        Task<List<UserDTO>> GetAllUsersAsync();
        Task<UserDTO?> GetUserByIdAsync(Guid id);
        Task DeleteUserAsync(Guid id);
        Task EditUserAsync(UserDTO user);
        Task AddUserAsync(UserDTO user);
    }
}
