using System.ComponentModel.DataAnnotations;
using HIPA_BE.Email;

namespace HIPA_BE.Contracts
{
    public record ForgotPasswordRequest(
        [Required(ErrorMessage = "api.error.requestChangePassword.invalidForm")]
        [EmailValidator(ErrorMessage = "api.error.requestChangePassword.invalidForm")]
        string Email);
}