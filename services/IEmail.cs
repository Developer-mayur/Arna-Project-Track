using Arna_Project_Track.Models;

namespace Arna_Project_Track.services
{
    public interface IEmail
    {
        Task SendEmailAsync(EmailModel emailModel);
    }
}
