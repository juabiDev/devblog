using DevBlog.DTOs;
using DevBlog.Entities;

namespace DevBlog.Mappers
{
    public static class UserMapper
    {
        public static User ToEntity(UserDTO user)
        {
            return new User
            {
                Name = user.Name,
                LastName = user.LastName,
                Email = user.Email,
                ProfilePhoto = user.ProfilePhoto ?? string.Empty, 
                About = user.About ?? string.Empty 
            };
        }

        public static UserDTO ToDTO(User user)
        {
            return new UserDTO
            {
                Name = user.Name,
                LastName = user.LastName,
                Email = user.Email,
                ProfilePhoto = user.ProfilePhoto ?? string.Empty,
                About = user.About ?? string.Empty
            };
        }
    }
}
