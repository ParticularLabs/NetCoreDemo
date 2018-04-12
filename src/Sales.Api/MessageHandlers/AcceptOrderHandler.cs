using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EShop.Messages.Commands;
using EShop.Messages.Events;
using NServiceBus;
using Sales.Api.Data;

namespace Sales.Api.MessageHandlers
{
    public class AcceptOrderHandler : IHandleMessages<AcceptOrder>
    {
        private readonly SalesDbContext dbContext;

        public AcceptOrderHandler(SalesDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task Handle(AcceptOrder message, IMessageHandlerContext context)
        {
            // Find Order and update the database.
            var order = dbContext.OrderDetails.First(m => m.OrderId == message.OrderId);
            order.IsOrderAccepted = true;
            await dbContext.SaveChangesAsync().ConfigureAwait(false);
            
            // Publish event
            await context.Publish(new OrderPlaced()
            {
                OrderId = message.OrderId
            }).ConfigureAwait(false);
        }
    }
}
