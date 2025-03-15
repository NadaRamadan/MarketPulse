using System;
using System.ComponentModel.DataAnnotations;

namespace API_FEB.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required, StringLength(50)]
        public string LastName { get; set; } = string.Empty;

        [Required, EmailAddress, StringLength(100)]
        public string Email { get; set; } = string.Empty; // Normalize to lowercase before storing

        [Required]
        public string PasswordHash { get; set; } = string.Empty; // Store hashed password only

        [Required, StringLength(20)]
        public string Role { get; set; } = "User"; // Default: "User" or "Admin"

        [Required]
        public bool HasAcceptedTerms { get; set; }

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
        public string UserType { get; set; } = string.Empty; // "Marketer", "Freelancer", "Brand Owner"

        // Fields applicable only to "Brand Owner"
        [StringLength(100)]
        public string? CompanyName { get; set; }

        [StringLength(255)]
        public string? WebsiteLink { get; set; }

        [StringLength(50)]
        public string? CompanySize { get; set; }

        public bool IsOnboarded { get; set; } = false; // Track if user has completed onboarding

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
