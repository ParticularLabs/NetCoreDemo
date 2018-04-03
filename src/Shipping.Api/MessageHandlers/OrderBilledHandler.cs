using System;
using System.Threading.Tasks;
using EShop.Messages.Events;
using NServiceBus;
using NServiceBus.Logging;

namespace Shipping.Api.MessageHandlers
{
    class OrderBilledHandler : IHandleMessages<OrderBilled>
    {
        static ILog log = LogManager.GetLogger<OrderBilledHandler>();
        public Task Handle(OrderBilled message, IMessageHandlerContext context)
        {
            log.Info("Payment has been received. Ready to ship.");
            return Task.CompletedTask;
        }
    }
}
