using System.Threading.Tasks;

namespace WebAPI.Infrastructure.SMS
{
    public interface ISmsSender
    {
        Task SendSms(string toNumber, string bodyMessage);
    }
}
