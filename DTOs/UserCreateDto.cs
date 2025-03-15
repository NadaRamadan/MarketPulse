using System.ComponentModel.DataAnnotations;

namespace API_FEB.DTOs
{
    public class UserCreateDto
    {
        [Required, StringLength(50)]
        public string FirstName { get; set; }

        [Required, StringLength(50)]
        public string LastName { get; set; }

        [Required, EmailAddress, StringLength(100)]
        public string Email { get; set; }

        [Required, MinLength(6)]
        public string Password { get; set; }

        [Required]
        public bool HasAcceptedTerms { get; set; } // ✅ Add this field

        [Required, StringLength(100)]
        public string Country { get; set; }

        [Required, StringLength(100)]
        public string City { get; set; }

        [Required, StringLength(10)]
        public string Gender { get; set; }

        [Required]
        [RegularExpression(@"^\+?\d{7,15}$", ErrorMessage = "Invalid phone number format.")]
        public string PhoneNumber { get; set; }

        [Required, StringLength(100)]
        public string JobTitle { get; set; }

        [Required, StringLength(20)]
        public string UserType { get; set; } // "Marketer", "Freelancer", "Brand Owner"

        public string? CompanyName { get; set; }
        public string? WebsiteLink { get; set; }
        public string? CompanySize { get; set; }
        [Required] 
        public string Role { get; set; } = "User"; // Default value if not provided

    }

}
