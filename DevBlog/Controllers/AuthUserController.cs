using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.DTOs;
using ServicesContracts.ServicesContracts;

namespace DevBlog.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthUserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IJwtService _jwtService;

        public AuthUserController(IUserService userService, IJwtService jwtService) { 
            _userService = userService;
            _jwtService = jwtService;
        }

        [HttpPost("/login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest userLogin)
        {
            try
            {
                var user = await _userService.Login(userLogin);

                if (user == null)
                {
                    return Unauthorized("Invalid credentials");
                }

                return Ok(_jwtService.CreateJwtToken(user));
            }
            catch(ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "El sistema no esta disponible en estos momentos");
            }
        }
        
    }
}
