using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
// For UserManager
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace BasicWithAuth
{
    public class AuthApi: IApi
    {
        private static readonly JsonSerializerOptions _options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        private readonly JwtSettings jwtSettings;

        public AuthApi(JwtSettings _jwtSettings)
        {
            jwtSettings = _jwtSettings;
        }

        private JwtSecurityToken GenerateToken(List<Claim> claims)
        {
            var key = new SymmetricSecurityKey(jwtSettings.Key);
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            return new JwtSecurityToken(
                issuer: jwtSettings.Issuer,
                audience: jwtSettings.Audience,
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: credentials,
                claims: claims
                );
        }

        private async Task Login(UserManager<BasicWithAuthUser> userManager, HttpContext context)
        {
            var authInfo = await JsonSerializer.DeserializeAsync<Auth>(context.Request.Body, _options);

            var user = await userManager.FindByNameAsync(authInfo?.UserName);

            if (user == null || !(await userManager.CheckPasswordAsync(user, authInfo?.Password)))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return;
            }

            var claims = new List<Claim>();

            if (user.isAdmin)
            {
                claims.Add(new Claim(TodoClaims.CanDelete, "true"));
                claims.Add(new Claim(TodoClaims.CanView, "true"));
            }

            var token = GenerateToken(claims);

            await JsonSerializer.SerializeAsync(
                context.Response.Body,
                new {
                    token =  new JwtSecurityTokenHandler().WriteToken(token), UserName = user.UserName, Email = user.Email },
                _options
                );
        }

        private async Task Register(UserManager<BasicWithAuthUser> userManager, HttpContext context)
        {
            var authInfo = await JsonSerializer.DeserializeAsync<Auth>(context.Request.Body, _options);

            var result = await userManager.CreateAsync(new BasicWithAuthUser { UserName = $"{authInfo.UserName}" }, authInfo.Password);

            if (result.Succeeded)
            {
                context.Response.StatusCode = StatusCodes.Status201Created;
                return;
            }

            context.Response.StatusCode = StatusCodes.Status400BadRequest;
        }
        public void MapRoutes(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapPost("/api/v2/auth/register", WithUserManager(Register));
            endpoints.MapPost("/api/v2/auth/login", WithUserManager(Login));
        }

        private RequestDelegate WithUserManager(Func<UserManager<BasicWithAuthUser>, HttpContext, Task> handler)
        {
            return context =>
            {
                // Resolve the service from the container
                // .GetRequiredService so if unavailable it throws an Exception instead of null
                var userManager = context.RequestServices.GetRequiredService<UserManager<BasicWithAuthUser>>();
                return handler(userManager, context);
            };
        }
    }
}
