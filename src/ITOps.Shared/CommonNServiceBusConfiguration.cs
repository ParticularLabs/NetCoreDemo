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
        }
    }
}
