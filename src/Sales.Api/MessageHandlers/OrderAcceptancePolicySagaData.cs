namespace Sales.Api.MessageHandlers
{
    using NServiceBus;

    public class OrderAcceptancePolicySagaData : ContainSagaData
    {
        public string OrderId { get; set; }
        public int ProductId { get; set; }
    }
}