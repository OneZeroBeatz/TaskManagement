using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskManagement.Application.Interfaces;

namespace TaskManagement.Application.Factories
{
    public class AuthenticationTokenFactory : IAuthenticationTokenFactory
    {
        private readonly IConfiguration _configuration;

        public AuthenticationTokenFactory(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public string GenerateToken(string email)
        {
            var claims = new[]
            {
                new Claim (ClaimTypes.Email, email),
            };

            //TODO: Create model for configuration
            var keyAsBytes = Encoding.UTF8.GetBytes(_configuration.GetSection("Token:Key").Value);
            var symmetricSecurityKey = new SymmetricSecurityKey(keyAsBytes);

            var credentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha512Signature);
            var jwtToken = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(int.Parse(_configuration.GetSection("Token:ExpirationInHours").Value)),
                signingCredentials: credentials);

            var token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return token;
        }
    }
}
