using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskManagement.Api.Models;
using TaskManagement.Application.Repositories;

namespace TaskManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        public LoginController(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        [HttpPost(Name = "login")]
        public async Task<ActionResult<string>> Login(LoginCommand loginCommand)
        {
            var user = await _userRepository.GetByEmail(loginCommand.Email);
            
            if (user == null)
                return BadRequest("Invalid credentials.");

            if (user.Password != loginCommand.Password)
                return BadRequest("Invalid credentials.");

            var claims = new[]
            {
                new Claim (ClaimTypes.Email, loginCommand.Email),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var jwtToken = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(6),
                signingCredentials: credentials);

            var token = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            return Ok(token);
        } 
    }
}