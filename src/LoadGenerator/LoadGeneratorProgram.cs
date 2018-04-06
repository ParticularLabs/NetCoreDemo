namespace LoadGenerator
{
    using System;
    using System.Linq;
    using System.Threading;
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




            Console.WriteLine("Press ▲/▼ arrows to increase/decrease messages per second");
            Console.WriteLine("Press S to cause a spike of 25 messages");
            Console.WriteLine("Press P to pause/unpause message sending");
            Console.WriteLine("Press ESC key to exit");

            var producer = new MessageProducer(endpoint);
            var producerTask = producer.Run();
            await UILoop(producer);

            producer.Stop();
            await producerTask;

            await endpoint.Stop()
                .ConfigureAwait(false);
        }

        static async Task UILoop(MessageProducer producer)
        {
            while (true)
            {
                var key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.Escape:
                    case ConsoleKey.Q:
                        return;
                    case ConsoleKey.UpArrow:
                        producer.Faster();
                        break;
                    case ConsoleKey.DownArrow:
                        producer.Slower();
                        break;
                    case ConsoleKey.S:
                        await producer.Spike(25);
                        break;
                    case ConsoleKey.P:
                        await producer.Pause();
                        break;
                }
            }
        }

        class MessageProducer
        {
            private IEndpointInstance endpoint;
            private int currentOrderId;
            private bool running = true;
            private int messagesPerSecond = 1;
            private bool paused;

            public MessageProducer(IEndpointInstance endpoint)
            {
                this.endpoint = endpoint;
            }

            public async Task Run()
            {
                while(running)
                {
                    if (!paused)
                    {
                        await SendNextMessage().ConfigureAwait(false);
                    }

                    var delay = 1000 / messagesPerSecond;
                    await Task.Delay(delay).ConfigureAwait(false);
                }
            }

            public void Stop()
            {
                running = false;
            }

            public void Faster()
            {
                messagesPerSecond++;
                Console.WriteLine($"Messages per second: {messagesPerSecond}");
            }

            public void Slower()
            {
                messagesPerSecond = Math.Max(1, --messagesPerSecond);
                Console.WriteLine($"Messages per second: {messagesPerSecond}");
            }

            public Task Spike(int count)
            {
                var orderId = ++currentOrderId;
                Console.WriteLine($"Sending {count} PlaceOrder messages");
                var sendTasks = Enumerable.Range(1, count).Select(i => SendNextMessage(false));
                return Task.WhenAll(sendTasks);
            }

            public Task Pause()
            {
                paused = !paused;
                var label = paused ? "paused" : "resuming";
                Console.WriteLine($"Message sending {label}");
                return Task.CompletedTask;
            }

            Task SendNextMessage(bool output = true)
            {
                var orderIdNumber = Interlocked.Increment(ref currentOrderId);
                var orderId = "LoadGen-" + orderIdNumber;
                var productId = (orderIdNumber + 2) % 3 + 1;
                if (output)
                {
                    Console.WriteLine($"Sending PlaceOrder message: OrderId '{orderId}', ProductId = {productId}");
                }
                return endpoint.Send(new PlaceOrder
                {
                    OrderId = orderId,
                    ProductId = productId
                });
            }
        }
    }
}
