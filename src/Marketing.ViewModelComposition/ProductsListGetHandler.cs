namespace Marketing.ViewModelComposition
{
    using System.Collections.Generic;
    using System.Dynamic;
    using System.Net.Http;
    using System.Threading.Tasks;
    using ITOps.ViewModelComposition;
    using ITOps.ViewModelComposition.Json;
    using Marketing.Events.ViewModelComposition;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Routing;

    internal class ProductsListGetHandler : IHandleRequests
    {
        public bool Matches(RouteData routeData, string httpVerb, HttpRequest request)
        {
            var controller = (string) routeData.Values["controller"];
            var action = (string) routeData.Values["action"];

            return HttpMethods.IsGet(httpVerb)
                   && controller.ToLowerInvariant() == "products"
                   && action.ToLowerInvariant() == "index";
        }

        public async Task Handle(dynamic vm, RouteData routeData, HttpRequest request)
        {
            //invoke Marketing back-end API to retrieve the current products
            var url = $"http://localhost:50688/product/";
            var client = new HttpClient();
            var response = await client.GetAsync(url);
            dynamic products = await response.Content.AsExpandoArrayAsync();

            // Create a dictionary that's keyed by OrderId. 
            var orderDictionary = MapToViewModelDictionary(products);

            // Raise an event so that other views that need t
            // enrich the view with more data related to each OrderId .  
            await vm.RaiseEventAsync(new ProductsLoaded {OrdersDictionary = orderDictionary});

            // Store the enriched data in the viewmodel.
            vm.Products = orderDictionary.Values;
        }

        IDictionary<dynamic, dynamic> MapToViewModelDictionary(dynamic[] products)
        {
            var dictionary = new Dictionary<dynamic, dynamic>();

            foreach (var product in products)
            {
                dynamic productDetailObject = new ExpandoObject();
                productDetailObject.productId = product.productId;
                productDetailObject.name = product.name;
                productDetailObject.imageUrl = product.imageUrl;
                dictionary[product.productId] = productDetailObject;
            }

            return dictionary;
        }
    }
}