
namespace DbTest.Example.Models
{
    public class Sale
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int ProductId { get; set; }

        public Product Product { get; set; }
        public Customer Customer { get; set; }
    }
}
