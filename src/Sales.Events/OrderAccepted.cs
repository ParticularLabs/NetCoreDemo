using NServiceBus;

namespace Sales.Events
{
    public class OrderAccepted : IEvent
    {
        public string OrderId { get; set; }
        public int ProductId { get; set; }
    }
}
