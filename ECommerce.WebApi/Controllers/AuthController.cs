using ECommerce.WebApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
namespace ECommerce.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly TokenService _tokenService;

        public AuthController(TokenService tokenService)
        {
            _tokenService = tokenService;
        }
        //giriş yapar
        [HttpPost("login")]
        public IActionResult Login(string username, string password)
        {
            if (username == "admin" && password == "1234")
            {
                var token = _tokenService.CreateToken(username);
                return Ok(new { Token = token });
            }

            return Unauthorized("Kullanıcı adı veya şifre hatalı");
        }
    }
}
