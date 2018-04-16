using System;
using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Logging;

namespace Warehouse.Azure
{
    class Program
    {
        static ILog log = LogManager.GetLogger(typeof(Program));
        static async Task Main(string[] args)
        {
            var endpointConfiguration = new EndpointConfiguration("Warehouse");
            endpointConfiguration.SendFailedMessagesTo("error");
            var asqConnectionString = Environment.GetEnvironmentVariable("NetCoreDemoAzureStorageQueueTransport");
        
            if (String.IsNullOrEmpty(asqConnectionString))
            {
                log.Info("Using Learning Transport");
                endpointConfiguration.UseTransport<LearningTransport>();
                
                // Persistence Configuration
                endpointConfiguration.UsePersistence<LearningPersistence>();
            }
            else
            {
                log.Info("Using Azure Service Bus Transport");
                endpointConfiguration.UseTransport<AzureStorageQueueTransport>()
                    .ConnectionString(asqConnectionString);

                // Persistence Configuration
                endpointConfiguration.UsePersistence<InMemoryPersistence>();
            }
            endpointConfiguration.UseSerialization<NewtonsoftSerializer>();
            endpointConfiguration.EnableInstallers();

            var endpointInstance = await Endpoint.Start(endpointConfiguration)
                .ConfigureAwait(false);

            while (true)
            {
                var key = Console.ReadKey();
                log.Info("Press Enter to publish the ItemRestocked event ...");
                
                if (key.Key != ConsoleKey.Enter)
                {
                    break;
                }
                
                var message = new ItemRestocked()
                {
                    OrderId = "EShop-1"
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
