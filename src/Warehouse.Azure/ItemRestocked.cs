
using NServiceBus;

namespace Warehouse.Azure
{
    public class ItemRestocked : IEvent
    {
        public string ProductId { get; set; }
    }
}
