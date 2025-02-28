using DevBlog.DTOs;
using DevBlog.Entities;

namespace DevBlog.Mappers
{
    public static class PostMapper
    {

        public static PostDTO ToDTO(Post post)
        {
            return new PostDTO
            {
                Id = post.Id,
                Title = post.Title,
                Content = post.Content,
                ImageUrl = post.ImageUrl,
                CreatedAt = post.CreatedAt,
                CategoryName = post.Category.Name,
                AuthorName = post.Author.Name,
                Tags = post.Tags.Select(TagMapper.ToDTO).ToList(),
                Comments = post.Comments.Select(CommentMapper.ToDTO).ToList()
            };

        }

        public static Post ToEntity(PostDTO postDTO)
        {
            return new Post
            {
                Id = postDTO.Id,
                Title = postDTO.Title,
                Content = postDTO.Content,
                ImageUrl = postDTO.ImageUrl,
                CreatedAt = postDTO.CreatedAt,
                Tags = postDTO.Tags.Select(TagMapper.ToEntity).ToList(),
                Comments = postDTO.Comments.Select(CommentMapper.ToEntity).ToList()
            };
        }
    }
}
