using DevBlog.DTOs;
using DevBlog.ServicesContract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DevBlog.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] UserDTO user)
        {
            try
            {
                await _userService.AddUserAsync(user);
                return Ok();
            }
            catch (ArgumentException excep)
            {
                return BadRequest(excep.Message);
            }
        }

    }
}
