﻿using System;
using EShop.Messages.Events;
using NServiceBus.Logging;

namespace Sales.Api.MessageHandlers
{
    using System.Threading.Tasks;
    using EShop.Messages.Commands;
    using NServiceBus;

    public class PlaceOrderHandler : IHandleMessages<PlaceOrder>
    {
        static ILog log = LogManager.GetLogger<PlaceOrderHandler>();
        static Random random = new Random();

        public async Task Handle(PlaceOrder message, IMessageHandlerContext context)
        {
            // Simulate some work
            await Task.Delay(random.Next(25, 50));

            log.Info($"PlaceOrder '{message.OrderId}' has been received. Do something meaningful");
            await context.Publish(new OrderPlaced()
            {
                OrderId = message.OrderId,
                ProductId = message.ProductId
            });
        }
    }
}
