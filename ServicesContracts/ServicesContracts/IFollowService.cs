using System.Collections.Generic;
using System.Threading.Tasks;
using ServicesContracts.DTOs;

namespace ServicesContracts;

public interface IFollowService
{
	Task FollowUserAsync(FollowRequest followRequest);

	Task UnfollowUserAsync(FollowRequest followRequest);

	Task<List<UserDTO>> GetFollowersAsync(string userName);

	Task<List<UserDTO>> GetFollowingAsync(string userName);

	Task<bool> IsFollowingAsync(FollowRequest followRequest);

	Task<int> GetFollowersCountAsync(string userName);

	Task<int> GetFollowingCountAsync(string userName);
}
