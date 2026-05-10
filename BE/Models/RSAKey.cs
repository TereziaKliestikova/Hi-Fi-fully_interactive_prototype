using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace HIPA_BE.Models;

public class RSAKey
{
    public static RSAParameters ParseJsonToRsaParameters(string jsonContent)
    {
        // Parse JSON into RSAParameters
        try
        {
            var jwk = Newtonsoft.Json.JsonConvert.DeserializeObject<JwkModel>(jsonContent);
            return new RSAParameters
            {
                D = Base64UrlEncoder.DecodeBytes(jwk.D),
                DP = Base64UrlEncoder.DecodeBytes(jwk.DP),
                DQ = Base64UrlEncoder.DecodeBytes(jwk.DQ),
                Exponent = Base64UrlEncoder.DecodeBytes(jwk.E),
                InverseQ = Base64UrlEncoder.DecodeBytes(jwk.QI),
                Modulus = Base64UrlEncoder.DecodeBytes(jwk.N),
                P = Base64UrlEncoder.DecodeBytes(jwk.P),
                Q = Base64UrlEncoder.DecodeBytes(jwk.Q),
            };
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error parsing JSON: {ex.Message}");
            throw;
        }
    }

    // Define a simple model for JWK parsing
    class JwkModel
    {
        public required string Alg { get; set; }
        public required string D { get; set; }
        public required string DP { get; set; }
        public required string DQ { get; set; }
        public required string E { get; set; }
        public required string IQ { get; set; }
        public required string Kid { get; set; }
        public required string Kty { get; set; }
        public required string N { get; set; }
        public required string P { get; set; }
        public required string Q { get; set; }
        public required string QI { get; set; }
    }
}