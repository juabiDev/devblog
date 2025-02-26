
namespace DevBlog.DTOs
{
    public class PostDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public string AuthorName { get; set; }
        public string CategoryName { get; set; }
        public ICollection<TagDTO> Tags { get; set; } = new List<TagDTO>();
        public ICollection<CommentDTO> Comments { get; set; } = new List<CommentDTO>();
    }
}
