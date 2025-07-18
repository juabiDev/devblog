using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.DTOs;
using ServicesContracts.ServicesContracts;

namespace DevBlog.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FollowController : ControllerBase
{
	private readonly IFollowService _followService;

	public FollowController(IFollowService followService)
	{
		_followService = followService;
	}

	[HttpPost("follow")]
	public async Task<IActionResult> FollowUser([FromBody] FollowRequest followRequest)
	{
		try
		{
			await _followService.FollowUserAsync(followRequest);
			return Ok();
		}
		catch (Exception ex)
		{
			return StatusCode(500, "El sistema no esta disponible en estos momentos");
		}
	}

	[HttpPost("unfollow")]
	public async Task<IActionResult> UnfollowUser([FromBody] FollowRequest followRequest)
	{
		try
		{
			await _followService.UnfollowUserAsync(followRequest);
			return Ok();
		}
		catch (Exception ex)
		{

			return StatusCode(500, "El sistema no esta disponible en estos momentos");
		}
	}

	[HttpGet("followers/{userName}")]
	public async Task<IActionResult> GetFollowers(string userName)
	{
		try
		{
			return Ok(await _followService.GetFollowersAsync(userName));
		}
		catch (Exception ex)
		{

			return StatusCode(500, "El sistema no esta disponible en estos momentos");
		}
	}

	[HttpGet("following/{userName}")]
	public async Task<IActionResult> GetFollowing(string userName)
	{
		try
		{
			return Ok(await _followService.GetFollowingAsync(userName));
		}
		catch (Exception ex)
		{
			return StatusCode(500, "El sistema no esta disponible en estos momentos");
		}
	}

	[HttpGet("isfollowing")]
	public async Task<IActionResult> IsFollowing([FromQuery] string followerName, [FromQuery] string followingName)
	{
		try
		{
			return Ok(new
			{
				IsFollowing = await _followService.IsFollowingAsync(new FollowRequest
				{
					FollowerUserName = followerName,
					FollowedUserName = followingName
				})
			});
		}
		catch (Exception ex)
		{
			return StatusCode(500, "El sistema no esta disponible en estos momentos");
		}
	}

	[HttpGet("followerscount/{userName}")]
	public async Task<IActionResult> GetFollowersCount(string userName)
	{
		try
		{
			return Ok(await _followService.GetFollowersCountAsync(userName));
		}
		catch (Exception ex)
		{
			return StatusCode(500, "El sistema no esta disponible en estos momentos");
		}
	}

	[HttpGet("followingcount/{userName}")]
	public async Task<IActionResult> GetFollowingCount(string userName)
	{
		try
		{
			return Ok(await _followService.GetFollowingCountAsync(userName));
		}
		catch (Exception ex)
		{
			return StatusCode(500, "El sistema no esta disponible en estos momentos");
		}
	}
}
