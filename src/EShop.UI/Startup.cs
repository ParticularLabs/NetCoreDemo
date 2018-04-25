﻿namespace EShop.UI
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
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddViewModelComposition();
            services.AddMvc()
                .AddViewModelCompositionMvcSupport();
            BootstrapNServiceBusForMessaging(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
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

        void BootstrapNServiceBusForMessaging(IServiceCollection services)
        {
            var endpointConfiguration = new EndpointConfiguration("EShop.UI");
            endpointConfiguration.PurgeOnStartup(true);
            endpointConfiguration.ApplyCommonNServiceBusConfiguration();
            var instance = Endpoint.Start(endpointConfiguration).GetAwaiter().GetResult();
            services.AddSingleton<IMessageSession>(instance);
        }
    }
}