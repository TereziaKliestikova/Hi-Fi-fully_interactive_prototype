using System.ComponentModel.DataAnnotations;

namespace HIPA_BE.Contracts
{
    public record LoginRequest(
        [Required(ErrorMessage = "api.error.login.invalidForm")]
        string Email,
        [Required(ErrorMessage = "api.error.login.invalidForm")]
        string Password);

}

