namespace EShop.Messages.Events
{
    using NServiceBus;
    public class OrderCancelled : IEvent
    {
        public string OrderId { get; set; }
    }
}
