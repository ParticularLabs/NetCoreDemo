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
        static Random random = new Random();
        
        public async Task Handle(OrderBilled message, IMessageHandlerContext context)
        {
            // Simulate some work
            await Task.Delay(random.Next(250, 750));

            log.Info("Payment has been received. Shipping.");
        }
    }
}
