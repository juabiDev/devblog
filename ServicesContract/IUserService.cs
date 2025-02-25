using DevBlog.DTOs;

namespace DevBlog.ServicesContract
{
    public interface IUserService
    {
        public List<UserDTO> GetAllUsers();
        public UserDTO GetUserById(Guid id);
        public void DeleteUser(Guid id);
        public void EditUser(UserDTO user);
        Task AddUserAsync(UserDTO user);
    }
}
