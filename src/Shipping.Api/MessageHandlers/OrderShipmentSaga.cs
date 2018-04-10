namespace Shipping.Api.MessageHandlers
{
    using System.Threading.Tasks;
    using EShop.Messages.Events;
    using NServiceBus;
    using NServiceBus.Logging;

    public class OrderShipmentSaga : Saga<OrderShipmentSagaData>, 
        IAmStartedByMessages<OrderBilled>, 
        IAmStartedByMessages<OrderPlaced>
    {
        static ILog log = LogManager.GetLogger<OrderShipmentSaga>();
        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<OrderShipmentSagaData> mapper)
        {
            mapper.ConfigureMapping<OrderBilled>(message => message.OrderId)
                .ToSaga(sagaData => sagaData.OrderId);

            mapper.ConfigureMapping<OrderPlaced>(message => message.OrderId)
                .ToSaga(sagaData => sagaData.OrderId);
        }

        public Task Handle(OrderBilled message, IMessageHandlerContext context)
        {
            log.Info($"Order '{message.OrderId}' has been billed.");
            Data.IsOrderBilled = true;
            CompleteSagaIfBothEventsReceived();
            return Task.CompletedTask;
        }

        public Task Handle(OrderPlaced message, IMessageHandlerContext context)
        {
            log.Info($"Order '{message.OrderId}' has been placed.");
            Data.IsOrderReceived = true;
            CompleteSagaIfBothEventsReceived();
            return Task.CompletedTask;
        }

        public Task CompleteSagaIfBothEventsReceived()
        {
            if (Data.IsOrderBilled && Data.IsOrderReceived)
            {
                log.Info($"Order '{Data.OrderId}' is ready to ship as both OrderPlaced and OrderBilled events has been received.");
                MarkAsComplete();
            }

            return Task.CompletedTask;
        }
    }
}
