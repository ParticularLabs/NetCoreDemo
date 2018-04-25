namespace Sales.Api.Models
{
    using System;

    public class OrderDetail
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string OrderId { get; set; }
        public DateTime OrderPlacedOn { get; set; }
        public decimal Price { get; set; }
        public bool IsOrderAccepted { get; set; }
        public bool IsOrderCancelled { get; set; }
    }
}