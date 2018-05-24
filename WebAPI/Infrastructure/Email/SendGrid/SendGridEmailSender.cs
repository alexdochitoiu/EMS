using System;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace WebAPI.Infrastructure.Email.SendGrid
{
    public class SendGridEmailSender : IEmailSender
    {
        public async Task<SendEmailResponse> SendEmailAsync(SendGridEmailDetails details)
        {
            var apiKey = IocContainer.Configuration["SendGrid:Key"];
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(details.FromEmail, details.FromName);
            var to = new EmailAddress(details.ToEmail, details.ToName);
            var subject = details.Subject;
            var msg = MailHelper.CreateSingleEmail(
                from, 
                to, 
                subject, 
                details.IsHtml ? null : details.Content,
                details.IsHtml ? details.Content : null);

            if (details.IsHtml)
            {
                msg.TemplateId = IocContainer.Configuration["SendGrid:TemplateId"];
                msg.AddSubstitution("{Title}", details.HtmlTitle);
                msg.AddSubstitution("{Content1}", details.HtmlContent1);
                msg.AddSubstitution("{Content2}", details.HtmlContent2);
                msg.AddSubstitution("{ButtonText}", details.HtmlButtonText);
                msg.AddSubstitution("{ButtonUrl}", details.HtmlButtonUrl);
                msg.AddSubstitution("{FacebookLink}", details.HtmlFacebookLink);
                msg.AddSubstitution("{TwitterLink}", details.HtmlTwitterLink);
                msg.AddSubstitution("{YoutubeLink}", details.HtmlYoutubeLink);
            }

            var result = await client.SendEmailAsync(msg);
            if (result.StatusCode == HttpStatusCode.Accepted)
                return new SendEmailResponse();

            try
            {
                var bodyResult = await result.Body.ReadAsStringAsync();
                var sendGridError = JsonConvert.DeserializeObject<SendGridError>(bodyResult);
                return new SendEmailResponse {ErrorMessage = sendGridError.Message};
            }
            catch (Exception)
            {
                return new SendEmailResponse {ErrorMessage = "Unknown error!"};
            }
        }
    }
}