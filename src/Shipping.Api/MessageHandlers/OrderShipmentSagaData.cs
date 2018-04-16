namespace Shipping.Api.MessageHandlers
{
    using NServiceBus;
    public class OrderShipmentSagaData : ContainSagaData
    {
        public string OrderId { get; set; }
        public bool IsOrderAccepted { get; set; }

        public bool IsOrderBilled { get; set; }
    }
}
