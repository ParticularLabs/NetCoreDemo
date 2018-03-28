﻿namespace Sales.Api
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Sales.Api.Data;
    using ITOps.Shared;
    using NServiceBus;

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
            services.AddDbContext<ProductPriceDbContext>(opt => opt.UseInMemoryDatabase("ProductPriceList"));
            services.AddMvc();
            BootstrapNServiceBusForMessaging(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }

        void BootstrapNServiceBusForMessaging(IServiceCollection services)
        {
            var endpointConfiguration = new EndpointConfiguration("Sales.Api");
            endpointConfiguration.ApplyCommonNServiceBusConfiguration(transport =>
            {
                var routing = transport.Routing();
                // Add any routing configuration here, For example:
                //routing.RouteToEndpoint(typeof(EShop.Messages.Commands.PlaceOrder).Assembly, "EShop.Messages.Commands", "YourEndpointName");
            });
            var instance = Endpoint.Start(endpointConfiguration).GetAwaiter().GetResult();
            services.AddSingleton<IMessageSession>(instance);
        }
    }
}
