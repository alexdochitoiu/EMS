using System.Threading.Tasks;

namespace WebAPI.Infrastructure.Email.SendGrid
{
    public interface IEmailSender
    {
        Task<SendEmailResponse> SendEmailAsync(SendGridEmailDetails emailDetails);
    }
}
