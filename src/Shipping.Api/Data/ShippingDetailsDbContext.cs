namespace Shipping.Api.Data
{
    using Microsoft.EntityFrameworkCore;
    using Shipping.Api.Models;
    public class ShippingDetailsDbContext : DbContext
    {
        public ShippingDetailsDbContext(DbContextOptions<ShippingDetailsDbContext> productDetails)
            : base(productDetails)
        {
        }

        public DbSet<ShippingDetails> ShippingDetails { get; set; }

    }
}
