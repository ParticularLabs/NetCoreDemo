namespace Sales.Internal
{
    using NServiceBus;

    public class PlaceOrder : ICommand
    {
        public string OrderId { get; set; }
        public int ProductId { get; set; }
    }
}