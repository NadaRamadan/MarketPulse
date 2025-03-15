namespace API_FEB.DTOs
{
    public class UserResponseDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = "User";
        public string Country { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string JobTitle { get; set; } = string.Empty;
        public string UserType { get; set; } = string.Empty;
        public string? CompanyName { get; set; }
        public string? WebsiteLink { get; set; }
        public string? CompanySize { get; set; }
        public bool IsOnboarded { get; set; }
    }
}
