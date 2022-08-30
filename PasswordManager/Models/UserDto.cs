using PasswordManager.Entities;
using System.ComponentModel.DataAnnotations;

namespace PasswordManager.Models
{
    public class UserDto
    {
        [Required]
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Username Can not be empty")]
        public string UserName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Password Can not be empty")]
        public string Password { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Name Can not be empty")]
        public string Name { get; set; }

        public string? Family { get; set; }
        public AccountType UserType { get; set; }
    }
}
