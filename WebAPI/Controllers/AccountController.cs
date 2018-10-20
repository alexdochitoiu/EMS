using System.Collections.Generic;
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
using WebAPI.Infrastructure.Email.SendGrid;
using WebAPI.Models.AccountModels;

namespace WebAPI.Controllers
{
    [Route("api/account")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailSender _emailSender;

        public AccountController(
            UserManager<ApplicationUser> userManager, 
            IUnitOfWork unitOfWork, 
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _emailSender = emailSender;
        }

        [HttpPost("register", Name = "Register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterCredentialsModel registerCredentials)
        {
            var invalidErrorMessage = "Please provide all required details to register for an account!";
            if (registerCredentials == null)
                return BadRequest(new RegisterResultModel
                {
                    Errors = new List<string>(new[] {invalidErrorMessage})
                });
            
            invalidErrorMessage = "Your password and confirmation password do not match.";
            if (registerCredentials.Password != registerCredentials.ConfirmPassword)
                return BadRequest(new RegisterResultModel
                {
                    Errors = new List<string>(new[] { invalidErrorMessage })
                });

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

            if (!result.Succeeded)
                return BadRequest(new RegisterResultModel
                {
                    Errors = new List<string>(result.Errors.Select(err => err.Description))
                });

            var emailConfirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var confirmationUrl =
                $"http://{Request.Host.Value}/api/account/verify/email/" +
                $"{HttpUtility.UrlEncode(user.Id.ToString())}/" +
                $"{HttpUtility.UrlEncode(emailConfirmationToken)}";

            var sendEmailResponse = await EmsEmailSender.SendVerificationEmailAsync(user.FirstName, user.Email, confirmationUrl, _emailSender);

            return Ok(new RegisterResultModel
            {
                Token = user.GenerateJwtToken(),
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.UserName,
                Email = user.Email,
                Errors = null,
                Warnings = !sendEmailResponse.Succeeded ?
                        new List<string>(
                            new[] {$"Cannot send email verification!\nCause: { sendEmailResponse.ErrorMessage }"}) :
                        null
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
            return result.Succeeded ? 
                    Ok("Email verified") 
                    : (IActionResult) BadRequest("Invalid email verification token");
        }

        [HttpPost("login", Name="Login")]
        public async Task<IActionResult> LoginAsync([FromBody]LoginCredentialsModel loginCredentials)
        {
            const string invalidErrorMessage = "Invalid username or password";
            if (string.IsNullOrWhiteSpace(loginCredentials?.EmailOrUsername))
                return BadRequest(invalidErrorMessage);

            var isEmail = loginCredentials.EmailOrUsername.Contains("@");

            var user = isEmail ?
                await _userManager.FindByEmailAsync(loginCredentials.EmailOrUsername) :
                await _userManager.FindByNameAsync(loginCredentials.EmailOrUsername);
            if (user == null)
                return BadRequest(new LoginResultModel
                {
                    Errors = new List<string>(new[] { invalidErrorMessage })
                });

            var isValidPassword = await _userManager.CheckPasswordAsync(user, loginCredentials.Password);

            return !isValidPassword
                ? BadRequest(new LoginResultModel
                {
                    Errors = new List<string>(new[] { invalidErrorMessage })
                })
                : (IActionResult) Ok(new LoginResultModel
                {
                    FirstName = user.FirstName,
                    Email = user.Email,
                    LastName = user.LastName,
                    Username = user.UserName,
                    Token = user.GenerateJwtToken()
                });
        }

        [HttpPost("login/external", Name = "ExternalLogin")]
        public async Task<IActionResult> ExternalLoginAsync([FromBody]ExternalLoginModel externalLoginModel)
        {
            var isEmail = externalLoginModel.EmailOrUsername.Contains("@");

            var user = isEmail ?
                await _userManager.FindByEmailAsync(externalLoginModel.EmailOrUsername) :
                await _userManager.FindByNameAsync(externalLoginModel.EmailOrUsername);

            var errors = user == null ? new List<string>(new[] {"User not registered"}) : null;

            return Ok(new LoginResultModel
            {
                Email = isEmail ? externalLoginModel.EmailOrUsername : null,
                FirstName = externalLoginModel.FirstName,
                LastName = externalLoginModel.LastName,
                Username = !isEmail ? externalLoginModel.EmailOrUsername : null,
                Errors = errors
            });
        }
    }
}
