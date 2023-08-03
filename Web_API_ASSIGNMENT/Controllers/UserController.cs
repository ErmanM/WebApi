using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Web_API_ASSIGNMENT.Data;
using Web_API_ASSIGNMENT.Model;

namespace Web_API_ASSIGNMENT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly WebApiDbContext _webApiDbContext;

        public UserController(WebApiDbContext webApiDbContext)
        {
            _webApiDbContext = webApiDbContext;
        }
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel user)
        {
            if (user == null)
            {
                return BadRequest("Invalid client request");
            }

            var userExist = _webApiDbContext.Users.FirstOrDefault(x => x.Username == user.UserName);

            if(userExist == null)
            {
                return BadRequest("Username or password is wrong");
            }

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokeOptions = new JwtSecurityToken(
                issuer: "https://localhost:7121",
                audience: "https://localhost:7121",
                claims: new List<Claim>() { new Claim("UserName", userExist.Username) },
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: signinCredentials
            );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
            return Ok(new { Token = tokenString, UserId = userExist.UserId, LocationId = userExist.LoactionId, ProjectId = userExist.ProjectId });
        }
    }
}
