using System.Threading.Tasks;

namespace Warehouse.Api.MessageHandlers
{
    using System;
    using EShop.Messages.Commands;
    using NServiceBus;
    public class PrepareInventoryHandler : IHandleMessages<PrepareInventory>
    {
        public Task Handle(PrepareInventory message, IMessageHandlerContext context)
        {
            Console.WriteLine("Received a new order. Prepare inventory");
            // Do something meaningful
            return Task.CompletedTask;
        }
    }
}
