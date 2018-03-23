using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shipping.Api.Models
{
    public class ShippingDetails
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public decimal Cost { get; set; }
        public bool FreeShippingEligible { get; set; }
    }
}
