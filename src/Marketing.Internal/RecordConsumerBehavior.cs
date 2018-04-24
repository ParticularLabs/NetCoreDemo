using NServiceBus;

namespace Marketing.Internal
{
    public class RecordConsumerBehavior : ICommand
    {
        public int ProductId { get; set; }
    }
}
