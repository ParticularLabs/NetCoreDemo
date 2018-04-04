namespace LoadGenerator
{
    using System;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using EShop.Messages.Commands;
    using ITOps.Shared;
    using NServiceBus;

    class LoadGeneratorProgram
    {
        static async Task Main(string[] args)
        {
            Console.Title = "Load Generator";

            var endpointConfiguration = new EndpointConfiguration("LoadGenerator");
            endpointConfiguration.ApplyCommonNServiceBusConfiguration();

            var endpoint = await Endpoint.Start(endpointConfiguration)
                .ConfigureAwait(false);

            while (true)
            {
                await endpoint.Send(new PlaceOrder { ProductId = 1 })
                    .ConfigureAwait(false);
                
                await Task.Delay(200)
                    .ConfigureAwait(false);
            }

            await endpoint.Stop()
                .ConfigureAwait(false);
        }
    }
}
