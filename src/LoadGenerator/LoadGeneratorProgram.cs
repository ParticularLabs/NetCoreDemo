using System;
using EShop.Messages.Commands;
using ITOps.Shared;
using NServiceBus;

namespace LoadGenerator
{
    using System.Threading.Tasks;

    class LoadGeneratorProgram
    {
        static async Task Main(string[] args)
        {
            var endpointConfiguration = new EndpointConfiguration("LoadBalancer");
            endpointConfiguration.ApplyCommonNServiceBusConfiguration();

            var endpoint = await Endpoint.Start(endpointConfiguration)
                .ConfigureAwait(false);

            while (true)
            {
                await Task.Delay(TimeSpan.FromMilliseconds(100))
                    .ConfigureAwait(false);

                await endpoint.Send(new PlaceOrder {ProductId = 1})
                    .ConfigureAwait(false);
            }

            await endpoint.Stop()
                .ConfigureAwait(false);
        }
    }
}
