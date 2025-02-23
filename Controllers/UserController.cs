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
        public ActionResult Create([FromBody] UserDTO user)
        {
            try
            {
                _userService.AddUser(user);
                return Ok();
            }
            catch (ArgumentException excep)
            {
                return BadRequest(excep.Message);
            }
        }

    }
}
