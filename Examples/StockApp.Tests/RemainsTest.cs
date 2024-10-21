using StockAppCore.Services;
using StockAppCore.Tests;
using StockAppCore.Tests.Fixtures;
using static StockApp.Tests.Utils.ParseUtils;

namespace StockApp.Tests
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
            // Income 2 goods to storage B
            DocumentBuilder
                .CreateMoveDocument("2016-01-15 10:00:00", StoragesFixture.StorageA, StoragesFixture.StorageB)
                .AddGood(GoodsFixture.JackDaniels, 10)
                .AddGood(GoodsFixture.JohnnieWalker, 15);

            // Outcome 1 good from storage B
            DocumentBuilder
                .CreateMoveDocument("2016-01-16 20:00:00", StoragesFixture.StorageB, StoragesFixture.StorageC)
                .AddGood(GoodsFixture.JohnnieWalker, 7);

            // test remains on storage B
            var remainsService = new RemainsService(SandBox.GetStockDbContext());
            var remains = remainsService.GetRemainFor(StoragesFixture.StorageB, ParseTime("2016-02-01 00:00:00"));

            // assert
            Assert.AreEqual(2, remains.Count);
            Assert.AreEqual(10, remains.Single(x => x.GoodId == GoodsFixture.JackDaniels.Id).Count);
            Assert.AreEqual(8, remains.Single(x => x.GoodId == GoodsFixture.JohnnieWalker.Id).Count);
        }
    }
}
