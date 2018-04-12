using System;
using System.Collections.Generic;
using System.Text;
using NServiceBus;

namespace EShop.Messages.Commands
{
    public class AcceptOrder : ICommand
    {
        public string OrderId { get; set; }
    }
}
