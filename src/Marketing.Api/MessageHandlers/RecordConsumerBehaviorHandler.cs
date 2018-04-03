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
        public Task Handle(RecordConsumerBehavior message, IMessageHandlerContext context)
        {
            log.Info("Perform marketing campaign.");
            return Task.CompletedTask;
        }
    }
}
