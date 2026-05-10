using System.ComponentModel.DataAnnotations;
using HIPA_BE.Email;

namespace HIPA_BE.Contracts
{
    public record RegistrationRequest(
        [Required(ErrorMessage = "api.error.registration.invalidForm")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "api.error.registration.firstNameLength")]
        string FirstName,
        [Required(ErrorMessage = "api.error.registration.invalidForm")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "api.error.registration.lastNameLength")]
        string LastName,
        [Required(ErrorMessage = "api.error.registration.invalidForm")]
        // [EmailAddress(ErrorMessage = "api.error.registration.invalidForm")]
        [EmailValidator(ErrorMessage = "api.error.registration.invalidForm")]
        string Email,
        [Required(ErrorMessage = "api.error.registration.invalidForm")]
        string Password);
}