namespace Shipping.Api
{
    using ITOps.Shared;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using NServiceBus;
    using Shipping.Api.Data;
    using Warehouse.Azure;

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
            services.AddDbContext<StockItemDbContext>(opt => opt.UseInMemoryDatabase("StockItemList"));
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
            var endpointConfiguration = new EndpointConfiguration("Shipping.Api");

            endpointConfiguration.ApplyCommonNServiceBusConfiguration(bridgeConfigurator: transport =>
            {
                // Bridge Shipping
                var bridge = transport.Routing().ConnectToBridge("bridge-shipping");

                // Subscribe to events from warehouse to be delivered via bridge
                bridge.RegisterPublisher(eventType: typeof(ItemRestocked), publisherEndpointName: "warehouse");
            });

            // Configure saga audit plugin
            endpointConfiguration.AuditSagaStateChanges(
                serviceControlQueue: "Particular.ServiceControl");

            var instance = Endpoint.Start(endpointConfiguration).GetAwaiter().GetResult();
            services.AddSingleton<IMessageSession>(instance);
        }
    }
}
