using System.Threading.Tasks;
using EShop.Messages.Commands;
using Sales.Api.Data;
using Sales.Api.Models;

namespace Sales.Api.MessageHandlers
{
    using NServiceBus;
    public class StoreOrderHandler : IHandleMessages<StoreOrder>
    {
        private readonly SalesDbContext dbContext;

        public StoreOrderHandler(SalesDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task Handle(StoreOrder message, IMessageHandlerContext context)
        {
            await dbContext.OrderDetails.AddAsync(new OrderDetail()
            {
                ProductId = message.ProductId,
                OrderPlacedOn = message.OrderPlacedOn,
                IsOrderAccepted = false,
                OrderId = message.OrderId
            }).ConfigureAwait(false);
            await dbContext.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
