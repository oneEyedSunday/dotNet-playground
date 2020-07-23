using System;
using System.Security.Cryptography;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace WithControllersAuthJWT
{
    public class JwtSettings
    {
        public string Issuer { get; }

        public string Audience { get; }

        public byte[] Key { get; }
        public JwtSettings(byte[] key, string issuer, string audience)
        {
            Key = key;
            Issuer = issuer;
            Audience = audience;
        }

        public TokenValidationParameters TokenValidationParameters => new TokenValidationParameters {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = Issuer,
            ValidAudience = Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Key)
        };

        public static JwtSettings FromConfiguration(IConfiguration config)
        {
            var issuer = config["jwt:issuer"] ?? "defaultissuer";
            var audience = config["jwt:audience"] ?? "defaultaudience";
            var base64Key = config["jwt:key"];

            byte[] key;
            if (!String.IsNullOrEmpty(base64Key))
            {
                key = Convert.FromBase64String(base64Key);
            }
            else {
                key = new byte[32];
                RandomNumberGenerator.Fill(key);
            }

            return new JwtSettings(key, issuer, audience);
        }
    }
}
