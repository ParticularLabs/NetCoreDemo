namespace Warehouse.Azure.UI.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using NServiceBus;

    public class ProductController : Controller
    {
        readonly IMessageSession messageSession;

        public ProductController(IMessageSession messageSession)
        {
            this.messageSession = messageSession;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> AddStock(int id)
        {
            var message = new ItemStockUpdated
            {
                ProductId = id,
                IsAvailable = true
            };

            await messageSession.Publish(message)
                .ConfigureAwait(false);

            return View("Restock");
        }

        public async Task<IActionResult> RemoveStock(int id)
        {
            var message = new ItemStockUpdated
            {
                ProductId = id,
                IsAvailable = false
            };

            await messageSession.Publish(message)
                .ConfigureAwait(false);

            return View("Restock");
        }
    }
}
