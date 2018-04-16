using System;
using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Logging;
using NServiceBus.MessageMutator;

namespace Warehouse.Subscriber.Azure
{
    class Program
    {
        static ILog log = LogManager.GetLogger(typeof(Program));

        static async Task Main(string[] args)
        {
            var endpointConfiguration = new EndpointConfiguration("Warehouse-Subscriber");
            endpointConfiguration.SendFailedMessagesTo("error");
            var asqConnectionString = Environment.GetEnvironmentVariable("NetCoreDemoAzureStorageQueueTransport");
          
            if (string.IsNullOrEmpty(asqConnectionString))
            {
                log.Info("Using Learning Transport");
                endpointConfiguration.UseTransport<LearningTransport>();

                // Persistence Configuration
                endpointConfiguration.UsePersistence<LearningPersistence>();
            }
            else
            {
                log.Info("Using Azure Storage Queue Transport");
                endpointConfiguration.UseTransport<AzureStorageQueueTransport>()
                    .ConnectionString(asqConnectionString);

                // Persistence Configuration
                endpointConfiguration.UsePersistence<InMemoryPersistence>();
            }

            endpointConfiguration.UseSerialization<NewtonsoftSerializer>();
            endpointConfiguration.RegisterMessageMutator(new RemoveAssemblyInfoFromMessageMutator());
            endpointConfiguration.EnableInstallers();

            var endpointInstance = await Endpoint.Start(endpointConfiguration)
                .ConfigureAwait(false);

            while (true)
            {
                var key = Console.ReadKey();
                log.Info("Press Enter to exit ...");

                if (key.Key == ConsoleKey.Enter)
                {
                    break;
                }
            }

            await endpointInstance.Stop()
                .ConfigureAwait(false);
        }
    }
}
