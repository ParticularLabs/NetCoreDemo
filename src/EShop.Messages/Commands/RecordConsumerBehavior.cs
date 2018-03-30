namespace EShop.Messages.Commands
{
    using NServiceBus;
    public class RecordConsumerBehavior : ICommand
    {
        public int ProductId { get; set; }
    }
}
