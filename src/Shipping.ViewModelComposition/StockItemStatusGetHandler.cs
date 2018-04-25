namespace Shipping.ViewModelComposition
{
    using System.Net.Http;
    using System.Threading.Tasks;
    using ITOps.ViewModelComposition;
    using ITOps.ViewModelComposition.Json;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Routing;

    public class StockItemStatusGetHandler : IHandleRequests
    {
        public bool Matches(RouteData routeData, string httpVerb, HttpRequest request)
        {
            var controller = (string) routeData.Values["controller"];
            var action = (string) routeData.Values["action"];

            return HttpMethods.IsGet(httpVerb)
                   && controller.ToLowerInvariant() == "products"
                   && action.ToLowerInvariant() == "details"
                   && routeData.Values.ContainsKey("id");
        }

        public async Task Handle(dynamic vm, RouteData routeData, HttpRequest request)
        {
            //invoke Warehouse back-end API to retrieve inventory related details
            var id = (string) routeData.Values["id"];

            var url = $"http://localhost:50686/product/{id}";
            var client = new HttpClient();
            var response = await client.GetAsync(url);

            dynamic stockStatus = await response.Content.AsExpandoAsync();
            vm.InStock = stockStatus.inStock;
        }
    }
}