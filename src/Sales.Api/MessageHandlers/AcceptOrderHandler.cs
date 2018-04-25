namespace Sales.Api.MessageHandlers
{
    using System.Linq;
    using System.Threading.Tasks;
    using NServiceBus;
    using Sales.Api.Data;
    using Sales.Events;
    using Sales.Internal;

    public class AcceptOrderHandler : IHandleMessages<AcceptOrder>
    {
        readonly SalesDbContext dbContext;

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
            await context.Publish(new OrderAccepted
            {
                OrderId = message.OrderId,
                ProductId = message.ProductId
            }).ConfigureAwait(false);
        }
    }
}