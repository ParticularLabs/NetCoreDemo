namespace Sales.Api.MessageHandlers
{
    using System.Threading.Tasks;
    using EShop.Messages.Commands;
    using NServiceBus;

    public class BuyItemHandler : IHandleMessages<BuyItem>
    {
        public Task Handle(BuyItem message, IMessageHandlerContext context)
        {
            // Do something meaningful
            return Task.CompletedTask;
        }
    }
}
