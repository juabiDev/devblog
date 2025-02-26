using DevBlog.DTOs;
using DevBlog.Entities;

namespace DevBlog.Mappers
{
    public static class UserMapper
    {
        public static User MapToEntity(UserDTO user)
        {
            return new User
            {
                Name = user.Name,
                LastName = user.LastName,
                Email = user.Email,
                ProfilePhoto = user.ProfilePhoto,
                About = user.About
            };
        }

        public static UserDTO MapToDTO(User user)
        {
            return new UserDTO
            {
                Name = user.Name,
                LastName = user.LastName,
                Email = user.Email,
                ProfilePhoto = user.ProfilePhoto,
                About = user.About
            };
        }
    }
}
