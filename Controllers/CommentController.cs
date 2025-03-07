using DevBlog.DTOs;
using DevBlog.ServicesContract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DevBlog.Controllers
{
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
                var comment = await _commentService.GetCommentAsync(id);
                return Ok(comment);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]

        // all properties have to be filled in the request body
        public async Task<IActionResult> AddComment(CreateCommentRequest comment)
        {
            try
            {
                await _commentService.AddCommentAsync(comment);
                return Ok(
                    new
                    {
                        message = "Comment added successfully"
                    }   
                );
            }
            catch(ArgumentException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateComment(Guid id, CreateCommentRequest comment)
        {
            try
            {
                await _commentService.UpdateCommentAsync(id, comment);
                return Ok(
                    new
                    {
                        message = "Comment updated successfully"
                    }
                );
            }
            catch (ArgumentException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id}/{userEmail}")]
        public async Task<IActionResult> DeleteComment(Guid id, string userEmail)
        {
            try
            {
                await _commentService.DeleteCommentAsync(id, userEmail);
                return Ok(
                    new
                    {
                        message = "Comment deleted successfully"
                    }
                );
            }
            catch (ArgumentException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
