using NServiceBus.Logging;

namespace Marketing.Api.MessageHandlers
{
    using System;
    using System.Threading.Tasks;
    using NServiceBus;
    using EShop.Messages.Commands;

    public class RecordConsumerBehaviorHandler : IHandleMessages<RecordConsumerBehavior>
    {
        static ILog log = LogManager.GetLogger<RecordConsumerBehaviorHandler>();
        static Random random = new Random();

        public async Task Handle(RecordConsumerBehavior message, IMessageHandlerContext context)
        {
            // Simulate some work
            await Task.Delay(random.Next(25, 50));

            log.Info("Perform marketing campaign.");
        }
    }
}
