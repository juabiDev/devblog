using DevBlog.DTOs;
using DevBlog.Entities;

namespace DevBlog.Mappers
{
    public static class CommentMapper
    {
        public static CommentDTO ToDTO(Comment comment)
        {
            return new CommentDTO
            {
                Id = comment.Id,
                Text = comment.Text,
                CreatedAt = comment.CreatedAt,
                AuthorName = comment.Author.Name,
                AuthorEmail = comment.Author.Email,
                AuthorLastName = comment.Author.LastName,
                AuthorProfilePhoto = comment.Author.ProfilePhoto
            };
        }
        public static Comment ToEntity(CommentDTO commentDTO)
        {
            return new Comment
            {
                Id = commentDTO.Id,
                Text = commentDTO.Text,
                CreatedAt = commentDTO.CreatedAt,
                Author = new User
                {
                    Name = commentDTO.AuthorName,
                    Email = commentDTO.AuthorEmail,
                    LastName = commentDTO.AuthorLastName,
                    ProfilePhoto = commentDTO.AuthorProfilePhoto
                }
            };
        }
    }
}
