using System.Diagnostics;
using System.Web;
using HIPA_BE.Models;
using HIPA_BE.ServiceErrors;
using Microsoft.AspNetCore.Identity;
using ErrorOr;
using HIPA_BE.Enums;
using Microsoft.EntityFrameworkCore;


namespace HIPA_BE.Services
{
    public class ApplicationUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public ApplicationUserService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<bool> ConfirmEmail(string token, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return false;

            var result = await _userManager.ConfirmEmailAsync(user, token);
            return result.Succeeded;
        }

        public async Task<ErrorOr<IdentityResult>> RegisterUser(ApplicationUser user, string password)
        {
            var existingUser = await _userManager.FindByEmailAsync(user.Email);
            if (existingUser != null)
            {
                return Errors.ApplicationUser.UserAlreadyExists;
            }

            var result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded)
            {
                return Errors.ApplicationUser.InvalidRegistrationPassword;
            }

            // assign user role as Student
            var roleResult = await _userManager.AddToRoleAsync(user, Roles.Student.ToString());
            if (!roleResult.Succeeded)
            {
                // TODO: Here add the return of some meaningful error type (idk what yet)
                Debug.WriteLine("Failed to assign role to new user " + user.Email);
            }

            return result;
        }


        public async Task<ErrorOr<bool>> ValidateUser(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return Errors.ApplicationUser.UnauthorizedAccess;
            }

            var checkPassword = await _userManager.CheckPasswordAsync(user, password);
            if (!checkPassword)
            {
                return Errors.ApplicationUser.UnauthorizedAccess;
            }

            var emailConfirmed = await _userManager.IsEmailConfirmedAsync(user);
            if (!emailConfirmed)
            {
                return Errors.ApplicationUser.EmailNotVerified;
            }

            return true;
        }

        public async Task<ErrorOr<ApplicationUser>> FindUserByEmail(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return Errors.ApplicationUser.UserDoesNotExist;
            }

            return user;
        }

        public async Task<bool> IsUserConfirmed(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return user is { EmailConfirmed: true };
        }

        public async Task<ErrorOr<bool>> ResetPassword(string token, string password, string email)
        {
            // Find user by email
            // No check required because the user must exist in database and have valid email
            // to  receive the reset password token & link
            var user = await _userManager.FindByEmailAsync(email);

            var passwordValidator = _userManager.PasswordValidators.FirstOrDefault();
            var passwordValidationResult = await passwordValidator?.ValidateAsync(_userManager, user, password)!;
            if (!passwordValidationResult.Succeeded)
            {
                return Errors.ApplicationUser.InvalidResetPassword;
            }

            var result = await _userManager.ResetPasswordAsync(user, token, password);
            if (!result.Succeeded)
            {
                return Errors.ApplicationUser.InvalidResetPasswordToken;
            }

            return result.Succeeded;
        }
    }
}