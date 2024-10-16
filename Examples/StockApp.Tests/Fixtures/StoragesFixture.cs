﻿
using DbTest;
using StockAppCore.Models;

namespace StockAppCore.Tests.Fixtures
{
    public class StoragesFixture : IModelFixture<Storage>
    {
        public static Storage MainStorage => new Storage
        {
            Id = 1,
            Name = "Main storage",
            IsDeleted = false
        };

        public static Storage RemoteStorage => new Storage
        {
            Id = 2,
            Name = "Remote storage",
            IsDeleted = false
        };

    }
}
