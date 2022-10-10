using Microsoft.AspNetCore.Mvc;
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



            return Ok("token");
        } 
    }
}