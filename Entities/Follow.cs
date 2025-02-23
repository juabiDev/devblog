namespace DevBlog.Entities
{
    public class Follow
    {
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid FollowerId { get; set; }
        public User Follower { get; set; }
    }
}
