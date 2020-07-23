using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace WithControllersAuthJWT
{
    [ApiController]
    [Route("/api/auth")]
    [AllowAnonymous]
    public class AuthController: ControllerBase
    {
        private readonly JwtSettings _jwtSettings;
        private readonly UserManager<User> _userManager;

        public AuthController(JwtSettings jwtSettings, UserManager<User> userManager)
        {
            _jwtSettings = jwtSettings;
            _userManager = userManager;
        }


        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(AuthAttempt creds)
        {
            IdentityResult result = await _userManager.CreateAsync(new User { UserName = creds.UserName }, creds.Password);

            if (result.Succeeded)
            {
                return Accepted();
            }
            return BadRequest(result.Errors);
        }

        [HttpPost("token")]
        public async Task<IActionResult> Login(AuthAttempt creds)
        {
            var user = await _userManager.FindByNameAsync(creds.UserName);
            if (user == null || !await _userManager.CheckPasswordAsync(user, creds.Password))
            {
                return Unauthorized();
            }

            var claims = new List<Claim>();

            if (user.IsAdmin)
            {
                claims.Add(new Claim("can_delete", "true"));
                claims.Add(new Claim("can_view", "true"));
            }

            var key = new SymmetricSecurityKey(_jwtSettings.Key);
            var signingCreds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: signingCreds
            );

            return Ok(new {
                token = new JwtSecurityTokenHandler().WriteToken(token)
            });
        }
    }
}