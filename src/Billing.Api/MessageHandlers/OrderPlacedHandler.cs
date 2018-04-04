using NServiceBus.Logging;

namespace Billing.Api.MessageHandlers
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
            await Task.Delay(random.Next(250, 750));

            log.Info("A new order has been placed, make sure the payment goes through.");
            await context.Publish(new OrderBilled() {ProductId = message.ProductId});
        }
    }
}
