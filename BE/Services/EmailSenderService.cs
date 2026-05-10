using HIPA_BE.Models;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Identity;
using MimeKit;
using MimeKit.Text;

namespace HIPA_BE.Services;

public class EmailSenderService
{
    private readonly UserManager<ApplicationUser> _userManager;


    public EmailSenderService(UserManager<ApplicationUser> userManager)
    { 
        _userManager = userManager;
    }
    
    public async Task<string> GenerateEmailConfirmationToken(ApplicationUser user)
    {
        return await _userManager.GenerateEmailConfirmationTokenAsync(user);
    }

    public async Task<string> GeneratePasswordResetToken(ApplicationUser user)
    {
        return await _userManager.GeneratePasswordResetTokenAsync(user);
    }
    
    public void SendConfirmationEmail(string url, ApplicationUser user)
    {
        var emailTemplate = File.ReadAllText("./Email/Templates/VerificationTemplate.html");
        
        emailTemplate = emailTemplate.Replace("{URL}", url);
        emailTemplate = emailTemplate.Replace("{FirstName}", user.FirstName);
        
        SendEmail(user, emailTemplate);
    }

    public void SendPasswordResetEmail(string url, ApplicationUser user)
    {
        var emailTemplate = File.ReadAllText("./Email/Templates/PasswordResetTemplate.html");
        
        emailTemplate = emailTemplate.Replace("{URL}", url);
        emailTemplate = emailTemplate.Replace("{FirstName}", user.FirstName);
        
        SendEmail(user, emailTemplate);
    }

    private void SendEmail(ApplicationUser user, string emailTemplate)
    {
        var emailMessage = new MimeMessage();
        emailMessage.From.Add(MailboxAddress.Parse(Environment.GetEnvironmentVariable("EMAIL_USERNAME")));
        emailMessage.To.Add(MailboxAddress.Parse(user.Email));
        emailMessage.Subject = "Email Confirmation: HIPA";
        emailMessage.Body = new TextPart(TextFormat.Html) { Text = emailTemplate };

        using var smtp = new SmtpClient();
        smtp.Connect(Environment.GetEnvironmentVariable("EMAIL_HOST"), int.Parse(Environment.GetEnvironmentVariable("EMAIL_PORT")), SecureSocketOptions.StartTls);
        smtp.Authenticate(Environment.GetEnvironmentVariable("EMAIL_USERNAME"),
            Environment.GetEnvironmentVariable("EMAIL_PASSWORD"));
        smtp.Send(emailMessage);
        smtp.Disconnect(true);
    }
    
    public async Task<bool> IsTokenValid(string token, string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        return user != null && await _userManager.VerifyUserTokenAsync(user, _userManager.Options.Tokens.EmailConfirmationTokenProvider, "EmailConfirmation", token);
    }
}