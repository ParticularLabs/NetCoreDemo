namespace Sales.ViewModelComposition
{
    using System.Threading;
    using System.Threading.Tasks;
    using ITOps.ViewModelComposition;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Routing;
    using NServiceBus;
    using Sales.Internal;

    public class BuyItemPostHandler : IHandleRequests
    {
        static int orderIdCounter;
        readonly IMessageSession session;

        public BuyItemPostHandler(IMessageSession messageSession)
        {
            session = messageSession;
        }

        public bool Matches(RouteData routeData, string httpVerb, HttpRequest request)
        {
            //determine if the incoming request should 
            //be composed with Marketing data, e.g.
            var controller = (string) routeData.Values["controller"];
            var action = (string) routeData.Values["action"];

            return HttpMethods.IsPost(httpVerb)
                   && controller.ToLowerInvariant() == "products"
                   && action.ToLowerInvariant() == "buyitem"
                   && routeData.Values.ContainsKey("id");
        }

        public async Task Handle(dynamic vm, RouteData routeData, HttpRequest request)
        {
            var productId = (string) routeData.Values["id"];
            var orderId = Interlocked.Increment(ref orderIdCounter);

            await session.Send(new PlaceOrder
            {
                OrderId = "EShop-" + orderId,
                ProductId = int.Parse(productId)
            });
        }
    }
}