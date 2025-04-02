using API_FEB.DTOs;
using API_FEB.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using API_FEB.Services;
using API_FEB.Enums;

public class AuthService
{
    private readonly UserManager<User> _userManager;
    private readonly IEmailService _emailService;

    public AuthService(UserManager<User> userManager, IEmailService emailService)
    {
        _userManager = userManager;
        _emailService = emailService;
    }

    // ✅ Register User
    public async Task<(bool Success, string Message, User? Data)> RegisterUser(UserCreateDto userCreateDto)
    {
        if (await _userManager.FindByEmailAsync(userCreateDto.Email) != null)
            return (false, "User with this email already exists.", null);

        var userType = userCreateDto.UserType; // ✅ Directly assign the enum


        var newUser = new User
        {
            FirstName = userCreateDto.FirstName,
            LastName = userCreateDto.LastName,
            Email = userCreateDto.Email.ToLower(),
            UserName = userCreateDto.Email.ToLower(),
            HasAcceptedTerms = userCreateDto.HasAcceptedTerms,
            Country = userCreateDto.Country,
            City = userCreateDto.City,
            Gender = userCreateDto.Gender,
            PhoneNumber = userCreateDto.PhoneNumber,
            JobTitle = userCreateDto.JobTitle,
            UserType = userType,  // ✅ Now properly assigned
            CompanyName = userCreateDto.CompanyName,
            WebsiteLink = userCreateDto.WebsiteLink,
            CompanySize = userCreateDto.CompanySize,
            IsOnboarded = false,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        var result = await _userManager.CreateAsync(newUser, userCreateDto.Password);
        if (!result.Succeeded)
            return (false, "User registration failed: " + string.Join(", ", result.Errors), null);

        return (true, "User registered successfully!", newUser);
    }

    // ✅ Login User
    public async Task<(bool Success, string Message, User? Data)> LoginUser(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email.ToLower());
        if (user == null || !await _userManager.CheckPasswordAsync(user, password))
            return (false, "Invalid email or password", null);

        return (true, "Login successful", user);
    }

    // ✅ Forgot Password - Generate Reset Token and Send Email
    public async Task<(bool Success, string Message)> ForgotPassword(string email)
    {
        var user = await _userManager.FindByEmailAsync(email.ToLower());
        if (user == null)
            return (false, "User not found");

        string resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
        user.ResetPasswordToken = resetToken;
        user.ResetTokenExpiry = DateTime.UtcNow.AddHours(1); // Token valid for 1 hour
        await _userManager.UpdateAsync(user);

        await _emailService.SendResetEmailAsync(user.Email, resetToken);

        return (true, "Password reset email sent");
    }

    // ✅ Reset Password
    public async Task<(bool Success, string Message)> ResetPassword(string email, string token, string newPassword)
    {
        var user = await _userManager.FindByEmailAsync(email.ToLower());
        if (user == null)
            return (false, "User not found");

        var resetResult = await _userManager.ResetPasswordAsync(user, token, newPassword);
        if (!resetResult.Succeeded)
            return (false, "Invalid or expired reset token");

        return (true, "Password reset successfully");
    }
}
