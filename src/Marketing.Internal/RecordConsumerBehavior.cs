namespace Marketing.Internal
{
    using NServiceBus;

    public class RecordConsumerBehavior : ICommand
    {
        public int ProductId { get; set; }
    }
}