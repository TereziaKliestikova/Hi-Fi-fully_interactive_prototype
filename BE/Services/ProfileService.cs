using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using HIPA_BE.Models;
using MailKit.Search;

public class ProfileService : IProfileService
{
    protected UserManager<ApplicationUser> _userManager;

    public ProfileService(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }
    
    public async Task GetProfileDataAsync(ProfileDataRequestContext context)
    {
        var user = await _userManager.GetUserAsync(context.Subject);
        
        if (user != null)
        {
            var roles = await _userManager.GetRolesAsync(user);

            var claims = roles.Select(role => new Claim("user_role", role)).ToList();
            var uId=await _userManager.GetUserIdAsync(user);
            // Add the role claims to the issued claims
            context.IssuedClaims.AddRange(claims);
           
        }
    
    }

    public async Task IsActiveAsync(IsActiveContext context)
    {
        var user = await _userManager.GetUserAsync(context.Subject);

        context.IsActive = (user != null);
    }
}