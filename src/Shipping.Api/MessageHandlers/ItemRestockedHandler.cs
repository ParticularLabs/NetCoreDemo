namespace Shipping.Api.MessageHandlers
{
    using System.Linq;
    using System.Threading.Tasks;
    using NServiceBus;
    using NServiceBus.Logging;
    using Shipping.Api.Data;
    using Warehouse.Azure;

    public class ItemRestockedHandler : IHandleMessages<ItemRestocked>
    {
        static readonly ILog log = LogManager.GetLogger<ItemRestocked>();
        readonly StockItemDbContext dbContext;

        public ItemRestockedHandler(StockItemDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task Handle(ItemRestocked message, IMessageHandlerContext context)
        {
            log.Info($"Product with ID '{message.ProductId}' is now available.");

            var stockItem = dbContext.StockItems.First(x => x.ProductId == message.ProductId);
            stockItem.InStock = true;
            await dbContext.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}