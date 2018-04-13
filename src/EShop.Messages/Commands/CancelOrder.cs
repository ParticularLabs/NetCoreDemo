namespace EShop.Messages.Commands
{
    using NServiceBus;

    public class CancelOrder : ICommand
    {
        public string OrderId { get; set; }
    }
}
