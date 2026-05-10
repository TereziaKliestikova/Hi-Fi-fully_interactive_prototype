using ErrorOr;
using HIPA_BE.ServiceErrors;
using Duende.IdentityModel.Client;
using Duende.IdentityServer.EntityFramework.Entities;
using Duende.IdentityServer.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace HIPA_BE.Services
{
    public class AuthorizationService
    {
        private readonly HttpClient _httpClient;

        public AuthorizationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public class TokenResponse
        {
            [JsonProperty("access_token")]
            public string? AccessToken { get; set; }

            [JsonProperty("expires_in")]
            public int ExpiresIn { get; set; }

            [JsonProperty("token_type")]
            public string TokenType { get; set; } = "Bearer";

            [JsonProperty("refresh_token")]
            public string? RefreshToken { get; set; }

            [JsonProperty("scope")]
            public string? Scope { get; set; }

            [JsonProperty("id_token")]
            public string? IdToken { get; set; }
        }

        public async Task<ErrorOr<TokenResponse>> AuthenticateUserAndReturnTokens(string email, string password)
        {
            var requestBody = new Dictionary<string, string>
            {
                ["grant_type"] = "password",
                ["username"] = email,
                ["password"] = password,
                ["scope"] = "openid profile email offline_access",
                ["client_id"] = Environment.GetEnvironmentVariable("CLIENT_ID_DEVELOPMENT"),
                ["client_secret"] = Environment.GetEnvironmentVariable("CLIENT_SECRET_DEVELOPMENT")
            };

            var request = new HttpRequestMessage(HttpMethod.Post, $"{Environment.GetEnvironmentVariable("ASPNETCORE_HTTPS_HOST")}:{Environment.GetEnvironmentVariable("ASPNETCORE_HTTPS_PORT")}/connect/token")
            {
                Content = new FormUrlEncodedContent(requestBody)
            };

            var response = await _httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                return Errors.ApplicationUser.UnauthorizedAccess;
            }

            var responseContent = await response.Content.ReadAsStringAsync();
            var tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(responseContent);
            return tokenResponse;

        }

        public async Task<TokenResponse> RefreshAccessToken(string refreshToken)
        {
            var requestBody = new Dictionary<string, string>
            {
                ["grant_type"] = "refresh_token",
                ["refresh_token"] = refreshToken,
                ["client_id"] = Environment.GetEnvironmentVariable("CLIENT_ID_DEVELOPMENT"),
                ["client_secret"] = Environment.GetEnvironmentVariable("CLIENT_SECRET_DEVELOPMENT")
            };

            var request = new HttpRequestMessage(HttpMethod.Post, $"{Environment.GetEnvironmentVariable("ASPNETCORE_HTTPS_HOST")}:{Environment.GetEnvironmentVariable("ASPNETCORE_HTTPS_PORT")}/connect/token")
            {
                Content = new FormUrlEncodedContent(requestBody)
            };

            var response = await _httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Authentication failed");
            }

            var responseContent = await response.Content.ReadAsStringAsync();
            var tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(responseContent);
            return tokenResponse;
        }

        public async Task RevokeRefreshToken(string refreshToken)
        {
            var requestBody = new Dictionary<string, string>
            {
                ["token"] = refreshToken,
                ["token_type_hint"] = "refresh_token",
                ["client_id"] = Environment.GetEnvironmentVariable("CLIENT_ID_DEVELOPMENT"),
                ["client_secret"] = Environment.GetEnvironmentVariable("CLIENT_SECRET_DEVELOPMENT")
            };

            var request = new HttpRequestMessage(HttpMethod.Post, $"{Environment.GetEnvironmentVariable("ASPNETCORE_HTTPS_HOST")}:{Environment.GetEnvironmentVariable("ASPNETCORE_HTTPS_PORT")}/connect/revocation")
            {
                Content = new FormUrlEncodedContent(requestBody)
            };

            var response = await _httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                   throw new Exception("Revocation failed");
            }
        }

        // public async ErrorOr<bool> ForgotPassword(string email)
        // {
        //     var user = await _userManager.FindByEmailAsync(email);
        //     return true;
        // }
    }
}