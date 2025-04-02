namespace API_FEB.Services
{
    public interface IEmailService
    {
        Task SendResetEmailAsync(string email, string token);
    }
}
