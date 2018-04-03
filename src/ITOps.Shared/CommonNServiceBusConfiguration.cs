using System;
using NServiceBus;
using NServiceBus.Transport;

namespace ITOps.Shared
{
    public static class CommonNServiceBusConfiguration
    {
        public static void ApplyCommonNServiceBusConfiguration(this EndpointConfiguration endpointConfiguration)
        {

            // Transport configuration
            var rabbitMqConnectionString = Environment.GetEnvironmentVariable("NetCoreDemoRabbitMQTransport");

            if (String.IsNullOrEmpty(rabbitMqConnectionString))
            {
                var transport = endpointConfiguration.UseTransport<LearningTransport>();
                ConfigureRouting(transport);
                // Persistence Configuration
                endpointConfiguration.UsePersistence<InMemoryPersistence>();
            }
            else
            {
                var transport = endpointConfiguration.UseTransport<RabbitMQTransport>()
                    .ConnectionString(rabbitMqConnectionString)
                    .UseConventionalRoutingTopology();
                ConfigureRouting(transport);
                // Persistence Configuration
                endpointConfiguration.UsePersistence<LearningPersistence>();
            }
            
            endpointConfiguration.EnableInstallers();

            
            
            // JSon Serializer
            endpointConfiguration.UseSerialization<NewtonsoftSerializer>();
            
            // Enable Metrics Collection and Reporting
            endpointConfiguration
                .EnableMetrics()
                .SendMetricDataToServiceControl("Particular.Monitoring", TimeSpan.FromSeconds(5));

            // Enable endpoint hearbeat reporting
            endpointConfiguration.SendHeartbeatTo("Particular.ServiceControl", TimeSpan.FromSeconds(30));

        }

        static void ConfigureRouting<T>(TransportExtensions<T> transport) 
            where T : TransportDefinition
        {
            var routing = transport.Routing();
            routing.RouteToEndpoint(typeof(EShop.Messages.Commands.PlaceOrder), "Sales.Api");
            routing.RouteToEndpoint(typeof(EShop.Messages.Commands.RecordConsumerBehavior), "Marketing.Api");

            // For transports that do not support publish/subcribe natively, e.g. MSMQ, SqlTransport, call RegisterPublisher
        }
    }
}
