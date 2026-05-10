using System.Globalization;
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;

namespace HIPA_BE.Email
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
    public class EmailValidator : ValidationAttribute
    {
        // https://learn.microsoft.com/en-us/dotnet/standard/base-types/how-to-verify-that-strings-are-in-valid-email-format
        protected override ValidationResult IsValid(object? obj, ValidationContext validationContext)
        {
            var email = obj as string;
            if (string.IsNullOrEmpty(email))
            {
                return ValidationResult.Success!;
            }
            try
            {
                // Normalize the domain
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Examines the domain part of the email and normalizes it.
                string DomainMapper(Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    var idn = new IdnMapping();

                    // Pull out and process domain name (throws ArgumentException on invalid)
                    string domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException)
            {
                return new ValidationResult(ErrorMessage ?? "Email validation failed.");
            }
            catch (ArgumentException)
            {
                return new ValidationResult(ErrorMessage ?? "Email validation failed.");
            }

            try
            {
                if (!Regex.IsMatch(email,
                @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)))
                {
                    return new ValidationResult(ErrorMessage ?? "Email validation failed.");
                }
            }
            catch (RegexMatchTimeoutException)
            {
                return new ValidationResult(ErrorMessage ?? "Email validation failed.");
            }
            
            // If no exception occurred and the email is valid, return success
            return ValidationResult.Success!;
        }
    }
}