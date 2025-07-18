using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RepositoriesContracts.RepositoriesContracts;
using ServicesContracts.DTOs;
using ServicesContracts.Mappers;
using ServicesContracts.ServicesContracts;

namespace Services.Services;

public class PostService : IPostService
{
	private readonly IPostRepository _postRepository;

	private readonly ILogger<PostService> _logger;

	public PostService(IPostRepository PostRepository, ILogger<PostService> logger)
	{
		_logger = logger;
		_postRepository = PostRepository;
	}

	public async Task CreatePost(CreatePostRequest post)
	{
		try
		{
			await _postRepository.AddPost(post);
		}
		catch (ArgumentException ex)
		{
			ArgumentException ex2 = ex;
			throw new ArgumentException(ex2.Message);
		}
		catch (Exception ex3)
		{
			Exception ex4 = ex3;
			_logger.LogError(ex4.Message, ex4);
			throw new Exception("Ocurrió un error inesperado al agregar el post.");
		}
	}

	public async Task DeletePost(Guid id, string userEmail)
	{
		try
		{
			await _postRepository.DeletePost(id, userEmail);
		}
		catch (DbUpdateException ex)
		{
			DbUpdateException ex2 = ex;
			DbUpdateException ex3 = ex2;
			_logger.LogError(((Exception)(object)ex3).Message, ex3);
			throw new DbUpdateException("Ocurrió un error al eliminar el post en la base de datos.", (Exception)(object)ex3);
		}
		catch (ArgumentException ex4)
		{
			ArgumentException ex5 = ex4;
			throw new ArgumentException(ex5.Message);
		}
		catch (Exception ex6)
		{
			Exception ex7 = ex6;
			_logger.LogError(ex7.Message, ex7);
			throw new Exception("Ocurrió un error inesperado al eliminar el post.");
		}
	}

	public async Task<List<PostDTO>> GetAllPosts()
	{
		try
		{
			return (await _postRepository.GetAllPosts()).Select((p) => PostMapper.ToDTO(p)).ToList();
		}
		catch (Exception ex)
		{
			Exception ex2 = ex;
			_logger.LogError(ex2.Message, ex2);
			throw new Exception("Ocurrió un error inesperado al obtener los posts.");
		}
	}

	public async Task<PostDTO> GetPostById(Guid id)
	{
		try
		{
			return PostMapper.ToDTO(await _postRepository.GetPostById(id));
		}
		catch (ArgumentException ex)
		{
			ArgumentException ex2 = ex;
			_logger.LogError(ex2.Message, ex2);
			throw new ArgumentException(ex2.Message);
		}
		catch (Exception ex3)
		{
			Exception ex4 = ex3;
			_logger.LogError(ex4.Message, ex4);
			throw new Exception("Ocurrió un error inesperado al obtener el post.");
		}
	}

	public async Task<List<PostDTO>> GetPostsByAuthor(Guid authorId)
	{
		try
		{
			return (await _postRepository.GetPostsByAuthorId(authorId)).Select((p) => PostMapper.ToDTO(p)).ToList();
		}
		catch (Exception ex)
		{
			Exception ex2 = ex;
			_logger.LogError(ex2.Message, ex2);
			throw new Exception("Ocurrió un error inesperado al obtener los posts.");
		}
	}

	public async Task<List<PostDTO>> GetPostsByCategory(Guid categoryId)
	{
		try
		{
			return (await _postRepository.GetPostsByCategoryId(categoryId)).Select((p) => PostMapper.ToDTO(p)).ToList();
		}
		catch (Exception ex)
		{
			Exception ex2 = ex;
			_logger.LogError(ex2.Message, ex2);
			throw new Exception("Ocurrió un error inesperado al obtener los posts.");
		}
	}

	public async Task<List<PostDTO>> GetPostsByTag(Guid tagId)
	{
		try
		{
			return (await _postRepository.GetPostsByTagId(tagId)).Select((p) => PostMapper.ToDTO(p)).ToList();
		}
		catch (Exception ex)
		{
			Exception ex2 = ex;
			_logger.LogError(ex2.Message, ex2);
			throw new Exception("Ocurrió un error inesperado al obtener los posts.");
		}
	}

	public async Task<PostDTO> UpdatePost(Post post, string userEmail)
	{
		throw new NotImplementedException();
	}
}
