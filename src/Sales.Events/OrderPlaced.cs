using NServiceBus;

namespace Sales.Events
{
    public class OrderPlaced : IEvent
    {
        public string OrderId { get; set; }
        public int ProductId { get; set; }
    }
}
