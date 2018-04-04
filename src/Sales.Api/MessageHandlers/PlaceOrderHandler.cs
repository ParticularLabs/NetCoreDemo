using System;
using EShop.Messages.Events;
using NServiceBus.Logging;

namespace Sales.Api.MessageHandlers
{
    using System.Threading.Tasks;
    using EShop.Messages.Commands;
    using NServiceBus;

    public class PlaceOrderHandler : IHandleMessages<PlaceOrder>
    {
        static ILog log = LogManager.GetLogger<PlaceOrderHandler>();
        static Random random = new Random();

        public async Task Handle(PlaceOrder message, IMessageHandlerContext context)
        {
            // Simulate some work
            await Task.Delay(random.Next(250, 750));

            log.Info("A new order has been received. Do something meaningful");
            await context.Publish(new OrderPlaced() {ProductId = message.ProductId});
        }
    }
}
