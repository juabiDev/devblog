namespace DevBlog.Entities
{
    public class Follow
    {
        public Guid FollowedId { get; set; }
        public User Followed { get; set; }
        public Guid FollowerId { get; set; }
        public User Follower { get; set; }
    }
}
