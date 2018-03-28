namespace Sales.Api.MessageHandlers
{
    using System.Threading.Tasks;
    using EShop.Messages.Commands;
    using NServiceBus;

    public class PlaceOrderHandler : IHandleMessages<PlaceOrder>
    {
        public Task Handle(PlaceOrder message, IMessageHandlerContext context)
        {
            // Do something meaningful
            return Task.CompletedTask;
        }
    }
}
