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

        public async Task AddUserAsync(UserDTO user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "El usuario no puede ser nulo.");
            }

            try
            {
                var newUser = new User
                {
                    Name = user.Name,
                    LastName = user.LastName,
                    Email = user.Email,
                    ProfilePhoto = user.ProfilePhoto,
                    About = user.About,
                    CreatedAt = DateTime.Now
                };

                _db.User.Add(newUser);
                await _db.SaveChangesAsync(); // Uso de la versión asíncrona
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"DbUpdateException: {ex.Message}");
                throw new Exception("Error al guardar el usuario en la base de datos.", ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                throw new Exception("Ocurrió un error inesperado al agregar el usuario.", ex);
            }
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
