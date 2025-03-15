using System.ComponentModel.DataAnnotations;

namespace API_FEB.Models
{
    public class Login
    {
        [Required, EmailAddress, StringLength(100)]
        public string Email { get; set; } = string.Empty;

        [Required, MinLength(6)]
        public string Password { get; set; } = string.Empty;
    }
}
