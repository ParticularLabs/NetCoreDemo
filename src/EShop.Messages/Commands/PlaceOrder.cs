namespace EShop.Messages.Commands
{
    using NServiceBus;

    public class PlaceOrder : ICommand
    {
        public int ProductId { get; set; }
    }
}
