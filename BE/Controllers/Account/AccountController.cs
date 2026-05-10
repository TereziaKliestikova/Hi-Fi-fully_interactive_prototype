using HIPA_BE.Contracts;
using HIPA_BE.Services;
using HIPA_BE.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using ErrorOr;
using HIPA_BE.Enums;
using Microsoft.AspNetCore.Authorization;
using log4net;
using System.Reflection;

namespace HIPA_BE.Controllers.Account
{
    [ApiController]
    [Route("account")]
    public class AccountController : ApiController
    {
        private readonly ApplicationUserService _applicationUserService;
        private readonly EmailSenderService _emailSenderService;
        private readonly AuthorizationService _authorizationService;

        public AccountController(ApplicationUserService applicationUserService, EmailSenderService emailSenderService,
            AuthorizationService authorizationService)
        {
            _applicationUserService = applicationUserService;
            _emailSenderService = emailSenderService;
            _authorizationService = authorizationService;
        }

        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod()?.DeclaringType ?? typeof(AccountController));

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegistrationRequest regUserRequest)
        {
            ErrorOr<ApplicationUser> getUser = ApplicationUser.From(regUserRequest);
            if (getUser.IsError)
            {
                return Problem(getUser.Errors);
            }

            var user = getUser.Value;
            ErrorOr<IdentityResult> userRegResult = await _applicationUserService.RegisterUser(user, regUserRequest.Password);
            if (userRegResult.IsError)
            {
                return Problem(userRegResult.Errors);
            }

            // TODO: Try to add token expiration time
            // Email confirmation
            var token = await _emailSenderService.GenerateEmailConfirmationToken(user);
            var ingressIsHttps = Environment.GetEnvironmentVariable("INGRESS_IS_HTTPS");
            var url = this.Url.Action(nameof(ConfirmEmail),
                                    "Account",
                                    new { token, email = user.Email },
                                    (ingressIsHttps != null && ingressIsHttps == "true" ? "https" : "http"));
            _emailSenderService.SendConfirmationEmail(url, user);

            return Ok(new { Message = "Registration successful!" });
        }

        // Controller for email confirmation
        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            var tokenValid = await _emailSenderService.IsTokenValid(token, email);
            if (!tokenValid)
            {
                return Redirect($"{Environment.GetEnvironmentVariable("FRONTEND_BASEURL")}/auth/login?emailVerificationStatus={EmailVerificationStatus.InvalidToken}");
            }

            var alreadyConfirmed = await _applicationUserService.IsUserConfirmed(email);
            if (alreadyConfirmed)
            {
                return Redirect($"{Environment.GetEnvironmentVariable("FRONTEND_BASEURL")}/auth/login?emailVerificationStatus={EmailVerificationStatus.AlreadyVerified}");
            }

            var emailConfirmed = await _applicationUserService.ConfirmEmail(token, email);
            if (emailConfirmed)
            {
                return Redirect($"{Environment.GetEnvironmentVariable("FRONTEND_BASEURL")}/auth/login?emailVerificationStatus={EmailVerificationStatus.Verified}");
            }

            return Redirect($"{Environment.GetEnvironmentVariable("FRONTEND_BASEURL")}/auth/login?emailVerificationStatus={EmailVerificationStatus.InvalidToken}");
        }

        [HttpPost("validate-user")]
        public async Task<IActionResult> ValidateUser(LoginRequest loginRequest)
        {
            ErrorOr<bool> userValidationResult = await _applicationUserService.ValidateUser(loginRequest.Email, loginRequest.Password);
            return userValidationResult.Match(
                result => Ok(new { Message = "User validated" }),
                errors => Problem(errors)
            );
        }

        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout(RefreshAccessTokenRequest refreshAccessTokenRequest)
        {
            try
            {
                await _authorizationService.RevokeRefreshToken(refreshAccessTokenRequest.RefreshToken);
            }
            catch(Exception e)
            {
                return BadRequest(new { Error = e.Message });
            }

            return Ok(new { Message = "Logout successful!" });
        }

        // TODO: Try to replace logic to the service
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordRequest forgotPasswordRequest)
        {
            var user = await _applicationUserService.FindUserByEmail(forgotPasswordRequest.Email);

            // send email if user exists
            if (user is { IsError: false, Value.EmailConfirmed: true })
            {
                var token = await _emailSenderService.GeneratePasswordResetToken(user.Value);

                var changePasswordUrl = $"{Environment.GetEnvironmentVariable("FRONTEND_BASEURL")}/auth/change-password/";
                var url = $"{changePasswordUrl}?token={Uri.EscapeDataString(token)}&email={Uri.EscapeDataString(user.Value.Email!)}";

                _emailSenderService.SendPasswordResetEmail(url, user.Value);
            }

            return Ok(new { Message = "Reset password request email sent" });
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest resetPasswordRequest)
        {
            var resetResult = await _applicationUserService.ResetPassword(resetPasswordRequest.Token,
                resetPasswordRequest.Password, resetPasswordRequest.Email);

            return resetResult.Match(
                result => Ok(new { Message = "Password reset successful!" }),
                errors => Problem(errors)
            );
        }
    }
}