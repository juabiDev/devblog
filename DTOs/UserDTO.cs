using System.ComponentModel.DataAnnotations;

namespace DevBlog.DTOs
{
    public class UserDTO
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email")]
        public string Email { get; set; }
        public string? ProfilePhoto { get; set; }
        public string? About { get; set; }
    }
}
