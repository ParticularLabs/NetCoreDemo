using System;
using NServiceBus;

namespace ITOps.Shared
{
    public static class CommonNServiceBusConfiguration
    {
        public static void ApplyCommonNServiceBusConfiguration(this EndpointConfiguration endpointConfiguration)
        {

            // Transport configuration
            var transport = endpointConfiguration.UseTransport<LearningTransport>();

            // Routing configuration 
            // Call RegisterPublisher when using a message based subscription such as MSMQ or SQLServer transport
            var routing = transport.Routing();
            //routing.RegisterPublisher(typeof(EShop.Messages.Events.OrderBilled), "Billing.Api");
            //routing.RegisterPublisher(typeof(EShop.Messages.Events.OrderPlaced), "Sales.Api");
            routing.RouteToEndpoint(typeof(EShop.Messages.Commands.PlaceOrder), "Sales.Api");
            routing.RouteToEndpoint(typeof(EShop.Messages.Commands.PrepareInventory), "Warehouse.Api");
            
            // Persistence Configuration
            endpointConfiguration.UsePersistence<LearningPersistence>();
            
            // JSon Serializer
            endpointConfiguration.UseSerialization<NewtonsoftSerializer>();
            
            // Enable Metrics Collection and Reporting
            endpointConfiguration
                .EnableMetrics()
                .SendMetricDataToServiceControl("Particular.Monitoring", TimeSpan.FromSeconds(5));

            // Enable endpoint hearbeat reporting
            endpointConfiguration.SendHeartbeatTo("Particular.ServiceControl", TimeSpan.FromSeconds(30));

        }
    }
}
