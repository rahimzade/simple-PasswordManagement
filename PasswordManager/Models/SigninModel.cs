using Microsoft.AspNetCore.Components;
using System.ComponentModel.DataAnnotations;

namespace PasswordManager.Models
{
    public class SigninModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Username Can not be empty")]
        public string UserName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Password Can not be empty")]
        public string Password { get; set; }
    }
}
