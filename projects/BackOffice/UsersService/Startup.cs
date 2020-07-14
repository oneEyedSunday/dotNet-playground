using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace UsersService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddMassTransit(options =>
            {
               var _config = new Dictionary<string, string> {
                   ["Host"] = "localhost",
                   ["UserName"] = "ispoa",
                   ["Password"] = "password",
                   ["VirtualHost"] = "my_vhost",
                   ["Port"] = "5672",
                   ["QueueName"] = "TopicSubscriptions"
               };

               options.AddBus(provider => ConfigureRabbitMQ(provider, _config));
            });

            services.AddMassTransitHostedService();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static IBusControl ConfigureRabbitMQ(IBusRegistrationContext context, Dictionary<string, string> config)
        {

            var bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                // cfg.UseHealthCheck(context);
                cfg.Host(config["Host"], config["VirtualHost"], rmqHost =>
                {
                    rmqHost.Username(config["UserName"]);
                    rmqHost.Password(config["Password"]);
                });

                cfg.ReceiveEndpoint(config["QueueName"], rmqEndpoint =>
                {
                    rmqEndpoint.PrefetchCount = 100;
                    rmqEndpoint.Durable = true;
                    // rmqEndpoint.ConfigureConsumer<>(context);
                });
            });

            bus.Start();

            return bus;
        }
    }
}
