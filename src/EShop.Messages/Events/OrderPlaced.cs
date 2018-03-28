namespace EShop.Messages.Events
{
    using NServiceBus;
    public class OrderPlaced : IEvent
    {
        public int ProductId { get; set; }
    }
}
