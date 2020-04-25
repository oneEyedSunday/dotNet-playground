using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Builder;

namespace CustomRouter {
  public class Startup {
    public void ConfigureServices(IServiceCollection services) {
      services.AddRouting();
    }

    public void Configure(IApplicationBuilder app) {
      RouteBuilder rbEndpoints = new RouteBuilder(app);
      rbEndpoints.Routes.Add(
        new Route(new MyRouter(), "custom/{number:int}", 
          app.ApplicationServices.GetService<IInlineConstraintResolver>()
        )
      );

      app.UseRouter(rbEndpoints.Build());
    }
  }
}