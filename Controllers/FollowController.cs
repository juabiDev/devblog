using DevBlog.DTOs;
using DevBlog.ServicesContract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DevBlog.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FollowController : Controller
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
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
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
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("followers/{userName}")]
        public async Task<IActionResult> GetFollowers(string userName)
        {
            try
            {
                var followers = await _followService.GetFollowersAsync(userName);
                return Ok(followers);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("following/{userName}")]
        public async Task<IActionResult> GetFollowing(string userName)
        {
            try
            {
                var following = await _followService.GetFollowingAsync(userName);
                return Ok(following);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("isfollowing")]
        public async Task<IActionResult> IsFollowing([FromQuery] string followerName, [FromQuery] string followingName)
        {
            try
            {
                var isFollowing = await _followService.IsFollowingAsync(new FollowRequest { FollowerUserName = followerName, FollowedUserName = followingName });

                return Ok(new { IsFollowing = isFollowing }); 
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("followerscount/{userName}")]
        public async Task<IActionResult> GetFollowersCount(string userName)
        {
            try
            {
                var followersCount = await _followService.GetFollowersCountAsync(userName);
                return Ok(followersCount);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("followingcount/{userName}")]
        public async Task<IActionResult> GetFollowingCount(string userName)
        {
            try
            {
                var followingCount = await _followService.GetFollowingCountAsync(userName);
                return Ok(followingCount);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}
