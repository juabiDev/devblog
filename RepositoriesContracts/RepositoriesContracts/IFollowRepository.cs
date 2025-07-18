using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Entities;
using ServicesContracts.DTOs;

namespace RepositoriesContracts.RepositoriesContracts;

public interface IFollowRepository
{
	Task FollowUserAsync(FollowRequest followRequest);

	Task UnfollowUserAsync(FollowRequest unfollowRequest);

	Task<IEnumerable<Follow>> GetFollowersAsync(string username);

	Task<IEnumerable<Follow>> GetFollowingAsync(string username);

	Task<bool> IsFollowingAsync(string followerUsername, string followedUsername);
}
