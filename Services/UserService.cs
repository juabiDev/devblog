using DevBlog.DTOs;
using DevBlog.Entities;
using DevBlog.ServicesContract;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Polly;
using Polly.Retry;
using System.Data.Common;

namespace DevBlog.Services
{
    public class UserService : IUserService
    {
        private readonly BlogDbContext _db;
        private readonly ResiliencePipeline _resiliencePipeline;
        public UserService(BlogDbContext Db)
        {
            _db = Db;

            // Retry policy for database operations
            _resiliencePipeline = new ResiliencePipelineBuilder()
                .AddRetry(new RetryStrategyOptions
                {
                    ShouldHandle = new PredicateBuilder()
                        .Handle<DbUpdateException>()  // Handle DbUpdateException
                        .Handle<SqlException>(), // Handle SqlException
                    MaxRetryAttempts = 3, // Retry 3 times
                    Delay = TimeSpan.FromSeconds(2), // Delay 2 seconds between retries
                    OnRetry = retryArgs =>
                    {
                        return default;
                    }
                })
                .Build();
        }

        public async Task AddUserAsync(UserDTO user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "User cannot be null.");
            }

            // Execute the operation with the resilience pipeline
            try
            {
                await _resiliencePipeline.ExecuteAsync(async token =>
                {
                    User userEntity = Mappers.UserMapper.MapToEntity(user);
                    await _db.User.AddAsync(userEntity);
                    await _db.SaveChangesAsync(token);
                });
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Error al agregar el usuario en la base de datos.");
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

        public async Task DeleteUserAsync(Guid id)
        {
            try
            {
                await _resiliencePipeline.ExecuteAsync(async token =>
                {
                    var user = await _db.User.FindAsync(id);
                    if (user == null)
                    {
                        throw new ArgumentNullException(nameof(user), "El usuario no existe.");
                    }
                    user.DeletedAt = DateTime.UtcNow;
                    _db.User.Update(user);
                    await _db.SaveChangesAsync(token);
                });
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Error al eliminar el usuario en la base de datos.");
            }
            catch (SqlException ex)
            {
                throw new Exception("Error al conectar la base de datos");
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error inesperado al eliminar el usuario.");
            }
        }

        public async Task EditUserAsync(UserDTO user)
        {
            try
            {
                await _resiliencePipeline.ExecuteAsync(async token =>
                {
                    User userEntity = Mappers.UserMapper.MapToEntity(user);
                    var userFounded = await _db.User.Where(e => e.Email == userEntity.Email).FirstOrDefaultAsync(token);
                    
                    if (userFounded == null)
                    {
                        throw new ArgumentNullException(nameof(user), "El usuario no existe.");
                    }

                    userFounded.Name = user.Name;
                    userFounded.LastName = user.LastName;
                    userFounded.Email = user.Email;
                    userFounded.ProfilePhoto = user.ProfilePhoto;
                    userFounded.About = user.About;
                    _db.User.Update(userFounded);
                    await _db.SaveChangesAsync(token);
                });
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Error al editar el usuario en la base de datos.");
            }
            catch (SqlException ex)
            {
                throw new Exception("Error al conectar la base de datos");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception("Ocurrió un error inesperado al editar el usuario.");
            }
        }

        public async Task<List<UserDTO>> GetAllUsersAsync()
        {
            try
            {
                return await _resiliencePipeline.ExecuteAsync(async token =>
                {
                    var users = await _db.User.ToListAsync(token);
                    return users.Select(Mappers.UserMapper.MapToDTO).ToList();
                });
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Error al obtener los usuarios en la base de datos.");
                
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
                return await _resiliencePipeline.ExecuteAsync(async token =>
                {
                    var user = await _db.User.FindAsync(id);
                    return user == null ? null : Mappers.UserMapper.MapToDTO(user);
                });
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
