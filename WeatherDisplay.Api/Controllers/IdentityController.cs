using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using WeatherDisplay.Api.Contracts;
using WeatherDisplay.Api.Models;
using WeatherDisplay.Api.Services;

namespace WeatherDisplay.Api.Controllers
{
    [ApiController]
    [Route("api/identity")]
    public class IdentityController : ControllerBase
    {
        private readonly IdentityOptions identityOptions;
        private readonly IUserService userService;

        public IdentityController(IOptionsMonitor<IdentityOptions> identityOptions, IUserService userService)
        {
            this.identityOptions = identityOptions.CurrentValue;
            this.userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login(LoginDto loginDto)
        {
            try
            {
                var user = this.userService.GetUser(loginDto.UserName, loginDto.Password);
                if (user != null)
                {
                    var accessToken = this.GenerateJwtToken(user);
                    return this.Ok(accessToken);
                }
            }
            catch
            {
                return this.BadRequest("Login failed");
            }

            return this.Unauthorized();
        }

        private string GenerateJwtToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.identityOptions.JwtKey));
            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var issueDate = DateTime.Now;
            var expiryDate = issueDate.AddDays(this.identityOptions.JwtExpireDays);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: this.identityOptions.JwtIssuer,
                audience: this.identityOptions.JwtIssuer,
                claims: new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Name, user.Username),
                },
                expires: expiryDate,
                signingCredentials: signingCredentials
            );

            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            return accessToken;
        }
    }
}
