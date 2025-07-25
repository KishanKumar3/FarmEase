namespace FarmEase.Application.Services.Interface
{
    public interface IEmailService
    {
        Task<string> SendEmailAsync(string toEmail, string subject, string message);
    }
}
