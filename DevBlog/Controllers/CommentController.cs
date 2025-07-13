using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts;
using ServicesContracts.DTOs;

namespace DevBlog.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CommentController : Controller
{
	private readonly ICommentService _commentService;

	public CommentController(ICommentService commentService)
	{
		_commentService = commentService;
	}

	[HttpGet("{id}")]
	public async Task<IActionResult> GetComentById(Guid id)
	{
		try
		{
			return Ok(await _commentService.GetCommentAsync(id));
		}
		catch (ArgumentNullException ex)
		{
			return BadRequest(ex.Message);
		}
		catch (ArgumentException ex)
		{
			return BadRequest(ex.Message);
		}
		catch (Exception ex)
		{
			return StatusCode(500, ex.Message);
		}
	}

	[HttpPost]
	public async Task<IActionResult> AddComment(CreateCommentRequest comment)
	{
		try
		{
			await _commentService.AddCommentAsync(comment);
			return Ok(new
			{
				message = "Comment added successfully"
			});
		}
		catch (ArgumentException ex)
		{
			return BadRequest(ex.Message);
		}
		catch (Exception ex)
		{
			return StatusCode(500, ex.Message);
		}
	}

	[HttpPut]
	public async Task<IActionResult> UpdateComment(Guid id, CreateCommentRequest comment)
	{
		try
		{
			await _commentService.UpdateCommentAsync(id, comment);
			return Ok(new
			{
				message = "Comment updated successfully"
			});
		}
		catch (ArgumentException ex)
		{
			return BadRequest(ex.Message);
		}
		catch (Exception ex)
		{
			return StatusCode(500, ex.Message);
		}
	}

	[HttpDelete("{id}/{userEmail}")]
	public async Task<IActionResult> DeleteComment(Guid id, string userEmail)
	{
		try
		{
			await _commentService.DeleteCommentAsync(id, userEmail);
			return Ok(new
			{
				message = "Comment deleted successfully"
			});
		}
		catch (ArgumentException ex)
		{
			ArgumentException ex2 = ex;
			return BadRequest(ex2.Message);
		}
		catch (Exception ex)
		{
			return StatusCode(500, ex.Message);
		}
	}
}
