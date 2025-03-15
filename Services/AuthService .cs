using API_FEB.Data;
using API_FEB.DTOs;
using API_FEB.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using BCrypt.Net;

public class AuthService
{
    private readonly ApplicationDbContext _context;

    public AuthService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<(bool Success, string Message, User? Data)> RegisterUser(UserCreateDto userCreateDto)
    {
        if (await _context.Users.AnyAsync(u => u.Email == userCreateDto.Email))
            return (false, "User with this email already exists.", null);

        var newUser = new User
        {
            FirstName = userCreateDto.FirstName,
            LastName = userCreateDto.LastName,
            Email = userCreateDto.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(userCreateDto.Password),
            Role = "User",
            HasAcceptedTerms = userCreateDto.HasAcceptedTerms,
            Country = userCreateDto.Country,
            City = userCreateDto.City,
            Gender = userCreateDto.Gender,
            PhoneNumber = userCreateDto.PhoneNumber,
            JobTitle = userCreateDto.JobTitle,
            UserType = userCreateDto.UserType,
            CompanyName = userCreateDto.CompanyName,
            WebsiteLink = userCreateDto.WebsiteLink,
            CompanySize = userCreateDto.CompanySize,
            IsOnboarded = false,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _context.Users.Add(newUser);
        await _context.SaveChangesAsync();

        return (true, "User registered successfully!", newUser);
    }

    public async Task<(bool Success, string Message, User? Data)> LoginUser(string email, string password)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email.ToLower());
        if (user == null)
            return (false, "Invalid email or password", null);

        if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            return (false, "Invalid email or password", null);

        return (true, "Login successful", user);
    }
}
