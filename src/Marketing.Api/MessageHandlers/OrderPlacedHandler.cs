using Marketing.Api.Data;
using Marketing.Api.Models;
using Sales.Events;

namespace Marketing.Api.MessageHandlers
{
    using System;
    using System.Threading.Tasks;
    using NServiceBus;
    using NServiceBus.Logging;

    public class OrderPlacedHandler : IHandleMessages<OrderPlaced>
    {
        private readonly ProductDetailsDbContext dbContext;

        public OrderPlacedHandler(ProductDetailsDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        static ILog log = LogManager.GetLogger<OrderPlacedHandler>();
        public async Task Handle(OrderPlaced message, IMessageHandlerContext context)
        {
            log.Info("Storing what products just got recently placed.");
            await dbContext.OrderDetails.AddAsync(new OrderDetails()
            {
                ProductId = message.ProductId,
                OrderId = message.OrderId
            }).ConfigureAwait(false);
            await dbContext.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
