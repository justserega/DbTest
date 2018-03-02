using NUnit.Framework;
using StockAppCore.Services;
using StockAppCore.Tests.Base;
using StockAppCore.Tests.Fixtures;
using System;
using System.Linq;

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
            var doc1 = builder.CreateDocument("15.01.2016 10:00:00", Storages.MainStorage, Storages.RemoteStorage);
            builder.AddGood(doc1, Goods.JackDaniels, 10);
            builder.AddGood(doc1, Goods.JohnnieWalker, 15);

            // Outcome from remote storage
            var doc2 = builder.CreateDocument("16.01.2016 20:00:00", Storages.RemoteStorage, Storages.MainStorage);
            builder.AddGood(doc2, Goods.JohnnieWalker, 7);

            /// ACT
            var remains = RemainsService.GetRemainFor(Storages.RemoteStorage, new DateTime(2016, 02, 01));

            /// ASSERT
            Assert.AreEqual(2, remains.Count);
            Assert.AreEqual(10, remains.Single(x => x.GoodId == Goods.JackDaniels.Id).Count);
            Assert.AreEqual(8, remains.Single(x => x.GoodId == Goods.JohnnieWalker.Id).Count);
        }

        [Test]
        public void CalculateRemainsForMoveDocuments2()
        {
            /// ARRANGE 
            var builder = new ModelBuilder();

            // Income to remote storage
            var doc1 = builder.CreateDocument("15.01.2016 10:00:00", Storages.MainStorage, Storages.RemoteStorage);
            builder.AddGood(doc1, Goods.JackDaniels, 10);
            builder.AddGood(doc1, Goods.JohnnieWalker, 15);

            // Outcome from remote storage
            var doc2 = builder.CreateDocument("16.01.2016 20:00:00", Storages.RemoteStorage, Storages.MainStorage);
            builder.AddGood(doc2, Goods.JohnnieWalker, 7);

            /// ACT
            var remains = RemainsService.GetRemainFor(Storages.RemoteStorage, new DateTime(2016, 02, 01));

            /// ASSERT
            Assert.AreEqual(2, remains.Count);
            Assert.AreEqual(10, remains.Single(x => x.GoodId == Goods.JackDaniels.Id).Count);
            Assert.AreEqual(8, remains.Single(x => x.GoodId == Goods.JohnnieWalker.Id).Count);
        }
    }
}
