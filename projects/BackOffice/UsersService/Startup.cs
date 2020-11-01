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
using UsersService.Infrastructure.Filters;
using UsersService.Infrastructure.Config;
using UsersService.Infrastructure.Notifiers;
using UsersService.Application.Consumers;

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
            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(ValidateModelAttribute));
            })
            .ConfigureApiBehaviorOptions(options =>
            {
                // Suppress automatic model validation
                // So my filter works ;)
                // options.SuppressModelStateInvalidFilter = true;
            });

            services.AddSingleton<IRabbitMQConfig, RabbitMQConfig>((serviceProvider) =>
            {
                var configSections = Configuration.GetSection("Rabbitmq");
                return new RabbitMQConfig {
                    Host = configSections["Host"],
                    UserName = configSections["UserName"],
                    Password = configSections["Password"],
                    VirtualHost = configSections["VirtualHost"],
                    Port = Convert.ToUInt16(configSections["Port"]),
                    Endpoint = configSections["Endpoint"],
                    DurableQueue = Convert.ToBoolean(configSections["DurableQueue"])
                };
            });
            services.AddMassTransit(options =>
            {
                options.AddConsumer<RegistrationSuccessfulConsumer>();
                options.AddBus(provider => ConfigureRabbitMQ(provider));
            });

            services.AddMassTransitHostedService();
            services.AddTransient<ISendMessage, SendMessageToEndpoint>();
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

        private static IBusControl ConfigureRabbitMQ(IBusRegistrationContext context)
        {
            IRabbitMQConfig config = context.GetRequiredService<IRabbitMQConfig>();

            var bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                // cfg.UseHealthCheck(context);
                cfg.Host(config.Host, config.VirtualHost, rmqHost =>
                {
                    rmqHost.Username(config.UserName);
                    rmqHost.Password(config.Password);
                });

                cfg.ReceiveEndpoint(config.Endpoint, rmqEndpoint =>
                {
                    rmqEndpoint.PrefetchCount = config.PrefetchCount;
                    rmqEndpoint.Durable = config.DurableQueue;
                    rmqEndpoint.ConfigureConsumer<RegistrationSuccessfulConsumer>(context);
                });
            });

            bus.Start();

            return bus;
        }
    }
}
