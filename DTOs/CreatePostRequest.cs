using System.ComponentModel.DataAnnotations;

namespace DevBlog.DTOs
{
    public class CreatePostRequest
    {
        [Required(ErrorMessage = "Author id is required")]
        public Guid AuthorId { get; set; }
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Content is required")]
        public string Content { get; set; }
        [Required(ErrorMessage = "Image url is required")]
        public string ImageUrl { get; set; }
        [Required(ErrorMessage = "Category id is required")]
        public Guid CategoryId { get; set; }
        [Required(ErrorMessage = "Tags are required")]
        [MinLength(3, ErrorMessage = "At least three tags are required")]
        [MaxLength(5, ErrorMessage = "At most five tags are allowed")]
        public ICollection<Guid> Tags { get; set; } = new List<Guid>();
    }
}
