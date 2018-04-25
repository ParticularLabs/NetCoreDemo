namespace Warehouse.Azure
{
    using NServiceBus;

    public class ItemStockUpdated : IEvent
    {
        public int ProductId { get; set; }
        public bool IsAvailable { get; set; }
    }
}
