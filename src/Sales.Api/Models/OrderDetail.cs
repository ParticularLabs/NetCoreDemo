using System;

namespace Sales.Api.Models
{
    public class OrderDetail
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string OrderId { get; set; }
        public DateTime OrderPlacedOn { get; set; }

        public bool IsOrderAccepted { get; set; }
    }
}
