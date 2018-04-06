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
            await Task.Delay(random.Next(25, 50));

            log.Info($"Order '{message.OrderId}' has been placed, make sure the payment goes through.");

            await ThisIsntGoingToScaleWell();

            await context.Publish(new OrderBilled()
            {
                OrderId = message.OrderId,
                ProductId = message.ProductId
            });
        }

        private Task ThisIsntGoingToScaleWell()
        {
            return Task.Delay(random.Next(250, 350));
        }
    }
}
