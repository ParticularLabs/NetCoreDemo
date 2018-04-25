namespace Marketing.Api.MessageHandlers
{
    using System;
    using System.Threading.Tasks;
    using Marketing.Internal;
    using NServiceBus;
    using NServiceBus.Logging;

    public class RecordConsumerBehaviorHandler : IHandleMessages<RecordConsumerBehavior>
    {
        private static readonly ILog log = LogManager.GetLogger<RecordConsumerBehaviorHandler>();
        private static readonly Random random = new Random();

        public async Task Handle(RecordConsumerBehavior message, IMessageHandlerContext context)
        {
            // Simulate some work
            await Task.Delay(random.Next(25, 50));

            log.Info("Perform marketing campaign.");
        }
    }
}