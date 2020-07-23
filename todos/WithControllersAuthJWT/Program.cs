using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


namespace WithControllersAuthJWT
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var jwtSettings = JwtSettings.FromConfiguration(builder.Configuration);


            // Add Identity Core + EF Store
            builder.Services.AddIdentityCore<User>()
                .AddEntityFrameworkStores<DbContext>();

            // Add EF, Use In Memory
            builder.Services.AddDbContext<DbContext>(opts => opts.UseInMemoryDatabase("Todos"));
            // Add the jwtSettings as Singleton
            builder.Services.AddSingleton(jwtSettings);


            // Authorization & Authentication
            builder.Services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opts => opts.TokenValidationParameters = jwtSettings.TokenValidationParameters);


            builder.Services.AddAuthorization(opts =>
                {
                    opts.AddPolicy("admin", policy => policy.RequireClaim("can_delete", "true"));
                    opts.AddPolicy("user", policy => policy.RequireClaim("can_view", "true"));
                });

            builder.Services.AddControllers();
            WebApplication app = builder.Build();

            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            await app.RunAsync();
        }
    }
}
