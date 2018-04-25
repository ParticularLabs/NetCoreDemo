namespace Sales.ViewModelComposition
{
    using System.Net.Http;
    using System.Threading.Tasks;
    using ITOps.ViewModelComposition;
    using ITOps.ViewModelComposition.Json;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Routing;

    public class ProductPriceGetHandler : IHandleRequests
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
            //invoke Sales back-end API to retrieve sales related product details
            var id = (string) routeData.Values["id"];

            var url = $"http://localhost:50687/product/{id}";
            var client = new HttpClient();
            var response = await client.GetAsync(url);

            dynamic productPrice = await response.Content.AsExpandoAsync();
            vm.Price = productPrice.price;
        }
    }
}