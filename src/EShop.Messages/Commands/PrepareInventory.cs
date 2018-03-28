namespace EShop.Messages.Commands
{
    using NServiceBus;
    public class PrepareInventory : ICommand
    {
        public int ProductId { get; set; }
    }
}
