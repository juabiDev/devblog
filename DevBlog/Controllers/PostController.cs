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
public class PostController : ControllerBase
{
	private readonly IPostService _postService;

	public PostController(IPostService postService)
	{
		_postService = postService;
	}

	[HttpPost]
	public async Task<IActionResult> Create([FromBody] CreatePostRequest post)
	{
		try
		{
			await _postService.CreatePost(post);
			return Ok();
		}
		catch (ArgumentNullException ex)
		{
			return BadRequest(ex.Message);
		}
		catch (DbException)
		{
			return StatusCode(500, "El sistema no esta disponible en estos momentos");
		}
		catch (Exception)
		{
			return StatusCode(500, "El sistema no esta disponible en estos momentos");
		}
	}

	[HttpDelete("{id}")]
	public async Task<IActionResult> Delete(Guid id, string userEmail)
	{
		try
		{
			await _postService.DeletePost(id, userEmail);
			return Ok(new
			{
				message = "Post was deleted successfully"
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
		catch (Exception)
		{
			return StatusCode(500, "El sistema no esta disponible en estos momentos");
		}
	}

	[HttpGet("{id}")]
	public async Task<ActionResult<PostDTO>> GetPostById(Guid id)
	{
		try
		{
			return await _postService.GetPostById(id);
		}
		catch (ArgumentException ex)
		{
			return BadRequest(ex.Message);
		}
		catch (DbException)
		{
			return StatusCode(500, "El sistema no esta disponible en estos momentos");
		}
		catch (Exception)
		{
			return StatusCode(500, "El sistema no esta disponible en estos momentos");
		}
	}

	[HttpGet("user/{authorId}")]
	public async Task<ActionResult<List<PostDTO>>> GetPostsByAuthor(Guid authorId)
	{
		try
		{
			return await _postService.GetPostsByAuthor(authorId);
		}
		catch (ArgumentException ex)
		{
			return BadRequest(ex.Message);
		}
		catch (DbException)
		{
			return StatusCode(500, "El sistema no esta disponible en estos momentos");
		}
		catch (Exception)
		{
			return StatusCode(500, "El sistema no esta disponible en estos momentos");
		}
	}

	[HttpGet]
	public async Task<ActionResult<List<PostDTO>>> GetAllPosts()
	{
		try
		{
			return await _postService.GetAllPosts();
		}
		catch (Exception)
		{
			return StatusCode(500, "El sistema no esta disponible en estos momentos");
		}
	}
}
