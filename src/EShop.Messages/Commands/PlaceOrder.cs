namespace EShop.Messages.Commands
{
    using NServiceBus;

    public class PlaceOrder : ICommand
    {
        public string OrderId { get; set; }
        public int ProductId { get; set; }
    }
}
