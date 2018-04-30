namespace Warehouse.Azure.UI
{
    using System;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using NServiceBus;
    using NServiceBus.Logging;

    public class Startup
    {
        static ILog log = LogManager.GetLogger(typeof(Program));
        private IEndpointInstance endpoint;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            endpoint = BootstrapNServiceBusForMessaging(services);

            services.AddSingleton<IMessageSession>(endpoint);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime applicationLifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    "default",
                    "{controller=Product}/{action=Index}/{id?}");
            });

            applicationLifetime.ApplicationStopping.Register(() => endpoint.Stop().GetAwaiter().GetResult());
        }

        IEndpointInstance BootstrapNServiceBusForMessaging(IServiceCollection services)
        {
            var endpointConfiguration = new EndpointConfiguration("Warehouse");
            endpointConfiguration.SendFailedMessagesTo("error");

            var asqConnectionString = Environment.GetEnvironmentVariable("NetCoreDemoAzureStorageQueueTransport");
            if (string.IsNullOrEmpty(asqConnectionString))
            {
                log.Info("Connection for Azure Storage Queue transport is missing or empty.");
            }

            log.Info("Using Azure Storage Queue Transport");
            endpointConfiguration.UseTransport<AzureStorageQueueTransport>()
                .ConnectionString(asqConnectionString);

            // Persistence Configuration
            endpointConfiguration.UsePersistence<InMemoryPersistence>();

            endpointConfiguration.UseSerialization<NewtonsoftSerializer>();
            endpointConfiguration.EnableInstallers();

            return Endpoint.Start(endpointConfiguration).GetAwaiter().GetResult();
        }
    }
}