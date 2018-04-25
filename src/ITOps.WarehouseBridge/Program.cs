namespace ITOps.WarehouseBridge
{
    using System;
    using System.Threading.Tasks;
    using NServiceBus;
    using NServiceBus.Bridge;
    using NServiceBus.Configuration.AdvancedExtensibility;
    using NServiceBus.Serialization;
    using NServiceBus.Settings;

    internal class Program
    {
        private static async Task Main(string[] args)
        {
            Console.Title = "ITOps.WarehouseBridge";

            var asqConnectionString = Environment.GetEnvironmentVariable("NetCoreDemoAzureStorageQueueTransport");
            if (string.IsNullOrEmpty(asqConnectionString))
                Console.WriteLine("Connection for Azure Storage Queue transport is missing or empty.");

            var rabbitMqConnectionString = Environment.GetEnvironmentVariable("NetCoreDemoRabbitMQTransport");
            if (string.IsNullOrEmpty(asqConnectionString))
                Console.WriteLine("Connection for RabbitMQ transport is missing or empty.");

            var bridgeConfiguration = Bridge
                .Between<AzureStorageQueueTransport>("bridge-warehouse", transport =>
                {
                    transport.ConnectionString(asqConnectionString);
                    transport.SerializeMessageWrapperWith<NewtonsoftSerializer>();

                    // Workaround required for ASQ
                    var settings = transport.GetSettings();
                    var serializer = Tuple.Create(new NewtonsoftSerializer() as SerializationDefinition,
                        new SettingsHolder());
                    settings.Set("MainSerializer", serializer);
                })
                .And<RabbitMQTransport>("bridge-shipping", transport =>
                {
                    transport.ConnectionString(rabbitMqConnectionString);
                    transport.UseConventionalRoutingTopology();
                });

            bridgeConfiguration.AutoCreateQueues();
            bridgeConfiguration.UseSubscriptionPersistence(new InMemorySubscriptionStorage());

            var bridge = bridgeConfiguration.Create();

            await bridge.Start()
                .ConfigureAwait(false);

            Console.WriteLine("Press ESC key to exit");
            UILoop();

            await bridge.Stop()
                .ConfigureAwait(false);
        }

        private static void UILoop()
        {
            while (true)
            {
                var key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.Escape:
                    case ConsoleKey.Q:
                        return;
                }
            }
        }
    }
}