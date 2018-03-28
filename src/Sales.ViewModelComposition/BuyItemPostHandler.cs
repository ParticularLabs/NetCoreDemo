﻿using System;
using System.Collections.Generic;
using System.Text;
using EShop.Messages.Commands;

namespace Sales.ViewModelComposition
{
    public class BuyItemPostHandler : IHandleRequests
    {
        private IMessageSession session;
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
            await session.Send("Sales.Api", new BuyItem { ProductId = Int32.Parse(productId) });
        }
    }
}
