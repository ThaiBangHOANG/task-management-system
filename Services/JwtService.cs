using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskManagementSystem.API.Models;

namespace TaskManagementSystem.API.Services
{
    public class JwtService
    {
        private readonly IConfiguration _configuration;

        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(User user)
        {
            var key =
                _configuration["JwtSettings:Key"];

            var securityKey =
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(key)
                );

            var credentials =
                new SigningCredentials(
                    securityKey,
                    SecurityAlgorithms.HmacSha256
                );

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Email,user.Email)
            };

            var token =
                new JwtSecurityToken(
                    issuer:
                        _configuration["JwtSettings:Issuer"],
                    audience:
                        _configuration["JwtSettings:Audience"],
                    claims: claims,
                    expires:
                        DateTime.Now.AddMinutes(
                            Convert.ToDouble(
                                _configuration[
                                    "JwtSettings:ExpireMinutes"
                                ]
                            )
                        ),
                    signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler()
                .WriteToken(token);
        }
    }
}