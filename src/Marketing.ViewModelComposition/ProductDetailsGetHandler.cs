using ITOps.ViewModelComposition;
using ITOps.ViewModelComposition.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Net.Http;
using System.Threading.Tasks;

namespace Marketing.ViewModelComposition
{
    public class ProductDetailsGetHandler : IHandleRequests
    {
        public bool Matches(RouteData routeData, string httpVerb, HttpRequest request)
        {
            //determine if the incoming request should 
            //be composed with Marketing data, e.g.
            var controller = (string)routeData.Values["controller"];
            var action = (string)routeData.Values["action"];

            return HttpMethods.IsGet(httpVerb)
                && controller.ToLowerInvariant() == "products"
                && action.ToLowerInvariant()== "details"
                && routeData.Values.ContainsKey("id");
        }

        public async Task Handle(dynamic vm, RouteData routeData, HttpRequest request)
        {
            //invoke Marketing back-end API to retrieve marketing related product details
            var id = (string)routeData.Values["id"];

            var url = $"http://localhost:50688/product/{id}";
            var client = new HttpClient();
            var response = await client.GetAsync(url);

            dynamic productDetails = await response.Content.AsExpandoAsync();

            vm.ProductName = productDetails.name;
            vm.ProductDescription = productDetails.description;
            vm.ProductId = productDetails.productId;
        }
    }
}
