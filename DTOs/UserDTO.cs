using System.ComponentModel.DataAnnotations;

namespace DevBlog.DTOs
{
    public class UserDTO
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Username is required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email")]
        public string Email { get; set; }
        public string? ProfilePhoto { get; set; }
        public string? About { get; set; }
    }
}
