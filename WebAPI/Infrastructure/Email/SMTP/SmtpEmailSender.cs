using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using WebAPI.Infrastructure.Email.Interfaces;
using WebAPI.Infrastructure.Email.Models;

namespace WebAPI.Infrastructure.Email.SMTP
{
    public class SmtpEmailSender : IEmailSender
    {
        public async Task<SendEmailResponse> SendEmailAsync(EmailDetails details)
        {
            var username = IocContainer.Configuration["Email:SMTP:Account"];
            var password = IocContainer.Configuration["Email:SMTP:Password"];
            var smtpClient = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential(username, password),
                EnableSsl = true
            };

            var from = new MailAddress(details.FromEmail, details.FromName);
            var to = new MailAddress(details.ToEmail, details.ToName);

            var mail = new MailMessage
            {
                From = from,
                Subject = details.Subject,
                SubjectEncoding = System.Text.Encoding.UTF8,
                IsBodyHtml = details.IsHtml,
                Body = details.Content,
                BodyEncoding = System.Text.Encoding.UTF8,
                Priority = MailPriority.High
            };
            mail.To.Add(to);

            try
            {
                await smtpClient.SendMailAsync(mail);
                return new SendEmailResponse();
            }
            catch (Exception ex)
            {
                return new SendEmailResponse { ErrorMessage = $"Error occured. Cause: { ex }" };
            }
        }
    }
}
