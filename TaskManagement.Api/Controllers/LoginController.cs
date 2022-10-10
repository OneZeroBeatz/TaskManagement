using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskManagement.Application.Repositories;

namespace TaskManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        public LoginController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet(Name = "login")]
        public async Task<ActionResult<string>> Login(string email, string password)
        {
            var user = await _userRepository.GetByEmail(email);
            
            if (user == null)
                return BadRequest("Invalid credentials.");

            if (user.Password != password)
                return BadRequest("Invalid credentials.");

            var claims = new[]
            {
                new Claim (ClaimTypes.Email, email),
                new Claim(ClaimTypes.NameIdentifier, user.Username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("validationKey"));

            var jwtToken = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                );

            var token = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            return Ok(token);
        } 
    }
}