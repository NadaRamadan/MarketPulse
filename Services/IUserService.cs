using API_FEB.Models;

namespace API_FEB.Services
{
    public interface IUserService
    {
        Task<User?> GetUserByEmailAsync(string email);
        Task<string> GenerateResetTokenAsync(User user);
    }
}
