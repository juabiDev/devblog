using DevBlog.DTOs;
using DevBlog.ServicesContract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.Common;

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
            catch (ArgumentNullException excep)
            {
                return BadRequest(excep.Message);
            }
            catch (DbException excep)
            {
                return StatusCode(500, "El sistema no esta disponible en estos momentos");
            }
            catch (Exception excep)
            {
                return StatusCode(500, "El sistema no esta disponible en estos momentos");
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<UserDTO>>> GetAllUsers()
        {
            try
            {
                return await _userService.GetAllUsersAsync();
            }
            catch (Exception excep)
            {
                return StatusCode(500, "El sistema no esta disponible en estos momentos");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetUserById(Guid id)
        {
            try
            {
                var user = await _userService.GetUserByIdAsync(id);
                if (user == null)
                {
                    return NotFound();
                }
                return user;
            }
            catch (Exception excep)
            {
                return StatusCode(500, "El sistema no esta disponible en estos momentos");
            }
        }
    }
}
