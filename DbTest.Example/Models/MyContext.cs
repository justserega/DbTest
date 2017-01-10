using System.Data.Entity;

namespace DbTest.Example.Models
{
    public class MyContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Sale> Sales { get; set; }
    }
}
