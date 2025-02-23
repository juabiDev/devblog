using DevBlog.DTOs;
using DevBlog.Entities;
using DevBlog.ServicesContract;
using Microsoft.EntityFrameworkCore;

namespace DevBlog.Services
{
    public class UserService : IUserService
    {
        private readonly BlogDbContext _db;
        public UserService(BlogDbContext Db)
        {
            _db = Db;
        }

        public void AddUser(UserDTO user)
        {
            _db.User.Add(new User
            {
                Name = user.Name,
                LastName = user.LastName, 
                Email = user.Email,
                ProfilePhoto = user.ProfilePhoto,
                About = user.About,
                CreatedAt = DateTime.Now
            });

            _db.SaveChanges();
        }

        public void DeleteUser(Guid id)
        {
            throw new NotImplementedException();
        }

        public void EditUser(UserDTO user)
        {
            throw new NotImplementedException();
        }

        public List<UserDTO> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public UserDTO GetUserById(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
