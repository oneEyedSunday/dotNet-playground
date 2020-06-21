using System;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
// For UserManager
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace BasicWithAuth
{
    public class AuthApi: IApi
    {
        private static readonly JsonSerializerOptions _options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        public AuthApi()
        {

        }

        private async Task Register(UserManager<BasicWithAuthUser> userManager, HttpContext context)
        {
            var authInfo = await JsonSerializer.DeserializeAsync<Auth>(context.Request.Body, _options);

            var result = await userManager.CreateAsync(new BasicWithAuthUser { UserName = $"user_{authInfo.UserName}" }, authInfo.Password);

            if (result.Succeeded)
            {
                context.Response.StatusCode = StatusCodes.Status201Created;
                return;
            }

            context.Response.StatusCode = StatusCodes.Status400BadRequest;
        }
        public void MapRoutes(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapPost("/api/v2/auth", WithUserManager(Register));
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
