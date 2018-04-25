namespace Marketing.ViewModelComposition
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using ITOps.ViewModelComposition;
    using ITOps.ViewModelComposition.Json;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Routing;
    using Sales.Events.ViewModelComposition;

    internal class OrdersListGetHandler : ISubscribeToViewModelCompositionEvent
    {
        public bool Matches(RouteData routeData, string httpVerb, HttpRequest request)
        {
            var controller = (string) routeData.Values["controller"];
            var action = (string) routeData.Values["action"];

            return HttpMethods.IsGet(httpVerb)
                   && controller.ToLowerInvariant() == "orders"
                   && action.ToLowerInvariant() == "index";
        }

        public void RegisterCallback(DynamicViewModel viewModel)
        {
            viewModel.RegisterCallback<OrdersLoaded>(async (pageViewModel, eventData, routeData, query) =>
            {
                var orderIds = string.Join(",", eventData.OrdersDictionary.Keys.ToArray());
                var url = $"http://localhost:50688/product/order?orderIds={orderIds}";
                var client = new HttpClient();
                var response = await client.GetAsync(url);
                dynamic productList = await response.Content.AsExpandoArrayAsync();

                foreach (var orderId in eventData.OrdersDictionary.Keys)
                {
                    var order = eventData.OrdersDictionary[orderId];

                    // For each order, fill in the product details. 
                    var productDetail =
                        ((IEnumerable<dynamic>) productList).First(product => product.productId == order.productId);

                    order.name = productDetail.name;
                    order.imageUrl = productDetail.imageUrl;
                }
            });
        }
    }
}