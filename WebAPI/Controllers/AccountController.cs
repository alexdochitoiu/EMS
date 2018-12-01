using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Data.Core.Domain;
using Data.Core.Domain.Entities;
using Data.Core.Domain.Entities.Identity;
using Data.Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebAPI.Infrastructure;
using WebAPI.Infrastructure.Email;
using WebAPI.Infrastructure.Email.Interfaces;
using WebAPI.Models.AccountModels;
using ExternalLoginModel = WebAPI.Models.AccountModels.ExternalLoginModel;
using ForgotPasswordModel = WebAPI.Models.AccountModels.ForgotPasswordModel;

namespace WebAPI.Controllers
{
    [Route("api/account")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailSender _emailSender;
        private readonly ILogger _logger;

        public AccountController(
            UserManager<ApplicationUser> userManager, 
            IUnitOfWork unitOfWork, 
            IEmailSender emailSender,
            ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _emailSender = emailSender;
            _logger = logger;
        }

        [HttpPost("register", Name = "Register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterCredentialsModel registerCredentials)
        {
            _logger.LogInformation($"A new user is registering... Info: {registerCredentials}");
            var invalidErrorMessage = "Please provide all required details to register for an account!";
            if (registerCredentials == null)
            {
                return BadRequest(new RegisterResultModel
                {
                    Errors = new List<string>(new[] { invalidErrorMessage })
                });
            }

            invalidErrorMessage = "Your password and confirmation password do not match.";
            if (registerCredentials.Password != registerCredentials.ConfirmPassword)
            {
                return BadRequest(new RegisterResultModel
                {
                    Errors = new List<string>(new[] { invalidErrorMessage })
                });
            }

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
            {
                return BadRequest(new RegisterResultModel
                {
                    Errors = new List<string>(result.Errors.Select(err => err.Description))
                });
            }

            var emailConfirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var clientUrl = IocContainer.Configuration["CommonSettings:ClientURL"];
            var confirmationUrl =
                $"{clientUrl}/verify/email/" +
                $"{HttpUtility.UrlEncode(user.Id.ToString())}/" +
                $"{HttpUtility.UrlEncode(emailConfirmationToken)}";
            
            var sendEmailResponse = await EmsEmailSender.SendVerificationEmailAsync(user.UserName, user.Email, confirmationUrl, _emailSender);

            return Ok(new RegisterResultModel
            {
                Token = user.GenerateJwtToken(),
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.UserName,
                Email = user.Email,
                Errors = null,
                Warnings = !sendEmailResponse.Succeeded 
                        ? new List<string>(
                            new[] {$"Cannot send email verification!\nCause: { sendEmailResponse.ErrorMessage }"}) 
                        : null
            });
        }

        [HttpPost("resend-verification-mail", Name = "ResendVerificationMail")]
        public async Task<IActionResult> ResendVerificationMailAsync([FromBody] ForgotPasswordModel model)
        {
            var isEmail = model.EmailOrUsername.Contains('@');

            var user = isEmail
                ? await _userManager.FindByEmailAsync(model.EmailOrUsername)
                : await _userManager.FindByNameAsync(model.EmailOrUsername);

            if (user == null)
            {
                return BadRequest("Account not registered with this e-mail");
            }

            var emailConfirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var clientUrl = IocContainer.Configuration["CommonSettings:ClientURL"];
            var confirmationUrl =
                $"{clientUrl}/verify/email/" +
                $"{HttpUtility.UrlEncode(user.Id.ToString())}/" +
                $"{HttpUtility.UrlEncode(emailConfirmationToken)}";

            var sendEmailResponse = await EmsEmailSender.SendVerificationEmailAsync(user.UserName, user.Email, confirmationUrl, _emailSender);

            return sendEmailResponse.Succeeded ?
                   (IActionResult) Ok(sendEmailResponse) :
                   BadRequest(sendEmailResponse);
        }

        [HttpGet("verify/email/{userId}/{emailToken}", Name = "VerifyEmail")]
        public async Task<IActionResult> VerifyEmailAsync(string userId, string emailToken)
        {
            if (!Guid.TryParse(userId, out _))
            {
                return BadRequest("Something went wrong. Invalid user id!");
            }

            emailToken = emailToken.Replace("%2f", "/").Replace("%2F", "/");

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return BadRequest("Something went wrong. User not found!");
            }

            if (user.EmailConfirmed)
            {
                return BadRequest("The e-mail was already verified!");
            }

            var result = await _userManager.ConfirmEmailAsync(user, emailToken);
            return result.Succeeded
                    ? Ok("E-mail was successfully verified!")
                    : (IActionResult)BadRequest("Invalid email verification token!");
        }

        [HttpPost("forgot-password", Name = "ForgotPassword")]
        public async Task<IActionResult> ForgotPasswordAsync([FromBody] ForgotPasswordModel model)
        {
            var isEmail = model.EmailOrUsername.Contains('@');

            var user = isEmail
                ? await _userManager.FindByEmailAsync(model.EmailOrUsername)
                : await _userManager.FindByNameAsync(model.EmailOrUsername);

            if (user == null)
            {
                return BadRequest("Account not registered with this e-mail");
            }

            var resetPasswordToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            var clientUrl = IocContainer.Configuration["CommonSettings:ClientURL"];
            var resetPasswordUrl =
                $"{clientUrl}/reset/password/" +
                $"{HttpUtility.UrlEncode(user.Id.ToString())}/" +
                $"{HttpUtility.UrlEncode(resetPasswordToken)}";

            var sendEmailResponse = await EmsEmailSender.SendPasswordResetEmailAsync(user.UserName, user.Email, resetPasswordUrl, _emailSender);
            return sendEmailResponse.Succeeded
                ? (IActionResult) Ok(sendEmailResponse)
                : BadRequest($"{ sendEmailResponse.ErrorMessage }");
        }

        [HttpPost("reset/password/{userId}/{resetPasswordToken}", Name = "ResetPassword")]
        public async Task<IActionResult> ResetPasswordAsync(string userId, string resetPasswordToken, [FromBody] ResetPasswordModel model)
        {
            if (!Guid.TryParse(userId, out _))
            {
                return BadRequest("Something went wrong. Invalid user id!");
            }

            resetPasswordToken = resetPasswordToken.Replace("%2f", "/").Replace("%2F", "/");

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return BadRequest("User not found");

            var error = new IdentityError
            {
                Description = "Your password and confirmation password do not match."
            };
            if (model.Password != model.ConfirmPassword)
            {
                return BadRequest(error);
            }

            var response = await _userManager.ResetPasswordAsync(user, resetPasswordToken, model.Password);
            return response.Succeeded 
                ? (IActionResult) Ok(response) 
                : BadRequest(response);
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
                return BadRequest(new LoginResultModel
                {
                    Errors = new List<string>(new[] { invalidErrorMessage })
                });

            if (!user.EmailConfirmed)
            {
                invalidErrorMessage = "E-mail was not confirmed";
                return BadRequest(new LoginResultModel
                {
                    Errors = new List<string>(new[] {invalidErrorMessage})
                });
            }

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
        public async Task<IActionResult> ExternalLoginAsync([FromBody] ExternalLoginModel externalLoginModel)
        {
            // Verify if user already exists in database
            var user = await _userManager.FindByEmailAsync(externalLoginModel.Email);            
            var email = externalLoginModel.Email;
            var username = email.Substring(0, email.IndexOf('@'));
            if (user == null)
            {
                // User not registered. Save info to database
                user = ApplicationUser.Create(
                    externalLoginModel.FirstName,
                    externalLoginModel.LastName,
                    GenderEnum.Other,
                    DateTime.MinValue,
                    email,
                    username,
                    null,
                    null);

                user.EmailConfirmed = true;

                var result = await _userManager.CreateAsync(user);


                var ut = new UserToken
                {
                    UserId = user.Id,
                    LoginProvider = externalLoginModel.Provider,
                    Name = username,
                    Value = externalLoginModel.Token
                };

                await _unitOfWork.Users.AddUserTokenAsync(ut);
                var tokenResult = await _unitOfWork.CompleteAsync();

                return !result.Succeeded && tokenResult == 0
                    ? BadRequest(new LoginResultModel
                    {
                        Errors = new List<string>(result.Errors.Select(err => err.Description))
                    })
                    : (IActionResult) Ok(new LoginResultModel
                    {
                        UserId = user.Id.ToString(),
                        Token = externalLoginModel.Token,
                        Email = email,
                        FirstName = externalLoginModel.FirstName,
                        LastName = externalLoginModel.LastName,
                        Username = username,
                        Errors = null
                    });
            }

            var userToken = await _unitOfWork.Users.GetUserTokenAsync(user.Id);
            if (userToken != null && externalLoginModel.Provider == userToken.LoginProvider)
            {

                return Ok(new LoginResultModel
                {
                    UserId = user.Id.ToString(),
                    Token = externalLoginModel.Token,
                    Email = email,
                    FirstName = externalLoginModel.FirstName,
                    LastName = externalLoginModel.LastName,
                    Username = username,
                    Errors = null
                });
            }

            var errors = userToken == null
                ? new List<string>(new[] { "This e-mail was already registered using our signing up form." })
                : new List<string>(new[] { $"This e-mail is already signed up by a { userToken.LoginProvider } account." });

            return BadRequest(new LoginResultModel
            {
                Email = email,
                FirstName = externalLoginModel.FirstName,
                LastName = externalLoginModel.LastName,
                Username = username,
                Errors = errors
            });
        }
    }
}
