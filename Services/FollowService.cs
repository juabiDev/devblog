using DevBlog.DTOs;
using DevBlog.Entities;
using DevBlog.Mappers;
using DevBlog.ServicesContract;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Polly;
using Polly.Retry;

namespace DevBlog.Services
{
    public class FollowService : IFollowService
    {
        private readonly BlogDbContext _db;
        private readonly ResiliencePipeline _resiliencePipeline;

        public FollowService(BlogDbContext db)
        {
            _db = db;

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

        public async Task FollowUserAsync(FollowRequest followRequest)
        {
            try
            {
                await _resiliencePipeline.ExecuteAsync(async token =>
                {
                    var follower = await _db.User.FirstOrDefaultAsync(u => u.UserName == followRequest.FollowerUserName, token);
                    var followed = await _db.User.FirstOrDefaultAsync(u => u.UserName == followRequest.FollowedUserName, token);

                    if (follower == null || followed == null)
                    {
                        throw new ArgumentException("User not found");
                    }

                    var follow = new Follow
                    {
                        FollowerId = follower.Id,
                        FollowedId = followed.Id
                    };

                    await _db.Follow.AddAsync(follow, token);
                    await _db.SaveChangesAsync(token);
                });
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to follow user", ex);
            }
        }

        public async Task<List<UserDTO>> GetFollowersAsync(string userName)
        {
            try
            {
                return await _resiliencePipeline.ExecuteAsync(async token =>
                {
                    var user = await _db.User
                        .Include(u => u.Followers)
                        .ThenInclude(f => f.Follower)
                        .AsSplitQuery()
                        .FirstOrDefaultAsync(u => u.UserName == userName, token);
                    
                    if (user == null)
                    {
                        throw new ArgumentException("User not found");
                    }

                   return user.Followers.Select(f => UserMapper.ToDTO(f.Follower)).ToList();
                });
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to get followers", ex);
            }
        }

        public async Task<List<UserDTO>> GetFollowingAsync(string userName)
        {
            try
            {
                return await _resiliencePipeline.ExecuteAsync(async token =>
                {
                    var user = await _db.User
                        .FirstOrDefaultAsync(u => u.UserName == userName, token);

                    if (user == null)
                    {
                        throw new ArgumentException("User not found");
                    }

                    var following = await _db.Follow
                        .Include(f => f.Followed)
                        .Where(f => f.FollowerId == user.Id)
                        .AsSplitQuery()
                        .ToListAsync(token);

                    return following.Select(f => UserMapper.ToDTO(f.Followed)).ToList();
                });
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to get following", ex);
            }
        }

        public async Task<int> GetFollowersCountAsync(string userName)
        {
            try
            {
                return await _resiliencePipeline.ExecuteAsync(async token =>
                {
                    var user = await _db.User
                        .Include(u => u.Followers)
                        .ThenInclude(f => f.Follower)
                        .AsSplitQuery()
                        .FirstOrDefaultAsync(u => u.UserName == userName, token);

                    if (user == null)
                    {
                        throw new ArgumentException("User not found");
                    }

                    return await _db.Follow
                        .CountAsync(f => f.FollowedId == user.Id, token);
                });
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to get followers count", ex);
            }
        }

        public async Task<int> GetFollowingCountAsync(string userName)
        {
            try
            {
                return await _resiliencePipeline.ExecuteAsync(async token =>
                {
                    var user = await _db.User
                        .Include(u => u.Followers)
                        .ThenInclude(f => f.Followed)
                        .AsSplitQuery()
                        .FirstOrDefaultAsync(u => u.UserName == userName, token);
                    
                    if (user == null)
                    {
                        throw new ArgumentException("User not found");
                    }
             
                    return await _db.Follow
                        .CountAsync(f => f.FollowerId == user.Id, token);
                });
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to get following count", ex);
            }
        }

        public async Task<bool> IsFollowingAsync(FollowRequest followRequest)
        {
            try
            {
                return await _resiliencePipeline.ExecuteAsync(async token =>
                {
                    var follower = await _db.User.FirstOrDefaultAsync(u => u.UserName == followRequest.FollowerUserName, token);
                    var followed = await _db.User.FirstOrDefaultAsync(u => u.UserName == followRequest.FollowedUserName, token);
                    
                    if (follower == null || followed == null)
                    
                    {
                        throw new ArgumentException("User not found");
                    }
                    
                    return await _db.Follow.AnyAsync(f => f.FollowerId == follower.Id && f.FollowedId == followed.Id, token);
                });
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to check if following", ex);
            }
        }

        public async Task UnfollowUserAsync(FollowRequest followRequest)
        {
            try
            {
                await _resiliencePipeline.ExecuteAsync(async token =>
                {
                    var follower = await _db.User.FirstOrDefaultAsync(u => u.UserName == followRequest.FollowerUserName, token);
                    var followed = await _db.User.FirstOrDefaultAsync(u => u.UserName == followRequest.FollowedUserName, token);
                    
                    if (follower == null || followed == null)
                    {
                        throw new ArgumentException("User not found");
                    }
                    
                    var follow = await _db.Follow.FirstOrDefaultAsync(f => f.FollowerId == follower.Id && f.FollowedId == followed.Id, token);
                   
                    if (follow == null)
                    {
                        throw new ArgumentException("User is not following");
                    }

                    _db.Follow.Remove(follow);
                    await _db.SaveChangesAsync(token);
                });
            }
            catch(ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to unfollow user", ex);
            }
        }
    }
}
