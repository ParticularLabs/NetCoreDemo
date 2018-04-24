using NServiceBus;

namespace Sales.Internal
{
    public class CancelOrder : ICommand
    {
        public string OrderId { get; set; }
    }
}
