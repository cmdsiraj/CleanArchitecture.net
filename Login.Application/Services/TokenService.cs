using Login.Application.Dtos;
using Login.Application.IServices;
using Login.Domain.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Login.Application.Services
{
    internal class TokenService : ITokenService
    {
        private readonly JwtSettingsDTO _jwtSettings;

        public TokenService(IOptions<JwtSettingsDTO> jwtSettings)
        {
            this._jwtSettings = jwtSettings.Value;
        }
        public TokenDTO GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(_jwtSettings.Key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name,user.UserName)
                }),
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new TokenDTO {Token = tokenHandler.WriteToken(token) };
        }

        public TokenDTO GenerateLoginToken(string email, DateTime timestamp)
        {
            string secretKey = _jwtSettings.Key;
            string encodeData = $"{email}:{timestamp}";
            using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(secretKey)))
            {
                byte[] hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(encodeData));
                return new TokenDTO { Token = Convert.ToBase64String(hash) };
            }
        }
    }
}
