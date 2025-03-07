using System.ComponentModel.DataAnnotations;

namespace DevBlog.DTOs
{
    public class CommentDTO
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Content is required")]
        [MinLength(10)]
        [MaxLength(500)]
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        [Required(ErrorMessage = "Author name is required")]
        public string AuthorName { get; set; }
        [Required(ErrorMessage = "Author email is required")]
        public string AuthorEmail { get; set; }
        [Required(ErrorMessage = "Author lastname is required")]
        public string AuthorLastName { get; set; }
        public string AuthorProfilePhoto { get; set; }
    }
}
