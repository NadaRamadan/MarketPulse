using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using API_FEB.Enums;
using Microsoft.AspNetCore.Identity;

namespace API_FEB.Models
{
    public class User : IdentityUser
    {
        [Required, StringLength(50)]
        public string FirstName { get; set; }

        [Required, StringLength(50)]
        public string LastName { get; set; }

        public bool HasAcceptedTerms { get; set; }

        [StringLength(100)]
        public string Country { get; set; }

        [StringLength(100)]
        public string City { get; set; }

        [StringLength(10)]
        public string Gender { get; set; }

        public string? CompanyName { get; set; }
        public string? WebsiteLink { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))] // ✅ Allows string values in API
        public CompanySizeEnum CompanySize { get; set; }

        [Required]
        public string Role { get; set; } = "User";

        public bool IsOnboarded { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public string? ResetPasswordToken { get; set; }
        public DateTime? ResetTokenExpiry { get; set; }

        [StringLength(100)]
        public string? JobTitle { get; set; }

        [Phone]
        public override string? PhoneNumber { get; set; }

        [Required]
        [JsonConverter(typeof(JsonStringEnumConverter))] // ✅ This is correct, no need for StringLength
        public UserTypeEnum UserType { get; set; } // ❌ Remove any [StringLength] here
    }
}
