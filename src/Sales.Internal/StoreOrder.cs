using System;
using NServiceBus;

namespace Sales.Internal
{
    public class StoreOrder : ICommand
    {
        public string OrderId { get; set; }
        public int ProductId { get; set; }
        public DateTime OrderPlacedOn { get; set; }
    }
}
