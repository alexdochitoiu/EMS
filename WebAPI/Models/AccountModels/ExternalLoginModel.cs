namespace WebAPI.Models.AccountModels
{
    public class ExternalLoginModel
    {
        public string Provider { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhotoUrl { get; set; }
        public string Token { get; set; }
    }
}
