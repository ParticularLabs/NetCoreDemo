namespace Sales.Api.MessageHandlers
{
    using System.Linq;
    using System.Threading.Tasks;
    using NServiceBus;
    using Sales.Api.Data;
    using Sales.Internal;

    public class CancelOrderHandler : IHandleMessages<CancelOrder>
    {
        readonly SalesDbContext dbContext;

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