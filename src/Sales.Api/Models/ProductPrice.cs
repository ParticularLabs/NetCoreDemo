namespace Sales.Api.Models
{
    public class ProductPrice
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public decimal Price { get; set; }
    }
}