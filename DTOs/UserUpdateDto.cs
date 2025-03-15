using System.ComponentModel.DataAnnotations;

namespace API_FEB.DTOs
{
    public class UserUpdateDto
    {
        [Required, StringLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required, StringLength(50)]
        public string LastName { get; set; } = string.Empty;

        [Required, StringLength(20)]
        public string Role { get; set; } = "User";

        [Required, StringLength(100)]
        public string Country { get; set; } = string.Empty;

        [Required, StringLength(100)]
        public string City { get; set; } = string.Empty;

        [Required, StringLength(10)]
        public string Gender { get; set; } = string.Empty;

        [Required]
        [RegularExpression(@"^\+?\d{7,15}$", ErrorMessage = "Invalid phone number format.")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required, StringLength(100)]
        public string JobTitle { get; set; } = string.Empty;

        [Required, StringLength(20)]
        public string UserType { get; set; } = string.Empty;

        [StringLength(100)]
        public string? CompanyName { get; set; }

        [Required, Url]
        public string? WebsiteLink { get; set; }

        [StringLength(50)]
        public string? CompanySize { get; set; }
    }
}
