using EasyNetQ.Management.Client.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MinvoiceWebhook.Models;
using MinvoiceWebhook.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MinvoiceWebhook.Controllers
{
    /*[Route("api/[controller]")]
    [ApiController]
    public class UserLoginController : ControllerBase
    {
        private const string SecretKey = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiYWRtaW" +
            "4iOnRydWV9.TJVA95OrM7E2cBab30RMHrHDcEfxjoYZgeFONFh7HgQ";
        private readonly SymmetricSecurityKey _signingKey;
       
        public UserLoginController()
        {
            _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));
        }
        [HttpPost("login")]
        public IActionResult Login([FromBody] string username, string password)
        {
            if (username != "admin" || password != "123456")
            {
                return Unauthorized();
            }           
            var token = GenerateToken(username,password);
            return Ok(new { token });
        }
        private string GenerateToken(string password)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, password),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2), 
                signingCredentials: new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }*/
    [ApiController]
    [Route("api/[controller]")]
    public class UserLoginController : ControllerBase
    {
        private readonly ITokenService _tokenService;

        public UserLoginController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel model)
        {
            if (model.Username != "Admin" || model.Password != "123456")
            {
                return Unauthorized();
            }
            var token = _tokenService.GenerateToken(model.Username, model.Password);

            // Gửi token về phía client
            return Ok(new { Token = token });
        }
    }

}
