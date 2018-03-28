using System.Threading.Tasks;

namespace Warehouse.Api.MessageHandlers
{
    using EShop.Messages.Commands;
    using NServiceBus;
    public class PrepareInventoryHandler : IHandleMessages<PrepareInventory>
    {
        public Task Handle(PrepareInventory message, IMessageHandlerContext context)
        {
            // Do something meaningful
            return Task.CompletedTask;
        }
    }
}
