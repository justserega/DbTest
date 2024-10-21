using StockAppCore.Services;
using StockAppCore.Tests.Fixtures;

namespace StockAppCore.Tests
{
    public class RemainsTest
    {
        [SetUp]
        public void SetUp()
        {
            SandBox.InitDatabase();
        }

        [Test]
        public void CalculateRemainsForMoveDocuments()
        {            
            // Income to remote storage
            DocumentBuilder
                .CreateMoveDocument("2016-01-15 10:00:00", StoragesFixture.StorageA, StoragesFixture.StorageB)
                .AddGood(GoodsFixture.JackDaniels, 10)
                .AddGood(GoodsFixture.JohnnieWalker, 15);

            // Outcome from remote storage
            DocumentBuilder
                .CreateMoveDocument("2016-01-16 20:00:00", StoragesFixture.StorageB, StoragesFixture.StorageA)
                .AddGood(GoodsFixture.JohnnieWalker, 7);

            // ACT
            var date = DateTime.SpecifyKind(new DateTime(2016, 02, 01), DateTimeKind.Utc);
            var remains = new RemainsService(SandBox.GetStockDbContext()).GetRemainFor(StoragesFixture.StorageB, date);

            // ASSERT
            Assert.AreEqual(2, remains.Count);
            Assert.AreEqual(10, remains.Single(x => x.GoodId == GoodsFixture.JackDaniels.Id).Count);
            Assert.AreEqual(8, remains.Single(x => x.GoodId == GoodsFixture.JohnnieWalker.Id).Count);
        }

        [Test]
        public void CalculateRemainsForMoveDocuments2()
        {
            // Income to remote storage
            DocumentBuilder
                .CreateMoveDocument("2016-01-15 10:00:00", StoragesFixture.StorageA, StoragesFixture.StorageB)
                .AddGood(GoodsFixture.JackDaniels, 10)
                .AddGood(GoodsFixture.JohnnieWalker, 15);

            // Outcome from remote storage
            DocumentBuilder
                .CreateMoveDocument("2016-01-16 20:00:00", StoragesFixture.StorageB, StoragesFixture.StorageA)
                .AddGood(GoodsFixture.JohnnieWalker, 7);

            // ACT
            var date = DateTime.SpecifyKind(new DateTime(2016, 02, 01), DateTimeKind.Utc);
            var remains = new RemainsService(SandBox.GetStockDbContext()).GetRemainFor(StoragesFixture.StorageB, date);

            // ASSERT
            Assert.AreEqual(2, remains.Count);
            Assert.AreEqual(10, remains.Single(x => x.GoodId == GoodsFixture.JackDaniels.Id).Count);
            Assert.AreEqual(8, remains.Single(x => x.GoodId == GoodsFixture.JohnnieWalker.Id).Count);
        }
    }
}
