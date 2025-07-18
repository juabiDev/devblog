using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ServicesContracts.DTOs;

namespace ServicesContracts.ServicesContracts;

public interface ICommentService
{
	Task AddCommentAsync(CreateCommentRequest comment);

	Task DeleteCommentAsync(Guid id, string userEmail);

	Task UpdateCommentAsync(Guid id, CreateCommentRequest comment);

	Task<IEnumerable<CommentDTO>> GetCommentsAsync(Guid postId);

	Task<CommentDTO> GetCommentAsync(Guid id);

	Task<CommentDTO> GetCommentAsync(Guid id, Guid userId);
}
