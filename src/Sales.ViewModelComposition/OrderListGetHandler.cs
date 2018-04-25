namespace Sales.ViewModelComposition
{
    using System.Collections.Generic;
    using System.Dynamic;
    using System.Net.Http;
    using System.Threading.Tasks;
    using ITOps.ViewModelComposition;
    using ITOps.ViewModelComposition.Json;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Routing;
    using Sales.Events.ViewModelComposition;

    internal class OrderListGetHandler : IHandleRequests
    {
        public bool Matches(RouteData routeData, string httpVerb, HttpRequest request)
        {
            var controller = (string) routeData.Values["controller"];
            var action = (string) routeData.Values["action"];

            return HttpMethods.IsGet(httpVerb)
                   && controller.ToLowerInvariant() == "orders"
                   && action.ToLowerInvariant() == "index";
        }

        public async Task Handle(dynamic vm, RouteData routeData, HttpRequest request)
        {
            //invoke Sales back-end API to retrieve the currently placed orders
            var url = $"http://localhost:50687/order/";
            var client = new HttpClient();
            var response = await client.GetAsync(url);
            dynamic orders = await response.Content.AsExpandoArrayAsync();

            // Create a dictionary that's keyed by OrderId. 
            var orderDictionary = MapToViewModelDictionary(orders);

            // Raise an event so that other views that need to
            // enrich the view with more data related to each OrderId .  
            await vm.RaiseEventAsync(new OrdersLoaded {OrdersDictionary = orderDictionary});

            // Store the enriched data in the viewmodel.
            vm.Orders = orderDictionary.Values;
        }

        private IDictionary<dynamic, dynamic> MapToViewModelDictionary(dynamic[] orders)
        {
            var dictionary = new Dictionary<dynamic, dynamic>();

            foreach (var order in orders)
            {
                dynamic orderDetailObject = new ExpandoObject();
                orderDetailObject.orderId = order.orderId;
                orderDetailObject.price = order.price;
                orderDetailObject.orderPlacedOn = order.orderPlacedOn;
                orderDetailObject.isOrderAccepted = order.isOrderAccepted;
                orderDetailObject.isOrderCancelled = order.isOrderCancelled;
                orderDetailObject.productId = order.productId;
                dictionary[order.orderId] = orderDetailObject;
            }

            return dictionary;
        }
    }
}