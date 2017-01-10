using DbTest.Example.Models;

namespace DbTest.Example.Tests.Base
{
    public class ModelBuilder
    {
        public Sale CreateSale(Product product, Customer customer)
        {
            var sale = new Sale
            {
                CustomerId = customer.Id,
                ProductId = product.Id,
            };

            using (var db = new MyContext())
            {
                db.Sales.Add(sale);
                db.SaveChanges();
            }

            return sale;
        }
    }
}
