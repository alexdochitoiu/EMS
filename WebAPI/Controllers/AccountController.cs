using System.Linq;
using System.Threading.Tasks;
using Data.Core.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Infrastructure;
using WebAPI.Models.AccountModels;

namespace WebAPI.Controllers
{
    [Route("api/account")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        
        [HttpPost("register", Name = "Register")]
        public async Task<IActionResult> RegisterAsync([FromBody]RegisterCredentialsModel registerCredentials)
        {
            const string invalidErrorMessage = "Please provide all required details to register for an account!";
            if (registerCredentials == null) return BadRequest(invalidErrorMessage);

            var user = ApplicationUser.Create(
                registerCredentials.FirstName,
                registerCredentials.LastName,
                registerCredentials.Gender,
                registerCredentials.DateOfBirth,
                registerCredentials.Email,
                registerCredentials.Username,
                registerCredentials.Phone,
                registerCredentials.Address);

            var result = await _userManager.CreateAsync(user, registerCredentials.Password);

            if (!result.Succeeded) return BadRequest(result.Errors.ToList());

            var emailConfirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            // TODO: Email verification code
            // ...

            return Ok(new RegisterResultModel
            {
                FirstName = user.FirstName,
                Email = user.Email,
                LastName = user.LastName,
                Username = user.UserName,
                Token = user.GenerateJwtToken()
            });
        }

        [HttpPost("login", Name="Login")]
        public async Task<IActionResult> LoginAsync([FromBody]LoginCredentialsModel loginCredentials)
        {
            var invalidErrorMessage = "Invalid username or password";
            if (string.IsNullOrWhiteSpace(loginCredentials?.EmailOrUsername))
                return BadRequest(invalidErrorMessage);

            var isEmail = loginCredentials.EmailOrUsername.Contains("@");

            var user = isEmail ?
                await _userManager.FindByEmailAsync(loginCredentials.EmailOrUsername) :
                await _userManager.FindByNameAsync(loginCredentials.EmailOrUsername);
            if (user == null)
                return BadRequest(invalidErrorMessage);

            var isValidPassword = await _userManager.CheckPasswordAsync(user, loginCredentials.Password);

            if (!isValidPassword)
                return BadRequest(invalidErrorMessage);

            return Ok(new LoginResultModel
            {
                FirstName = user.FirstName,
                Email = user.Email,
                LastName = user.LastName,
                Username = user.UserName,
                Token = user.GenerateJwtToken()
            });
        }
    }
}
