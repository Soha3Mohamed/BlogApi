using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace UserPostApi.Models.DTOs.User
{
    public class AddUserDTO
    {
        [Required]
        public string Name { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        [PasswordPropertyText]
        public string Password { get; set; }
    }
}
