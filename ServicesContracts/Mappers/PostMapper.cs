using System.Linq;
using Entities;
using ServicesContracts.DTOs;

namespace ServicesContracts.Mappers;

public static class PostMapper
{
	public static PostDTO ToDTO(Post post)
	{
		PostDTO postDTO = new PostDTO();
		postDTO.Id = post.Id;
		postDTO.Title = post.Title;
		postDTO.Content = post.Content;
		postDTO.ImageUrl = post.ImageUrl;
		postDTO.CreatedAt = post.CreatedAt;
		postDTO.CategoryName = post.Category.Name;
		postDTO.AuthorName = post.Author.Name;
		postDTO.Tags = post.Tags.Select(TagMapper.ToDTO).ToList();
		postDTO.Comments = post.Comments.Select(CommentMapper.ToDTO).ToList();
		return postDTO;
	}

	public static Post ToEntity(PostDTO postDTO)
	{
		Post post = new Post();
		post.Id = postDTO.Id;
		post.Title = postDTO.Title;
		post.Content = postDTO.Content;
		post.ImageUrl = postDTO.ImageUrl;
		post.CreatedAt = postDTO.CreatedAt;
		post.Tags = postDTO.Tags.Select(TagMapper.ToEntity).ToList();
		post.Comments = postDTO.Comments.Select(CommentMapper.ToEntity).ToList();
		return post;
	}
}
