using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Entities;
using ServicesContracts.DTOs;

namespace RepositoriesContracts.RepositoriesContracts;

public interface IPostRepository
{
	Task AddPost(CreatePostRequest post);

	Task DeletePost(Guid id, string userEmail);

	Task<Post> GetPostById(Guid id);

	Task<IEnumerable<Post>> GetPostsByAuthorId(Guid authorId);

	Task<IEnumerable<Post>> GetPostsByCategoryId(Guid categoryId);

	Task<IEnumerable<Post>> GetPostsByTagId(Guid tagId);

	Task<IEnumerable<Post>> GetAllPosts();
}
