using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts;
using ServicesContracts.DTOs;

namespace DevBlog.Controllers;

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
	public async Task<IActionResult> Create([FromBody] UserDTO user)
	{
		try
		{
			await _userService.AddUserAsync(user);

			return Ok(new
			{
				message = "User created successfully"
			});
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
