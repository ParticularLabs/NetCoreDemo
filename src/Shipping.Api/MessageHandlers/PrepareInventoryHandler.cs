using System;
using System.Threading.Tasks;
using EShop.Messages.Commands;
using NServiceBus;

namespace Shipping.Api.MessageHandlers
{
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
