namespace EShop.Messages.Commands
{
    using System;
    using NServiceBus;
    public class StoreOrder : ICommand
    {
        public string OrderId { get; set; }
        public int ProductId { get; set; }
        public DateTime OrderPlacedOn { get; set; }
    }
}
