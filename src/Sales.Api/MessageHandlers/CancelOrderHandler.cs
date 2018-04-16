namespace Sales.Api.MessageHandlers
{
    using System.Linq;
    using Sales.Api.Data;
    using System.Threading.Tasks;
    using EShop.Messages.Commands;
    using NServiceBus;
    public class CancelOrderHandler : IHandleMessages<CancelOrder>
    {
        private readonly SalesDbContext dbContext;

        public CancelOrderHandler(SalesDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task Handle(CancelOrder message, IMessageHandlerContext context)
        {
            // Find Order and update the database.
            var order = dbContext.OrderDetails.First(m => m.OrderId == message.OrderId);
            order.IsOrderCancelled = true;
            await dbContext.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
