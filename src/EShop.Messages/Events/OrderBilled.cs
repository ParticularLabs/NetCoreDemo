namespace EShop.Messages.Events
{
    using NServiceBus;
    public class OrderBilled : IEvent
    {
        public int ProductId { get; set; }
    }
}