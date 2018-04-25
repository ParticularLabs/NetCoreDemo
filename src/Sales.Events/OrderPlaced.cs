namespace Sales.Events
{
    using NServiceBus;

    public class OrderPlaced : IEvent
    {
        public string OrderId { get; set; }
        public int ProductId { get; set; }
    }
}