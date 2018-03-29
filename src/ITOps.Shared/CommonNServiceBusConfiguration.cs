using System;
using NServiceBus;

namespace ITOps.Shared
{
    public static class CommonNServiceBusConfiguration
    {
        public static void ApplyCommonNServiceBusConfiguration(this EndpointConfiguration endpointConfiguration,
            Action<TransportExtensions<LearningTransport>> messageEndpointMappings = null)
        {
            var transport = endpointConfiguration.UseTransport<LearningTransport>();
            messageEndpointMappings?.Invoke(transport);
            endpointConfiguration.UsePersistence<LearningPersistence>();
            endpointConfiguration.UseSerialization<NewtonsoftSerializer>();

            // Enable Metrics Collection and Reporting
            endpointConfiguration
                .EnableMetrics()
                .SendMetricDataToServiceControl("Particular.Monitoring", TimeSpan.FromSeconds(5), "BillingInstance1");

            // Enable endpoint hearbeat reporting
            endpointConfiguration.SendHeartbeatTo("Particular.ServiceControl", TimeSpan.FromSeconds(30));

        }
    }
}
