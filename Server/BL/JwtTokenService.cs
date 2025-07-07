using Microsoft.IdentityModel.Tokens;
using Server.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace TasksApi.Services
{
    public class JwtTokenService
    {
        private readonly IConfiguration _configuration;

        public JwtTokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

  
        public string GenerateJwtToken(string username, List<string> roles, User u)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.UserData, u.UserName),
                new Claim(ClaimTypes.UserData, u.PassWard),
                new Claim(ClaimTypes.UserData, u.Email),
                new Claim(ClaimTypes.UserData, u.FullName),
                new Claim(ClaimTypes.UserData, u.Phone),

                new Claim(ClaimTypes.NameIdentifier, u.Id.ToString())
            };

            // Add roles as claims
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: credentials
            );

            var a = new JwtSecurityTokenHandler().WriteToken(token);
            return a;
        }

    }
}
