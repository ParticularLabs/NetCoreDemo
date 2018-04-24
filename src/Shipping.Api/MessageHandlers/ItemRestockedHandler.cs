using System.Linq;
using Shipping.Api.Data;

namespace Shipping.Api.MessageHandlers
{
    using System.Threading.Tasks;
    using NServiceBus;
    using NServiceBus.Logging;
    using Warehouse.Azure;

    public class ItemRestockedHandler : IHandleMessages<ItemRestocked>
    {
        static ILog log = LogManager.GetLogger<ItemRestocked>();
        private readonly StockItemDbContext dbContext;

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