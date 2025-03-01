
using System.ComponentModel.DataAnnotations;

namespace DevBlog.DTOs
{
    public class PostDTO
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }
        public string Content { get; set; }
        [Required(ErrorMessage = "Image url is required")]
        public string ImageUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        [Required(ErrorMessage = "Author name is required")]
        public string AuthorName { get; set; }
        [Required(ErrorMessage = "Category name is required")]
        public string CategoryName { get; set; }
        public ICollection<TagDTO> Tags { get; set; } = new List<TagDTO>();
        public ICollection<CommentDTO> Comments { get; set; } = new List<CommentDTO>();
    }
}
