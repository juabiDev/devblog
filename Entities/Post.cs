namespace DevBlog.Entities
{
    public class Post
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public int AuthorId { get; set; }
        public User Author { get; set; }
        public int CategoryId { get; set; }
        public Category category { get; set; }

        public ICollection<Tag> Tags { get; set; } = new List<Tag>();

    }
}
