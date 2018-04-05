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
            endpointConfiguration.ApplyCommonNServiceBusConfiguration(enableMonitoring: false);
            endpointConfiguration.SendOnly();

            var endpoint = await Endpoint.Start(endpointConfiguration)
                .ConfigureAwait(false);

            Console.WriteLine("Press up/down arrow to increase/decrease messages per second");
            Console.WriteLine("Press ESC key to exit");
            var messagesPerSecond = 1;
            var currentOrderId = 0;

            while (true)
            {
                if (Console.KeyAvailable) {
                    var key = Console.ReadKey();
                    if (key.Key == ConsoleKey.Escape)
                    {
                        break;
                    }
                    switch (key.Key) {
                        case ConsoleKey.UpArrow:
                            messagesPerSecond++;
                            break;
                        case ConsoleKey.DownArrow:
                            messagesPerSecond = Math.Max(1, --messagesPerSecond);
                            break;
                    }
                    Console.WriteLine($"Messages per second: {messagesPerSecond}");

                }
                var delay = 1000 / messagesPerSecond;

                var orderId = ++currentOrderId;
                var productId = orderId % 3 + 1;
                Console.WriteLine($"Sending PlaceOrder message: OrderId 'LoadGen-{orderId}', ProductId = {productId}");
                await endpoint.Send(new PlaceOrder
                    {
                        OrderId = "LoadGen-" + orderId,
                        ProductId = productId
                    })
                    .ConfigureAwait(false);


                await Task.Delay(delay)
                    .ConfigureAwait(false);
            }

            await endpoint.Stop()
                .ConfigureAwait(false);
        }
    }
}
