using NServiceBus;

namespace Sales.Events
{
    public class OrderCancelled : IEvent
    {
        public string OrderId { get; set; }
    }
}
