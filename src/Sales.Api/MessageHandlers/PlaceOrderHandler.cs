using System;
using EShop.Messages.Events;

namespace Sales.Api.MessageHandlers
{
    using System.Threading.Tasks;
    using EShop.Messages.Commands;
    using NServiceBus;

    public class PlaceOrderHandler : IHandleMessages<PlaceOrder>
    {
        public async Task Handle(PlaceOrder message, IMessageHandlerContext context)
        {
            // Do something meaningful
            Console.WriteLine("Order has been received. Do something meaningful");
            await context.Publish(new OrderPlaced() {ProductId = message.ProductId});
        }
    }
}
