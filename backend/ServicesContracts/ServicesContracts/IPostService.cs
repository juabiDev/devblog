using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Entities;
using ServicesContracts.DTOs;

namespace ServicesContracts;

public interface IPostService
{
	Task CreatePost(CreatePostRequest post);

	Task<PostDTO> UpdatePost(Post post, string userEmail);

	Task DeletePost(Guid id, string userEmail);

	Task<PostDTO> GetPostById(Guid id);

	Task<List<PostDTO>> GetAllPosts();

	Task<List<PostDTO>> GetPostsByCategory(Guid categoryId);

	Task<List<PostDTO>> GetPostsByTag(Guid tagId);

	Task<List<PostDTO>> GetPostsByAuthor(Guid authorId);
}
