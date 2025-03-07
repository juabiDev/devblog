namespace DevBlog.Entities
{
    public class Comment
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public User Author { get; set; }
        public Guid AuthorId { get; set; }
        public Post Post { get; set; }
        public Guid PostId { get; set; }
    }
}
