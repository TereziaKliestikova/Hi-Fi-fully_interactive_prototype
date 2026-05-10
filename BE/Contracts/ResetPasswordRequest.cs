using System.ComponentModel.DataAnnotations;

namespace HIPA_BE.Contracts
{
    public record ResetPasswordRequest(
        [Required(ErrorMessage = "api.error.changePassword.invalidForm")]
        string Token,
        [Required(ErrorMessage = "api.error.changePassword.invalidForm")]
        string Password,
        [Required(ErrorMessage = "api.error.changePassword.invalidForm")]
        string Email);
}