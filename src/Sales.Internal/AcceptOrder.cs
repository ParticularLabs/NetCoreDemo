using NServiceBus;

namespace Sales.Internal
{
    public class AcceptOrder : ICommand
    {
        public string OrderId { get; set; }
        public int ProductId { get; set; }
    }
}
