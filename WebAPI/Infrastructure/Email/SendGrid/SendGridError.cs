namespace WebAPI.Infrastructure.Email.SendGrid
{
    public class SendGridError
    {
        public string Message { get; set; }

        public string Field { get; set; }

        public string Help { get; set; }
    }
}
