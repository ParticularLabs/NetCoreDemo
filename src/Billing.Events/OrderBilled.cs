namespace Billing.Events
{
    using NServiceBus;

    public class OrderBilled : IEvent
    {
        public string OrderId { get; set; }
        public int ProductId { get; set; }
    }
}