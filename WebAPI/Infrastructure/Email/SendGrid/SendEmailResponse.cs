namespace WebAPI.Infrastructure.Email.SendGrid
{
    public class SendEmailResponse
    {
        public bool Succeeded => string.IsNullOrEmpty(ErrorMessage);
        public string ErrorMessage { get; set; }
    }
}
