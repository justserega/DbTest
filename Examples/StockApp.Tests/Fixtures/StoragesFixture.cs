
using DbTest;
using StockAppCore.Models;

namespace StockAppCore.Tests.Fixtures
{
    public class StoragesFixture : IModelFixture<Storage>
    {
        public static Storage StorageA => new Storage
        {
            Id = 1,
            Name = "Storage A",
            IsDeleted = false
        };

        public static Storage StorageB => new Storage
        {
            Id = 2,
            Name = "Storage B",
            IsDeleted = false
        };

    }
}
