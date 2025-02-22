namespace DevBlog.Entities
{
    public class Category
    {
        public Guid Id { get; set; }
        public string name { get; set; }
        public ICollection<Post> Posts { get; set; } = new List<Post>();
    }
}
