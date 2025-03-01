using System.ComponentModel.DataAnnotations;

namespace API_FEB.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string? FirstName { get; set; }

        [Required, StringLength(50)]
        public string? LastName { get; set; }

        [Required, EmailAddress]
        public string? Email { get; set; }

        [Required]
        public string? PasswordHash { get; set; }

        [Required]
        public string Role { get; set; } = "User"; // Default: User or Admin

        [Required]
        public bool HasAcceptedTerms { get; set; }

        [Required, StringLength(100)]
        public string Country { get; set; } = string.Empty;

        [Required, StringLength(100)]
        public string City { get; set; } = string.Empty;

        [Required, StringLength(10)]
        public string Gender { get; set; } = string.Empty;

        [Required, Phone]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required, StringLength(100)]
        public string JobTitle { get; set; } = string.Empty;

        [Required, StringLength(20)]
        public string UserType { get; set; } = string.Empty; // "Marketer", "Freelancer", or "Brand Owner"

        public string? CompanyName { get; set; } // Only for Brand Owner
        public string? WebsiteLink { get; set; } // Only for Brand Owner
        public string? CompanySize { get; set; } // Only for Brand Owner
            public bool IsOnboarded { get; set; } = false; // New field to track onboarding

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
