using System.ComponentModel.DataAnnotations;

namespace DevBlog.DTOs
{
    public class FollowRequest
    {
        [Required(ErrorMessage = "FollowerUserName is required")]
        public string FollowerUserName { get; set; }
        [Required(ErrorMessage = "FollowedUserName is required")]
        public string FollowedUserName { get; set; }
    }
}
