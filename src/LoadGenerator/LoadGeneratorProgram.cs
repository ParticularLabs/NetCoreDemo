namespace LoadGenerator
{
    using System;
    using System.Threading.Tasks;
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

            Console.WriteLine("Press enter to send a message");
            Console.WriteLine("Press any key to exit");

            while (true)
            {
                var key = Console.ReadKey();
                Console.WriteLine();

                if (key.Key != ConsoleKey.Enter)
                {
                    break;
                }

                Console.WriteLine("Sending PlaceOrder message for ProductId = 1");
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
