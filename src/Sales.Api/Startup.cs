namespace Sales.Api
{
    using System;
    using Autofac;
    using Autofac.Extensions.DependencyInjection;
    using ITOps.Shared;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using NServiceBus;
    using Sales.Api.Data;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            // We are sharing the same container between MVC Core and NServiceBus endpoint.
            services.AddDbContext<SalesDbContext>(opt => opt.UseInMemoryDatabase("ProductPriceList"));
            services.AddMvc();

            var builder = new ContainerBuilder();
            builder.Populate(services);

            // Register a place holder instance in the Asp.net core container, so when the container is built, it will
            // have the correct reference to IMessageSession.
            IMessageSession endpoint = null;
            builder.Register(c => endpoint)
                .As<IMessageSession>()
                .SingleInstance();

            var container = builder.Build();
            endpoint = BootstrapNServiceBusForMessaging(container);
            return new AutofacServiceProvider(container);
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

        IMessageSession BootstrapNServiceBusForMessaging(IContainer container)
        {
            var endpointConfiguration = new EndpointConfiguration("Sales.Api");
            endpointConfiguration.ApplyCommonNServiceBusConfiguration(container);

            // Configure saga audit plugin
            endpointConfiguration.AuditSagaStateChanges("Particular.ServiceControl");
            return Endpoint.Start(endpointConfiguration).GetAwaiter().GetResult();
        }
    }
}