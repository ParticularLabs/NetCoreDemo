namespace Marketing.Api.MessageHandlers
{
    using System;
    using System.Threading.Tasks;
    using NServiceBus;
    using EShop.Messages.Events;

    public class OrderPlacedHandler : IHandleMessages<OrderPlaced>
    {
        public Task Handle(OrderPlaced message, IMessageHandlerContext context)
        {
            Console.WriteLine("Don't do what Facebook did!");
            return Task.CompletedTask;
        }
    }
}
