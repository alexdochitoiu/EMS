using System.IO;
using System.Threading.Tasks;
using WebAPI.Infrastructure.Email.Interfaces;
using WebAPI.Infrastructure.Email.Models;

namespace WebAPI.Infrastructure.Email
{
    public static class EmsEmailSender
    {
        public static async Task<SendEmailResponse> SendVerificationEmailAsync(
            string displayName, 
            string email,
            string verificationUrl,
            IEmailSender emailSender)
        {
            
            var htmlContent = File.ReadAllText("Infrastructure/Email/email_template.html");
            htmlContent = htmlContent
                .Replace("{ Website Link }", IocContainer.Configuration["CommonSettings:ClientURL"])
                .Replace("{ Content Heading }", "E-mail confirmation")
                .Replace("{ Content Body }",
                    $"Hi {displayName ?? "there"},<br><br>Thanks for creating an account with us. To continue, please verify your e-mail.")
                .Replace("{ Button Text }", "Verify e-mail")
                .Replace("{ Button Url }", verificationUrl)
                .Replace("{ Facebook Link }", IocContainer.Configuration["CommonSettings:Facebook"])
                .Replace("{ Twitter Link }", IocContainer.Configuration["CommonSettings:Twitter"])
                .Replace("{ Youtube Link }", IocContainer.Configuration["CommonSettings:Youtube"]);

            return await emailSender.SendEmailAsync(
                new EmailDetails
                {
                    FromName = IocContainer.Configuration["CommonSettings:FromName"],
                    FromEmail = IocContainer.Configuration["CommonSettings:FromEmail"],
                    Subject = "Welcome on EMS Platform! Confirm your e-mail",
                    Content = htmlContent,
                    IsHtml = true,
                    ToName = displayName,
                    ToEmail = email
                });
        }

        public static async Task<SendEmailResponse> SendPasswordResetEmailAsync(
            string displayName,
            string email,
            string resetPasswordUrl,
            IEmailSender emailSender)
        {
            var htmlContent = File.ReadAllText("Infrastructure/Email/email_template.html");
            htmlContent = htmlContent
                .Replace("{ Website Link }", IocContainer.Configuration["CommonSettings:ClientURL"])
                .Replace("{ Content Heading }", "Reset your password")
                .Replace("{ Content Body }",
                    $"Hello {displayName ?? "there"},<br><br>You made a request to change your EMS account password.")
                .Replace("{ Button Text }", "Reset password")
                .Replace("{ Button Url }", resetPasswordUrl)
                .Replace("{ Facebook Link }", IocContainer.Configuration["CommonSettings:Facebook"])
                .Replace("{ Twitter Link }", IocContainer.Configuration["CommonSettings:Twitter"])
                .Replace("{ Youtube Link }", IocContainer.Configuration["CommonSettings:Youtube"]);

            return await emailSender.SendEmailAsync(
                new EmailDetails
                {
                    FromName = IocContainer.Configuration["CommonSettings:FromName"],
                    FromEmail = IocContainer.Configuration["CommonSettings:FromEmail"],
                    Subject = "Reset password from your EMS account",
                    Content = htmlContent,
                    IsHtml = true,
                    ToName = displayName,
                    ToEmail = email
                });
        }
    }
}
