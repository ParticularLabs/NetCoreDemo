namespace Sales.Api.MessageHandlers
{
    using System;
    using System.Threading.Tasks;
    using NServiceBus;
    using NServiceBus.Logging;
    using Sales.Events;
    using Sales.Internal;

    public class OrderAcceptancePolicySaga : Saga<OrderAcceptancePolicySagaData>,
        IAmStartedByMessages<PlaceOrder>,
        IAmStartedByMessages<CancelOrder>,
        IHandleTimeouts<BuyersRemorseIsOver>
    {
        private static readonly ILog log = LogManager.GetLogger<OrderAcceptancePolicySaga>();

        public Task Handle(CancelOrder message, IMessageHandlerContext context)
        {
            log.Info($"Received the CancelOrder command for {message.OrderId}. Cancelling the order.");
            MarkAsComplete();
            var orderCancelled = new OrderCancelled
            {
                OrderId = message.OrderId
            };
            return context.Publish(orderCancelled);
        }

        public async Task Handle(PlaceOrder message, IMessageHandlerContext context)
        {
            log.Info(
                $"Received the PlaceOrder command for {message.OrderId}. Wait for the grace period to see if the user cancels order.");
            Data.ProductId = message.ProductId;
            Data.OrderId = message.OrderId;
            await context.Send(new StoreOrder
            {
                OrderId = message.OrderId,
                OrderPlacedOn = DateTime.UtcNow,
                ProductId = message.ProductId
            }).ConfigureAwait(false);
            await RequestTimeout(context, TimeSpan.FromSeconds(20), new BuyersRemorseIsOver()).ConfigureAwait(false);
        }

        public Task Timeout(BuyersRemorseIsOver state, IMessageHandlerContext context)
        {
            log.Info("Grace time to cancel order has elapsed. Order is being placed for fulfillment.-");
            MarkAsComplete();
            return context.Send(new AcceptOrder
            {
                OrderId = Data.OrderId,
                ProductId = Data.ProductId
            });
        }

        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<OrderAcceptancePolicySagaData> mapper)
        {
            mapper.ConfigureMapping<PlaceOrder>(message => message.OrderId)
                .ToSaga(sagaData => sagaData.OrderId);

            mapper.ConfigureMapping<CancelOrder>(message => message.OrderId)
                .ToSaga(sagaData => sagaData.OrderId);
        }
    }
}