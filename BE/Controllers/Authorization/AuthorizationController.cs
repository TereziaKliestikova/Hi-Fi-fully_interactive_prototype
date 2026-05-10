using HIPA_BE.Contracts;
using HIPA_BE.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace HIPA_BE.Controllers.Account
{
    [ApiController]
    [Route("auth")]
    public class AuthorizationController : ApiController
    {
        private readonly AuthorizationService _authorizationService;

        public AuthorizationController(AuthorizationService authorizationService)
        { 
            _authorizationService = authorizationService;
        }

        [HttpPost("refresh")]
        [Authorize]
        public async Task<IActionResult> Refresh(RefreshAccessTokenRequest refreshAccessTokenRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _authorizationService.RefreshAccessToken(refreshAccessTokenRequest.RefreshToken);

            if (response is null)
            {
                return BadRequest(new { Error = "Invalid refresh token" });
            }   
            return Ok(response);
        }
    }
}