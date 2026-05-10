using System.ComponentModel.DataAnnotations;

namespace HIPA_BE.Contracts
{
    public record RefreshAccessTokenRequest(
        [Required(ErrorMessage = "api.error.logout.invalidForm")]
        string RefreshToken);

}