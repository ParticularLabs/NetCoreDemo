
using NServiceBus;

namespace Warehouse.Azure
{
    public class ItemRestocked : IEvent
    {
        public int ProductId { get; set; }
    }
}
