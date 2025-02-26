namespace DevBlog.DTOs
{
    public class CreatePostRequest
    {
        public Guid AuthorId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ImageUrl { get; set; }
        public Guid CategoryId { get; set; }
        public ICollection<Guid> Tags { get; set; } = new List<Guid>();
    }
}
