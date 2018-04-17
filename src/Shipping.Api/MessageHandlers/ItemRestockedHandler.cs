namespace Shipping.Api.MessageHandlers
{
    using System.Threading.Tasks;
    using NServiceBus;
    using NServiceBus.Logging;
    using Warehouse.Azure;

    public class ItemRestockedHandler : IHandleMessages<ItemRestocked>
    {
        static ILog log = LogManager.GetLogger<ItemRestocked>();

        public Task Handle(ItemRestocked message, IMessageHandlerContext context)
        {
            log.Info($"{message.ProductId}");

            return Task.CompletedTask;
        }
    }
}