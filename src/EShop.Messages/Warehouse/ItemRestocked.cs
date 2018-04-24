// This is the exact message schema that is defined for the ItemRestocked event from the other team, i.e. Warehouse.Azure endpoint. 
// This class is declared locally, so we don't need to reference an assembly DLL for message schema.

namespace Warehouse.Azure
{
    using NServiceBus;

    public class ItemRestocked : IEvent
    {
        public int ProductId { get; set; }
    }
}