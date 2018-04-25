namespace Sales.ViewModelComposition
{
    using System.Threading.Tasks;
    using ITOps.ViewModelComposition;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Routing;
    using NServiceBus;
    using Sales.Internal;

    public class CancelOrderPostHandler : IHandleRequests
    {
        private readonly IMessageSession session;

        public CancelOrderPostHandler(IMessageSession messageSession)
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
                   && controller.ToLowerInvariant() == "orders"
                   && action.ToLowerInvariant() == "cancelorder"
                   && routeData.Values.ContainsKey("id");
        }

        public async Task Handle(dynamic vm, RouteData routeData, HttpRequest request)
        {
            var orderId = (string) routeData.Values["id"];

            await session.Send(new CancelOrder
            {
                OrderId = orderId
            });
        }
    }
}