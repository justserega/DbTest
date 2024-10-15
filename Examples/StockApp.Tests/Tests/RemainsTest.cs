using StockAppCore.Services;
using StockAppCore.Tests.Fixtures;

namespace StockAppCore.Tests
{
    public class RemainsTest
    {
        [SetUp]
        public void SetUp()
        {
            World.InitDatabase();
        }

        [Test]
        public void CalculateRemainsForMoveDocuments()
        {
            /// ARRANGE 
            var builder = new ModelBuilder();

            // Income to remote storage
            var doc1 = builder.CreateDocument("15.01.2016 10:00:00", StoragesFixture.MainStorage, StoragesFixture.RemoteStorage);
            builder.AddGood(doc1, GoodsFixture.JackDaniels, 10);
            builder.AddGood(doc1, GoodsFixture.JohnnieWalker, 15);

            // Outcome from remote storage
            var doc2 = builder.CreateDocument("16.01.2016 20:00:00", StoragesFixture.RemoteStorage, StoragesFixture.MainStorage);
            builder.AddGood(doc2, GoodsFixture.JohnnieWalker, 7);

            /// ACT
            var date = DateTime.SpecifyKind(new DateTime(2016, 02, 01), DateTimeKind.Utc);
            var remains = new RemainsService(World.GetContext()).GetRemainFor(StoragesFixture.RemoteStorage, date);

            /// ASSERT
            Assert.AreEqual(2, remains.Count);
            Assert.AreEqual(10, remains.Single(x => x.GoodId == GoodsFixture.JackDaniels.Id).Count);
            Assert.AreEqual(8, remains.Single(x => x.GoodId == GoodsFixture.JohnnieWalker.Id).Count);
        }

        [Test]
        public void CalculateRemainsForMoveDocuments2()
        {
            /// ARRANGE 
            var builder = new ModelBuilder();

            // Income to remote storage
            var doc1 = builder.CreateDocument("15.01.2016 10:00:00", StoragesFixture.MainStorage, StoragesFixture.RemoteStorage);
            builder.AddGood(doc1, GoodsFixture.JackDaniels, 10);
            builder.AddGood(doc1, GoodsFixture.JohnnieWalker, 15);

            // Outcome from remote storage
            var doc2 = builder.CreateDocument("16.01.2016 20:00:00", StoragesFixture.RemoteStorage, StoragesFixture.MainStorage);
            builder.AddGood(doc2, GoodsFixture.JohnnieWalker, 7);

            /// ACT
            var date = DateTime.SpecifyKind(new DateTime(2016, 02, 01), DateTimeKind.Utc);
            var remains = new RemainsService(World.GetContext()).GetRemainFor(StoragesFixture.RemoteStorage, date);

            /// ASSERT
            Assert.AreEqual(2, remains.Count);
            Assert.AreEqual(10, remains.Single(x => x.GoodId == GoodsFixture.JackDaniels.Id).Count);
            Assert.AreEqual(8, remains.Single(x => x.GoodId == GoodsFixture.JohnnieWalker.Id).Count);
        }
    }
}
