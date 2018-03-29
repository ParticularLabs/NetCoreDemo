using System;
using System.Threading.Tasks;
using EShop.Messages.Events;
using NServiceBus;

namespace Shipping.Api.MessageHandlers
{
    class OrderBilledHandler : IHandleMessages<OrderBilled>
    {
        public Task Handle(OrderBilled message, IMessageHandlerContext context)
        {
            Console.WriteLine("Payment has been received. Ready to ship.");
            return Task.CompletedTask;
        }
    }
}
