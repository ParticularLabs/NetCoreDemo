using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ITOps.ViewModelComposition;
using ITOps.ViewModelComposition.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Sales.ViewModelComposition
{
    class OrderListGetHandler : IHandleRequests
    {
        public bool Matches(RouteData routeData, string httpVerb, HttpRequest request)
        {
            var controller = (string)routeData.Values["controller"];
            var action = (string)routeData.Values["action"];

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
            vm.Orders = orders;
        }
    }
}
