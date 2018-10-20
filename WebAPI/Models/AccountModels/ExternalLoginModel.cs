namespace WebAPI.Models.AccountModels
{
    public class ExternalLoginModel
    {
        public string Provider { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailOrUsername { get; set; }
        public string PhotoUrl { get; set; }
    }
}
