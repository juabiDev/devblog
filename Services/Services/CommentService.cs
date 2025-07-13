using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RepositoriesContracts.RepositoriesContracts;
using ServicesContracts;
using ServicesContracts.DTOs;
using ServicesContracts.Mappers;

namespace Services.Services;

public class CommentService : ICommentService
{
	private readonly ICommentRepository _commentRepository;

	private readonly ILogger<CommentService> _logger;

	public CommentService(ICommentRepository CommentRepository, ILogger<CommentService> logger)
	{
		_logger = logger;
		_commentRepository = CommentRepository;
	}

	public async Task AddCommentAsync(CreateCommentRequest comment)
	{
		try
		{
			await _commentRepository.AddComment(comment);
		}
		catch (DbUpdateException ex)
		{
			DbUpdateException ex2 = ex;
			DbUpdateException ex3 = ex2;
			_logger.LogError(((Exception)(object)ex3).Message, ex3);
			throw new Exception("Error al agregar el comentario en la base de datos.");
		}
		catch (SqlException ex4)
		{
			SqlException ex5 = ex4;
			SqlException ex6 = ex5;
			_logger.LogError(((Exception)(object)ex6).Message, ex6);
			throw new Exception("Error al conectar la base de datos");
		}
		catch (ArgumentException ex7)
		{
			ArgumentException ex8 = ex7;
			_logger.LogError(ex8.Message, ex8);
			throw new ArgumentException(ex8.Message);
		}
		catch (Exception ex9)
		{
			Exception ex10 = ex9;
			_logger.LogError(ex10.Message, ex10);
			throw new Exception("Ocurrió un error inesperado al agregar el comentario.");
		}
	}

	public async Task DeleteCommentAsync(Guid id, string userEmail)
	{
		try
		{
			await _commentRepository.DeleteComment(id, userEmail);
		}
		catch (DbUpdateException ex)
		{
			DbUpdateException ex2 = ex;
			DbUpdateException ex3 = ex2;
			_logger.LogError(((Exception)(object)ex3).Message, ex3);
			throw new Exception("Error al eliminar el comentario en la base de datos.");
		}
		catch (SqlException ex4)
		{
			SqlException ex5 = ex4;
			SqlException ex6 = ex5;
			_logger.LogError(((Exception)(object)ex6).Message, ex6);
			throw new Exception("Error al conectar la base de datos");
		}
		catch (ArgumentException ex7)
		{
			ArgumentException ex8 = ex7;
			_logger.LogError(ex8.Message, ex8);
			throw new ArgumentException(ex8.Message);
		}
		catch (Exception ex9)
		{
			Exception ex10 = ex9;
			_logger.LogError(ex10.Message, ex10);
			throw new Exception("Ocurrió un error inesperado al eliminar el comentario.");
		}
	}

	public async Task<CommentDTO> GetCommentAsync(Guid id)
	{
		try
		{
			Comment comment = await _commentRepository.GetCommentAsync(id);
			if (comment == null)
			{
				throw new ArgumentException("Comentario no encontrado");
			}
			return CommentMapper.ToDTO(comment);
		}
		catch (DbUpdateException ex)
		{
			DbUpdateException ex2 = ex;
			DbUpdateException ex3 = ex2;
			_logger.LogError(((Exception)(object)ex3).Message, ex3);
			throw new Exception("Error al obtener el comentario en la base de datos.");
		}
		catch (SqlException ex4)
		{
			SqlException ex5 = ex4;
			SqlException ex6 = ex5;
			_logger.LogError(((Exception)(object)ex6).Message, ex6);
			throw new Exception("Error al conectar la base de datos");
		}
		catch (ArgumentException ex7)
		{
			ArgumentException ex8 = ex7;
			_logger.LogError(ex8.Message, ex8);
			throw new ArgumentException(ex8.Message);
		}
		catch (Exception ex9)
		{
			Exception ex10 = ex9;
			_logger.LogError(ex10.Message, ex10);
			throw new Exception("Ocurrió un error inesperado al obtener el comentario.");
		}
	}

	public async Task<CommentDTO> GetCommentAsync(Guid id, Guid userId)
	{
		try
		{
			Comment comment = await _commentRepository.GetCommentById(id, userId);
			if (comment == null)
			{
				throw new ArgumentException("Comentario no encontrado");
			}
			return CommentMapper.ToDTO(comment);
		}
		catch (DbUpdateException ex)
		{
			DbUpdateException ex2 = ex;
			DbUpdateException ex3 = ex2;
			_logger.LogError(((Exception)(object)ex3).Message, ex3);
			throw new Exception("Error al obtener el comentario en la base de datos.");
		}
		catch (SqlException ex4)
		{
			SqlException ex5 = ex4;
			SqlException ex6 = ex5;
			_logger.LogError(((Exception)(object)ex6).Message, ex6);
			throw new Exception("Error al conectar la base de datos");
		}
		catch (ArgumentException ex7)
		{
			ArgumentException ex8 = ex7;
			_logger.LogError(ex8.Message, ex8);
			throw new ArgumentException(ex8.Message);
		}
		catch (Exception ex9)
		{
			Exception ex10 = ex9;
			_logger.LogError(ex10.Message, ex10);
			throw new Exception("Ocurrió un error inesperado al obtener el comentario.");
		}
	}

	public async Task<IEnumerable<CommentDTO>> GetCommentsAsync(Guid postId)
	{
		try
		{
			IEnumerable<Comment> comments = await _commentRepository.GetCommentsAsync(postId);
			if (comments == null || !comments.Any())
			{
				throw new ArgumentException("No comments found for this post");
			}
			return comments.Select((c) => CommentMapper.ToDTO(c));
		}
		catch (DbUpdateException ex)
		{
			DbUpdateException ex2 = ex;
			DbUpdateException ex3 = ex2;
			_logger.LogError(((Exception)(object)ex3).Message, ex3);
			throw new Exception("Error al obtener los comentarios en la base de datos.");
		}
		catch (SqlException ex4)
		{
			SqlException ex5 = ex4;
			SqlException ex6 = ex5;
			_logger.LogError(((Exception)(object)ex6).Message, ex6);
			throw new Exception("Error al conectar la base de datos");
		}
		catch (ArgumentException ex7)
		{
			ArgumentException ex8 = ex7;
			_logger.LogError(ex8.Message, ex8);
			throw new ArgumentException(ex8.Message);
		}
		catch (Exception ex9)
		{
			Exception ex10 = ex9;
			_logger.LogError(ex10.Message, ex10);
			throw new Exception("Ocurrió un error inesperado al obtener los comentarios.");
		}
	}

	public async Task UpdateCommentAsync(Guid id, CreateCommentRequest comment)
	{
		try
		{
			await _commentRepository.UpdateCommentAsync(id, comment);
		}
		catch (DbUpdateException ex)
		{
			DbUpdateException ex2 = ex;
			DbUpdateException ex3 = ex2;
			_logger.LogError(((Exception)(object)ex3).Message, ex3);
			throw new Exception("Error al actualizar el comentario en la base de datos.");
		}
		catch (SqlException ex4)
		{
			SqlException ex5 = ex4;
			SqlException ex6 = ex5;
			_logger.LogError(((Exception)(object)ex6).Message, ex6);
			throw new Exception("Error al conectar la base de datos");
		}
		catch (ArgumentException ex7)
		{
			ArgumentException ex8 = ex7;
			_logger.LogError(ex8.Message, ex8);
			throw new ArgumentException(ex8.Message);
		}
		catch (Exception ex9)
		{
			Exception ex10 = ex9;
			_logger.LogError(ex10.Message, ex10);
			throw new Exception("Ocurrió un error inesperado al actualizar el comentario.");
		}
	}
}
