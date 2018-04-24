using System;
using System.Threading;
using ITOps.ViewModelComposition;
using NServiceBus;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Sales.Internal;

namespace Sales.ViewModelComposition
{
    public class BuyItemPostHandler : IHandleRequests
    {
        private IMessageSession session;
        private static int orderIdCounter;

        public BuyItemPostHandler(IMessageSession messageSession)
        {
            session = messageSession;
        }
        public bool Matches(RouteData routeData, string httpVerb, HttpRequest request)
        {
            //determine if the incoming request should 
            //be composed with Marketing data, e.g.
            var controller = (string)routeData.Values["controller"];
            var action = (string)routeData.Values["action"];

            return HttpMethods.IsPost(httpVerb)
                   && controller.ToLowerInvariant() == "products"
                   && action.ToLowerInvariant() == "buyitem"
                   && routeData.Values.ContainsKey("id");
        }

        public async Task Handle(dynamic vm, RouteData routeData, HttpRequest request)
        {
            var productId = (string)routeData.Values["id"];
            var orderId = Interlocked.Increment(ref orderIdCounter);

            await session.Send(new PlaceOrder
            {
                OrderId = "EShop-" + orderId,
                ProductId = Int32.Parse(productId)
            });
        }
    }
}
