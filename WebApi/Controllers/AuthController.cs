using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApi.Entities;

namespace WebApi.Controllers
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
        [HttpPost]
        public IActionResult Authenticate([FromBody] Credential credential)
        {
            var claims = new List<Claim>();

            if (credential.UserName.Equals("admin") && credential.Password.Equals("admin"))
            {
                // creating the security context
                claims = new List<Claim> {
                new Claim(ClaimTypes.Name,credential.UserName),
                new Claim(ClaimTypes.Email,$"{credential.UserName}@gmail.com"),
                new Claim("Departmant","HR"),
                new Claim(ClaimTypes.Role,"Admin"),
                new Claim("over18","18")
            };

            }
            else
            {
                ModelState.AddModelError("Authorized", "you are not able to access this endpoint.");
                return Unauthorized(ModelState);
            }
            var expiresAt = DateTime.UtcNow.AddMinutes(10);
            return Ok(
                 new
                 {
                    access_token = GenerateToken(claims,expiresAt)
                 }
             );
        }
        private string GenerateToken(IEnumerable<Claim> claims,DateTime expiresAt)
        {
            var key = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("SecretKey"));
            // generate the jwt
            var jwt = new JwtSecurityToken(
                claims: claims,
                expires: expiresAt,
                notBefore: DateTime.UtcNow,
                // take the key and the algorthim
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
                );

            // JwtSecurityTokenHandler take a variable of type securitytoken and convert it to string
            return new JwtSecurityTokenHandler().WriteToken(jwt);

        }
    }
}
