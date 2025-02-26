namespace DevBlog.DTOs
{
    public class CommentDTO
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
        public string AuthorName { get; set; }
        public string AuthorEmail { get; set; }
        public string AuthorLastName { get; set; }
        public string AuthorProfilePhoto { get; set; }
    }
}
