namespace Warehouse.Azure
{
    using System;
    using System.Threading.Tasks;
    using NServiceBus;
    using NServiceBus.Logging;

    class Program
    {
        static ILog log = LogManager.GetLogger(typeof(Program));

        static async Task Main(string[] args)
        {
            var endpointConfiguration = new EndpointConfiguration("Warehouse");
            endpointConfiguration.SendFailedMessagesTo("error");

            var asqConnectionString = Environment.GetEnvironmentVariable("NetCoreDemoAzureStorageQueueTransport");
            if (string.IsNullOrEmpty(asqConnectionString))
            {
                log.Info("Connection for Azure Storage Queue transport is missing or empty.");
            }
            
            log.Info("Using Azure Storage Queue Transport");
            var transport = endpointConfiguration.UseTransport<AzureStorageQueueTransport>()
                .ConnectionString(asqConnectionString);

            // Persistence Configuration
            endpointConfiguration.UsePersistence<InMemoryPersistence>();

            endpointConfiguration.UseSerialization<NewtonsoftSerializer>();
            endpointConfiguration.EnableInstallers();

            var endpointInstance = await Endpoint.Start(endpointConfiguration)
                .ConfigureAwait(false);

            log.Info("Press Enter to publish the ItemRestocked event ...");

            while (true)
            {
                var key = Console.ReadKey();

                if (key.Key != ConsoleKey.Enter)
                {
                    break;
                }

                var message = new ItemRestocked()
                {
                    ProductId = "3"
                };
                await endpointInstance.Publish(message)
                    .ConfigureAwait(false);
                log.Info("Published message ItemRestocked for EShop-1");
            }
            await endpointInstance.Stop()
                .ConfigureAwait(false);
        }
    }
}
