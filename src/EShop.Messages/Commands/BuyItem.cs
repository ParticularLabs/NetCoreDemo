namespace EShop.Messages.Commands
{
    using NServiceBus;

    public class BuyItem : ICommand
    {
        public int ProductId { get; set; }
    }
}
