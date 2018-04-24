using Marketing.Events.ViewModelComposition;

namespace Shipping.ViewModelComposition
{
    using System.Linq;
    using System.Collections.Generic;
    using System.Net.Http;
    using ITOps.ViewModelComposition;
    using ITOps.ViewModelComposition.Json;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Routing;

    class ProductListGetHandler : ISubscribeToViewModelCompositionEvent
    {
        public bool Matches(RouteData routeData, string httpVerb, HttpRequest request)
        {
            var controller = (string)routeData.Values["controller"];
            var action = (string)routeData.Values["action"];

            return HttpMethods.IsGet(httpVerb)
                   && controller.ToLowerInvariant() == "products"
                   && action.ToLowerInvariant() == "index";
        }

        public void RegisterCallback(DynamicViewModel viewModel)
        {
            viewModel.RegisterCallback<ProductsLoaded>(async (pageViewModel, eventData, routeData, query) =>
            {
                var productIds = string.Join(",", eventData.OrdersDictionary.Keys.ToArray());
                var url = $"http://localhost:50686/product?productIds={productIds}";
                var client = new HttpClient();
                var response = await client.GetAsync(url);
                dynamic productStockList = await response.Content.AsExpandoArrayAsync();

                foreach (var productId in eventData.OrdersDictionary.Keys)
                {
                    var product = eventData.OrdersDictionary[productId];

                    // For each product, fill in the price information. 
                    var stockInfo = ((IEnumerable<dynamic>)productStockList).First(p => p.productId == productId);
                    product.inStock = stockInfo.inStock;
                }
            });
        }
    }
}
