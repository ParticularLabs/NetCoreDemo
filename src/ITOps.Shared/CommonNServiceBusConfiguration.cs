using System;
using NServiceBus;
using NServiceBus.Logging;
using NServiceBus.Transport;

namespace ITOps.Shared
{
    public static class CommonNServiceBusConfiguration
    {
        static ILog log = LogManager.GetLogger(typeof (CommonNServiceBusConfiguration));

        public static void ApplyCommonNServiceBusConfiguration(this EndpointConfiguration endpointConfiguration, 
            bool enableMonitoring = true, Action<TransportExtensions<RabbitMQTransport>> bridgeConfigurator = null)
        {

            // Transport configuration
            var rabbitMqConnectionString = Environment.GetEnvironmentVariable("NetCoreDemoRabbitMQTransport");

            if (string.IsNullOrEmpty(rabbitMqConnectionString))
            {
                log.Info("Using Learning Transport");
                var transport = endpointConfiguration.UseTransport<LearningTransport>();
                ConfigureRouting(transport);
                // Persistence Configuration
                endpointConfiguration.UsePersistence<LearningPersistence>();
            }
            else
            {
                log.Info("Using RabbitMQ Transport");
                var transport = endpointConfiguration.UseTransport<RabbitMQTransport>()
                    .ConnectionString(rabbitMqConnectionString)
                    .UseConventionalRoutingTopology();

                ConfigureRouting(transport);

                bridgeConfigurator?.Invoke(transport);

                // Persistence Configuration
                endpointConfiguration.UsePersistence<InMemoryPersistence>();
            }
            
            endpointConfiguration.EnableInstallers();

            // JSON Serializer
            endpointConfiguration.UseSerialization<NewtonsoftSerializer>();
            
            if (enableMonitoring)
            {
                endpointConfiguration.AuditProcessedMessagesTo("audit");
                
                // Enable Metrics Collection and Reporting
                endpointConfiguration
                    .EnableMetrics()
                    .SendMetricDataToServiceControl("Particular.Monitoring", TimeSpan.FromSeconds(5));

                // Enable endpoint hearbeat reporting
                endpointConfiguration.SendHeartbeatTo("Particular.ServiceControl", TimeSpan.FromSeconds(30));
            }
        }

        static void ConfigureRouting<T>(TransportExtensions<T> transport) 
            where T : TransportDefinition
        {
            var routing = transport.Routing();
            routing.RouteToEndpoint(typeof(EShop.Messages.Commands.PlaceOrder), "Sales.Api");
            routing.RouteToEndpoint(typeof(EShop.Messages.Commands.CancelOrder), "Sales.Api");
            routing.RouteToEndpoint(typeof(EShop.Messages.Commands.StoreOrder), "Sales.Api");
            routing.RouteToEndpoint(typeof(EShop.Messages.Commands.AcceptOrder), "Sales.Api");
            routing.RouteToEndpoint(typeof(EShop.Messages.Commands.RecordConsumerBehavior), "Marketing.Api");

            // For transports that do not support publish/subcribe natively, e.g. MSMQ, SqlTransport, call RegisterPublisher
        }
    }
}
