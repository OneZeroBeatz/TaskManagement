using Invoicing.CrossCutting.Domain;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskManagement.Application.Messages;
using TaskManagement.Application.Repositories;

namespace TaskManagement.Application.MessageHandlers
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, Result<string>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;


        public LoginCommandHandler(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task<Result<string>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmail(request.Email);

            if (user == null)
                return Result.Error<string>("Invalid credentials.");

            if (user.Password != request.Password)
                return Result.Error<string>("Invalid credentials.");

            string token = GenerateToken(request.Email);

            return Result.Ok(token);
        }

        private string GenerateToken(string email)
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
