using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Data.Core.Domain.Entities;
using Data.Core.Domain.Entities.Identity;
using Data.Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Infrastructure;
using WebAPI.Infrastructure.Email;
using WebAPI.Models.AccountModels;

namespace WebAPI.Controllers
{
    [Route("api/account")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;

        public AccountController(UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }
        
        [HttpPost("register", Name = "Register")]
        public async Task<IActionResult> RegisterAsync([FromBody]RegisterCredentialsModel registerCredentials)
        {
            const string invalidErrorMessage = "Please provide all required details to register for an account!";
            if (registerCredentials == null) return BadRequest(invalidErrorMessage);

            var country = await _unitOfWork.Countries.GetByNameAsync(registerCredentials.Country);
            var city = await _unitOfWork.Cities.GetByNameAsync(registerCredentials.City);
            var address = Address.Create(
                country, 
                city, 
                registerCredentials.Street, 
                registerCredentials.Number, 
                registerCredentials.ZipCode);

            var user = ApplicationUser.Create(
                registerCredentials.FirstName,
                registerCredentials.LastName,
                registerCredentials.Gender,
                registerCredentials.DateOfBirth,
                registerCredentials.Email,
                registerCredentials.Username,
                registerCredentials.Phone,
                address);

            var result = await _userManager.CreateAsync(user, registerCredentials.Password);

            if (!result.Succeeded) return BadRequest(result.Errors.ToList());

            var emailConfirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var confirmationUrl = 
                $"http://{Request.Host.Value}/api/account/verify/email/" +
                $"{HttpUtility.UrlEncode(user.Id.ToString())}/" +
                $"{HttpUtility.UrlEncode(emailConfirmationToken)}";
            await EmsEmailSender.SendVerificationEmailAsync(user.FirstName, user.Email, confirmationUrl);

            return Ok(new RegisterResultModel
            {
                FirstName = user.FirstName,
                Email = user.Email,
                LastName = user.LastName,
                Username = user.UserName,
                Token = user.GenerateJwtToken()
            });
        }

        [HttpGet("verify/email/{userId}/{emailToken}", Name = "VerifyEmail")]
        public async Task<IActionResult> VerifyEmailAsync(string userId, string emailToken)
        {
            emailToken = emailToken.Replace("%2f", "/").Replace("%2F", "/");

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return BadRequest("User not found");
            if (user.EmailConfirmed) return BadRequest("Email already verified");

            var result = await _userManager.ConfirmEmailAsync(user, emailToken);
            if (result.Succeeded) return Ok("Email verified");

            return BadRequest("Invalid email verification token");
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
