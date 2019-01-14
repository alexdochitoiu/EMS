namespace WebAPI.Models.AccountModels
{
    public class LoginCredentialsModel
    {
        public string EmailOrUsername { get; set; }
        public string Password { get; set; }

        public string CurrentLongitude { get; set; }
        public string CurrentLatitude { get; set; }
    }
}
