using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Annotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApiNetCore.Api.Models;

namespace WebApiNetCore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("login")]
        [SwaggerOperation(Summary = "Authorization", Description = "Login with Username/Password and get token. Username: admin Password: 123")]
        public IActionResult Login([FromBody] LoginModel Model)
        {
            if (Model.Username == "admin" && Model.Password == "123")
            {
                var Claims = new[]
                {
                    new Claim(ClaimTypes.Name, Model.Username),
                    new Claim(ClaimTypes.Role, "Admin")
                };
                var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
                var Creds = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);
                var Token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    claims: Claims,
                    expires: DateTime.Now.AddHours(1),
                    signingCredentials: Creds
                );
                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(Token)
                });
            }
            return Unauthorized();
        }

    }
}