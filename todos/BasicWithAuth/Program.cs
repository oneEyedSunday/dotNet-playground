using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
// for dep injection in ConfigureServices
using Microsoft.Extensions.DependencyInjection;
// for IExceptionHandlerPathFEature
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
// for Configure extension method?
// or for IWebHostBuilder
using Microsoft.AspNetCore.Hosting;
// for Host
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;


namespace BasicWithAuth
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var app = Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(_b =>
                {
                    _b.Configure(Configure);
                    _b.ConfigureServices(ConfigureServices);
                }).Build();

            await app.RunAsync();
        }

        static void Configure(IApplicationBuilder app)
        {
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseExceptionHandler(_app =>
            {
                _app.Run(async context =>
                {
                    var feature = context.Features.Get<IExceptionHandlerPathFeature>();
                    var exception = feature.Error;

                    if (exception != null)
                    {
                        context.Response.ContentType = "application/json";
                        var errorResponse = new {
                            Error = true,
                            Message = exception.Message
                        };
                        await JsonSerializer.SerializeAsync(context.Response.Body, errorResponse);
                    }
                });
            });
            app.UseRouting();
            app.UseEndpoints((IEndpointRouteBuilder endpoints) =>
            {
                endpoints.MapGet("/api", async (HttpContext context) =>
                {
                    context.Response.StatusCode = StatusCodes.Status200OK;
                    await JsonSerializer.SerializeAsync(context.Response.Body, new {
                        Message = "Welcome, this time with Auth"
                    });
                    return;
                });



                var todoApi = new TodoApi();
                todoApi.MapRoutes(endpoints);
                var authApi = new AuthApi();
                authApi.MapRoutes(endpoints);
            });
        }


        static void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentityCore<BasicWithAuthUser>()
                .AddEntityFrameworkStores<TodoDbContext>();
            services.AddDbContext<TodoDbContext>(opts => opts.UseInMemoryDatabase("Todos"));
            services.AddAuthentication();
            services.AddAuthorization();
        }
    }
}
