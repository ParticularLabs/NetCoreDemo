namespace Billing.Api.MessageHandlers
{
    using System;
    using System.Threading.Tasks;
    using Billing.Events;
    using NServiceBus;
    using NServiceBus.Logging;
    using Sales.Events;

    public class OrderAcceptedHandler : IHandleMessages<OrderAccepted>
    {
        static readonly ILog log = LogManager.GetLogger<OrderAcceptedHandler>();
        static readonly Random random = new Random();

        public async Task Handle(OrderAccepted message, IMessageHandlerContext context)
        {
            // Simulate some work
            await Task.Delay(random.Next(25, 50));

            log.Info($"Order '{message.OrderId}' has been accepted, make sure the payment goes through.");

            await ThisIsntGoingToScaleWell();

            await context.Publish(new OrderBilled
            {
                OrderId = message.OrderId,
                ProductId = message.ProductId
            });
        }

        Task ThisIsntGoingToScaleWell()
        {
            return Task.Delay(random.Next(250, 350));
        }
    }
}