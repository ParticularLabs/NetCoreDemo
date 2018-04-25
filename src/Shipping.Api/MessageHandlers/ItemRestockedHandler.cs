namespace Shipping.Api.MessageHandlers
{
    using System.Linq;
    using System.Threading.Tasks;
    using NServiceBus;
    using NServiceBus.Logging;
    using Shipping.Api.Data;
    using Warehouse.Azure;

    public class ItemRestockedHandler : IHandleMessages<ItemStockUpdated>
    {
        static readonly ILog log = LogManager.GetLogger<ItemStockUpdated>();
        readonly StockItemDbContext dbContext;

        public ItemRestockedHandler(StockItemDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task Handle(ItemStockUpdated message, IMessageHandlerContext context)
        {
            log.Info($"Product Id: '{message.ProductId}', Availability: {message.IsAvailable}");

            var stockItem = dbContext.StockItems.First(x => x.ProductId == message.ProductId);
            stockItem.InStock = message.IsAvailable;
            await dbContext.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}