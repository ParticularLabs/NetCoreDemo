namespace Shipping.Api.MessageHandlers
{
    using System.Threading.Tasks;
    using Billing.Events;
    using NServiceBus;
    using NServiceBus.Logging;
    using Sales.Events;

    public class OrderShipmentSaga : Saga<OrderShipmentSagaData>,
        IAmStartedByMessages<OrderBilled>,
        IAmStartedByMessages<OrderAccepted>
    {
        private static readonly ILog log = LogManager.GetLogger<OrderShipmentSaga>();

        public Task Handle(OrderAccepted message, IMessageHandlerContext context)
        {
            log.Info($"Order '{message.OrderId}' has been accepted. Prepare inventory ready for shipping");
            Data.IsOrderAccepted = true;
            CompleteSagaIfBothEventsReceived();
            return Task.CompletedTask;
        }

        public Task Handle(OrderBilled message, IMessageHandlerContext context)
        {
            log.Info($"Order '{message.OrderId}' has been billed.");
            Data.IsOrderBilled = true;
            CompleteSagaIfBothEventsReceived();
            return Task.CompletedTask;
        }

        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<OrderShipmentSagaData> mapper)
        {
            mapper.ConfigureMapping<OrderBilled>(message => message.OrderId)
                .ToSaga(sagaData => sagaData.OrderId);

            mapper.ConfigureMapping<OrderAccepted>(message => message.OrderId)
                .ToSaga(sagaData => sagaData.OrderId);
        }

        public Task CompleteSagaIfBothEventsReceived()
        {
            if (Data.IsOrderBilled && Data.IsOrderAccepted)
            {
                log.Info(
                    $"Order '{Data.OrderId}' is ready to ship as both OrderAccepted and OrderBilled events has been received.");
                MarkAsComplete();
            }

            return Task.CompletedTask;
        }
    }
}