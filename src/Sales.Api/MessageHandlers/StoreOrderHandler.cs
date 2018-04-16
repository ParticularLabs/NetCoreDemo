using System.Linq;

namespace Sales.Api.MessageHandlers
{
    using System.Threading.Tasks;
    using NServiceBus;
    using EShop.Messages.Commands;
    using EShop.Messages.Events;
    using Sales.Api.Data;
    using Sales.Api.Models;
   
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
                OrderId = message.OrderId,
                Price = GetPriceFor(message.ProductId)
                
            }).ConfigureAwait(false);
            await dbContext.SaveChangesAsync().ConfigureAwait(false);

            // Publish event
            await context.Publish(new OrderPlaced()
            {
                OrderId = message.OrderId,
                ProductId = message.ProductId

            }).ConfigureAwait(false);
        }

        decimal GetPriceFor(int productId)
        {
           decimal price = dbContext.ProductPrices.Where(p => p.ProductId == productId)
               .Select(productPrice => productPrice.Price).First();
           return price;
        }
    }
}
