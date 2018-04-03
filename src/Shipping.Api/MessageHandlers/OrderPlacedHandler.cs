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
        public async Task Handle(OrderPlaced message, IMessageHandlerContext context)
        {
            log.Info("A new order has been placed, prepare the inventory for shipping");
            await context.Publish(new OrderBilled() { ProductId = message.ProductId });
        }
    }
}
