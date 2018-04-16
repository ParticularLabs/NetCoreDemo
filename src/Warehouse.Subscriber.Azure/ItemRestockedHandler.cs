using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Logging;
using Warehouse.Azure;

namespace Warehouse.Subscriber.Azure
{
    class ItemRestockedHandler : IHandleMessages<ItemRestocked>
    {
        ILog log = LogManager.GetLogger(typeof(ItemRestockedHandler));

        public Task Handle(ItemRestocked message, IMessageHandlerContext context)
        {
            log.Info($"Received the ItemRestocked event for {message.OrderId}");
            return Task.CompletedTask;
        }
    }
}
