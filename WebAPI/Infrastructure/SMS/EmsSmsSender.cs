using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace WebAPI.Infrastructure.SMS
{
    public class EmsSmsSender : ISmsSender
    {
        public async Task SendSms(string toNumber, string bodyMessage)
        {
            var accountSid = IocContainer.Configuration["SMS:AccountSID"];
            var authToken = IocContainer.Configuration["SMS:AuthToken"];
            var phoneNumber = IocContainer.Configuration["SMS:PhoneNumber"];

            TwilioClient.Init(accountSid, authToken);
            await MessageResource.CreateAsync(
                body: bodyMessage,
                from: new Twilio.Types.PhoneNumber(phoneNumber),
                to: new Twilio.Types.PhoneNumber(toNumber)
            );
        }
    }
}
