using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
// for dep injection in ConfigureServices
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
// for Configure extension method?
// or for IWebHostBuilder
using Microsoft.AspNetCore.Hosting;
// for Host
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Routing;


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
            });
        }


        static void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthorization();
        }
    }
}
