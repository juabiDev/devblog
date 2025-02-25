using DevBlog.DTOs;
using DevBlog.Entities;
using DevBlog.ServicesContract;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

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
                    CreatedAt = DateTime.UtcNow
                };

                _db.User.Add(newUser);
                await _db.SaveChangesAsync(); 
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Error al guardar el usuario en la base de datos.");
            }
            catch (SqlException ex)
            {
                throw new Exception("Error al conectar la base de datos");
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error inesperado al agregar el usuario.");
            }
        }

        public Task DeleteUserAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task EditUserAsync(UserDTO user)
        {
            throw new NotImplementedException();
        }

        public async Task<List<UserDTO>> GetAllUsersAsync()
        {
            try
            {
                List<User> users = await _db.User.ToListAsync();
                return users.Select(u => Mappers.UserMapper.MapToDTO(u)).ToList();
            }
            catch (SqlException ex)
            {
                throw new Exception("Error al conectar la base de datos");
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error inesperado al obtener los usuarios.");
            }
        }

        public async Task<UserDTO?> GetUserByIdAsync(Guid id)
        {
            try
            {
                var user = await _db.User.FindAsync(id);

                return user == null 
                    ? null 
                    : Mappers.UserMapper.MapToDTO(user);
            }
            catch (SqlException ex)
            {
                throw new Exception("Error al conectar la base de datos");
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error inesperado al obtener el usuario.");
            }
        }
    }
}
