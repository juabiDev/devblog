namespace DevBlog.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string? ProfilePhoto { get; set; }
        public string? About { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public ICollection<Follow> Followers { get; set; } = new List<Follow>();
        public ICollection<Post> Posts { get; set; } = new List<Post>();
    }
}
