using System.Collections.Generic;

namespace WebAPI.Models.AccountModels
{
    public class LoginResultModel
    {
        public string Token { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public bool Succeeded { get; set; }
        public List<string> Errors { get; set; }
    }
}
