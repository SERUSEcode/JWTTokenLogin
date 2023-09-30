using JWTTokenLogin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JWTTokenLogin.Controllers
{
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AuthenticationController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [Route("TestToken")]
        [Authorize]
        [HttpGet]
        //Checks if token works, must be tested with postman
        public async Task<IActionResult> TestToken()
        {
            return Ok("Your token is still active");
        }

        [HttpPost]
        [Route("authenticateWithUserDetail")]
        //Creates a token if with the right username and password
        public object Authenticate(string UserName, string Password)
        {
                if (UserName == "test" && Password == "123")
                {
                    var claims = new List<Claim>();
                    claims.Add(new Claim("Username", "test"));
                    claims.Add(new Claim("displayname", "test"));

                    claims.Add(new Claim(ClaimTypes.Role, "null"));

                var token = JwtHelper.GetJwtToken(
                "TestUsername",
                    _configuration["Jwt:Key"],
                    _configuration["Jwt:Issuer"],
                    _configuration["Jwt:Audience"],
                    TimeSpan.FromMinutes(Convert.ToDouble(_configuration["Jwt:TokenTimeoutMinutes"])),
                    claims.ToArray());

                return new
                {
                    role = "null",
                    username = UserName,
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expires = token.ValidTo
                };
            }

            return Ok("Username or password is wrong, please try again with username test and password 123");
        }
    
    //Creates the acuall token
    public class JwtHelper
        {
            public static JwtSecurityToken GetJwtToken(
                string username,
                string signingKey,
                string issuer,
                string audience,
                TimeSpan expiration,
                Claim[] additionalClaims = null)
            {
                var claims = new[]
                {
                new Claim(JwtRegisteredClaimNames.Sub,username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

                if (additionalClaims is object)
                {
                    var claimList = new List<Claim>(claims);
                    claimList.AddRange(additionalClaims);
                    claims = claimList.ToArray();
                }

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                return new JwtSecurityToken(
                    issuer: issuer,
                    audience: audience,
                    expires: DateTime.UtcNow.Add(expiration),
                    claims: claims,
                    signingCredentials: creds
                );
            }
        }

    }
}