using System.ComponentModel.DataAnnotations;

namespace DevBlog.DTOs
{
    public class CreateCommentRequest
    {
        [Required(ErrorMessage = "User email is required")]
        public string UserEmail { get; set; }
        public Guid PostId { get; set; }
        [Required(ErrorMessage = "Content is required")]
        public string Content { get; set; }
    }
}
