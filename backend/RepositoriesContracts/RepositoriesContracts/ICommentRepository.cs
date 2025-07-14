using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Entities;
using ServicesContracts.DTOs;

namespace RepositoriesContracts.RepositoriesContracts;

public interface ICommentRepository
{
	Task AddComment(CreateCommentRequest comment);

	Task DeleteComment(Guid id, string userEmail);

	Task<Comment> GetCommentAsync(Guid id);

	Task<Comment> GetCommentById(Guid id, Guid userId);

	Task<IEnumerable<Comment>> GetCommentsAsync(Guid postId);

	Task UpdateCommentAsync(Guid id, CreateCommentRequest comment);
}
