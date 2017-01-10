using DbTest.Example.Tests.Base;
using DbTest.Example.Tests.Fixtures;
using NUnit.Framework;

namespace DbTest.Example.Tests
{
    public class CreateSale
    {
        [SetUp]
        public void SetUp()
        {
            World.InitDatabase();
        }

        [Test]
        public void SaleProductToCustomer()
        {
            var builder = new ModelBuilder();
            var sale = builder.CreateSale(Products.Pen, Customers.Apple);

            

            Assert.AreEqual(1, 1);
        }
    }
}
