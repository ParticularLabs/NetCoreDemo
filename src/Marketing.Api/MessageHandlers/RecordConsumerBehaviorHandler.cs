namespace Marketing.Api.MessageHandlers
{
    using System;
    using System.Threading.Tasks;
    using NServiceBus;
    using EShop.Messages.Commands;

    public class RecordConsumerBehaviorHandler : IHandleMessages<RecordConsumerBehavior>
    {
        public Task Handle(RecordConsumerBehavior message, IMessageHandlerContext context)
        {
            Console.WriteLine("Perform marketing campaign.");
            return Task.CompletedTask;
        }
    }
}
