﻿using DevBlog.DTOs;
using DevBlog.ServicesContract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Data.Common;

namespace DevBlog.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
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
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                await _userService.AddUserAsync(user);
                return Ok(new
                {
                    message = "User created successfully"
                });
            }
            catch (ArgumentException excep)
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
                    return NotFound(new
                    {
                        message = "User not found"
                    });
                }
                
                return user;
            }
            catch (Exception excep)
            {
                return StatusCode(500, "El sistema no esta disponible en estos momentos");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(Guid id)
        {
            try
            {
                await _userService.DeleteUserAsync(id);
                return Ok(new
                {
                    message = "User deleted successfully"
                });
            }
            catch(ArgumentException excep)
            {
                return BadRequest(excep.Message);
            }
            catch (Exception excep)
            {
                return StatusCode(500, "El sistema no esta disponible en estos momentos");
            }
        }

        [HttpPut]
        public async Task<ActionResult> EditUser(Guid id, UserDTO user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                await _userService.EditUserAsync(id, user);

                return Ok(new
                {
                    message = "User updated successfully"
                });
            }
            catch (ArgumentException excep)
            {
                return BadRequest(excep.Message);
            }
            catch (Exception excep)
            {
                return StatusCode(500, "El sistema no esta disponible en estos momentos");
            }
        }
    }
}
