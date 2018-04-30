namespace EShop.UI
{
    using ITOps.Shared;
    using ITOps.ViewModelComposition;
    using ITOps.ViewModelComposition.Mvc;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.DependencyInjection;
    using NServiceBus;

    public class Startup
    {
        IEndpointInstance endpoint;

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddViewModelComposition();
            services.AddMvc()
                .AddViewModelCompositionMvcSupport();

            endpoint = BootstrapNServiceBusForMessaging(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime appLifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    "default",
                    "{controller=Products}/{action=Index}/{id?}");
            });

            app.UseStaticFiles();
        }

        IEndpointInstance BootstrapNServiceBusForMessaging(IServiceCollection services)
        {
            var endpointConfiguration = new EndpointConfiguration("EShop.UI");
            endpointConfiguration.PurgeOnStartup(true);
            endpointConfiguration.ApplyCommonNServiceBusConfiguration();
            var instance = Endpoint.Start(endpointConfiguration).GetAwaiter().GetResult();
            services.AddSingleton<IMessageSession>(instance);
            return instance;
        }
    }
}