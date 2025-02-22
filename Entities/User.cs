namespace DevBlog.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ProfilePhoto { get; set; }
        public string About { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

    }
}
