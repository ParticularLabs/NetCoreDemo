using System.Threading.Tasks;

namespace Warehouse.Api.MessageHandlers
{
    using System;
    using NServiceBus;
    using EShop.Messages.Events;
    class OrderBilledHandler : IHandleMessages<OrderBilled>
    {
        public Task Handle(OrderBilled message, IMessageHandlerContext context)
        {
            Console.WriteLine("Payment has been received. Ready to ship.");
            return Task.CompletedTask;
        }
    }
}
