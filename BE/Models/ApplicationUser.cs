using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using ErrorOr;
using HIPA_BE.Contracts;

namespace HIPA_BE.Models
{
    public class ApplicationUser : IdentityUser
    {
        public const int MinNameLength = 2;
        public const int MaxNameLength = 50;

        [Required]
        [StringLength(MaxNameLength)]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        [StringLength(MaxNameLength)]
        public string LastName { get; set; } = string.Empty;
        
        private static ErrorOr<ApplicationUser> Create(
            string firstName,
            string lastName,
            string email)
        {

            return new ApplicationUser
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    UserName = email // use email as username for now
                };
        }

        public static ErrorOr<ApplicationUser> From(RegistrationRequest request)
        {
            return Create(
                request.FirstName,
                request.LastName,
                request.Email);
        }
    }    
}