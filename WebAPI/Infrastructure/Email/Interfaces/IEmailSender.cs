using System.Threading.Tasks;
using WebAPI.Infrastructure.Email.Models;

namespace WebAPI.Infrastructure.Email.Interfaces
{
    public interface IEmailSender
    {
        Task<SendEmailResponse> SendEmailAsync(EmailDetails emailDetails);
    }
}
