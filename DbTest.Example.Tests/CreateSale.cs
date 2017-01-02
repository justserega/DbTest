using DbTest.Example.Tests.Fixtures;
using NUnit.Framework;

namespace DbTest.Example.Tests
{
    public class CreateSale
    {
        [SetUp]
        public void SetUp()
        {
            World.Init();
        }

        [Test]
        public void SaleProductToCustomer()
        {
            Assert.AreEqual(1, 1);
        }
    }
}
