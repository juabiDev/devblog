using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.DTOs;
using ServicesContracts.ServicesContracts;

namespace DevBlog.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
	private readonly IUserService _userService;
	private readonly IJwtService _jwtService;

	public UserController(IUserService userService, IJwtService jwtService)
	{
		_userService = userService;
		_jwtService = jwtService;
	}

	[HttpPost]
	public async Task<IActionResult> Create([FromBody] CreateUserRequest user)
	{
		try
		{
			var userCreated = await _userService.AddUserAsync(user);

			var authenticationResponse = _jwtService.CreateJwtToken(userCreated);
			
			return Ok(authenticationResponse);
		}
		catch (ArgumentException ex)
		{
			return BadRequest(ex.Message);
		}
		catch (DbException)
		{
			return StatusCode(500, "El sistema no esta disponible en estos momentos");
		}
		catch (Exception ex)
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
		catch (Exception)
		{
			return StatusCode(500, "El sistema no esta disponible en estos momentos");
		}
	}

	[HttpGet("{id}")]
	public async Task<ActionResult<UserDTO>> GetUserById(Guid id)
	{
		try
		{
			var user = await _userService.GetUserByIdAsync(id: id);

			if (user == null)
			{
				return NotFound(new
				{
					message = "User not found"
				});
			}

			return user;
		}
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception)
		{
			return StatusCode(500, "El sistema no esta disponible en estos momentos");
		}
	}

	[HttpDelete("{id}")]
	public async Task<IActionResult> DeleteUser(Guid id)
	{
		try
		{
			await _userService.DeleteUserAsync(id);

			return Ok(new
			{
				message = "User deleted successfully"
			});
		}
		catch (ArgumentException ex)
		{
			return BadRequest(ex.Message);
		}
		catch (Exception)
		{
			return StatusCode(500, "El sistema no esta disponible en estos momentos");
		}
	}

	[HttpPut]
	public async Task<IActionResult> EditUser(Guid id, UserDTO user)
	{
		try
		{
			await _userService.EditUserAsync(id, user);

			return Ok(new
			{
				message = "User updated successfully"
			});
		}
		catch (ArgumentException ex)
		{
			return BadRequest(ex.Message);
		}
		catch (Exception)
		{
			return StatusCode(500, "El sistema no esta disponible en estos momentos");
		}
	}
}
