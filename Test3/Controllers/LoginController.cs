using JwtAuthLibrary.Model;
using JwtAuthLibrary.Service;
using Microsoft.AspNetCore.Mvc;

namespace Test3.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : Controller
    {


        private readonly IJwtTokenService _jwtTokenService;

        public LoginController(IJwtTokenService jwtTokenService)
        {
            _jwtTokenService = jwtTokenService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLogin login)
        {
            try
            {
                // In a real application, you should validate the username and password against your data store
                if (login.Username != "testuser" || login.Password != "testpassword")
                {
                    return Unauthorized();
                }

                var token = _jwtTokenService.GenerateToken(login.Username);
                return Ok(new { Token = token }); 
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }
    }
}