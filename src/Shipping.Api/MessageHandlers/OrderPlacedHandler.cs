using NServiceBus.Logging;

namespace Shipping.Api.MessageHandlers
{
    using System;
    using System.Threading.Tasks;
    using NServiceBus;
    using EShop.Messages.Events;

    public class OrderPlacedHandler : IHandleMessages<OrderPlaced>
    {
        static ILog log = LogManager.GetLogger<OrderPlacedHandler>();
        static Random random = new Random();
        
        public async Task Handle(OrderPlaced message, IMessageHandlerContext context)
        {
            // Simulate some work
            await Task.Delay(random.Next(25, 50));

            log.Info($"Order '{message.OrderId}' has been placed, preparing the inventory and waiting for the payment");
        }
    }
}
