using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using WebAPI.Infrastructure.Email.SendGrid;

namespace WebAPI.Infrastructure.Email
{
    public static class EmsEmailSender
    {
        public static async Task<SendEmailResponse> SendVerificationEmailAsync(
            string displayName, 
            string email,
            string verificationUrl) =>

            await IocContainer.ServiceProvider.GetService<IEmailSender>().SendEmailAsync(new SendGridEmailDetails
            {
                FromName = IocContainer.Configuration["CommonSettings:FromName"],
                FromEmail = IocContainer.Configuration["CommonSettings:FromEmail"],
                Content = "Template",
                IsHtml = true,
                ToName = displayName,
                ToEmail = email,
                Subject = "Welcome to EMS! Confirm your e-mail",
                HtmlTitle = "Verify Email",
                HtmlContent1 = $"Hi {displayName ?? "there"},",
                HtmlContent2 = "Thanks for creating an account with us.<br>To continue, please verify your e-mail.",
                HtmlButtonText = "Verify e-mail",
                HtmlButtonUrl = verificationUrl,
                HtmlFacebookLink = IocContainer.Configuration["CommonSettings:Facebook"],
                HtmlTwitterLink = IocContainer.Configuration["CommonSettings:Twitter"],
                HtmlYoutubeLink = IocContainer.Configuration["CommonSettings:YouTube"]
            });
    }
}
