using NServiceBus;

namespace Sales.Internal
{
    public class PlaceOrder : ICommand
    {
        public string OrderId { get; set; }
        public int ProductId { get; set; }
    }
}
