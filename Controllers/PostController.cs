﻿using DevBlog.DTOs;
using DevBlog.ServicesContract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.Common;

namespace DevBlog.Controllers
{
    [ApiController]
    [Route("api/posts")]
    public class PostController : Controller
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreatePostRequest post)
        {
            try
            {
                await _postService.CreatePost(post);
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
        public async Task<ActionResult<List<PostDTO>>> GetAllPosts()
        {
            try
            {
                return await _postService.GetAllPosts();
            }
            catch (Exception excep)
            {
                return StatusCode(500, "El sistema no esta disponible en estos momentos");
            }
        }
    }
}
