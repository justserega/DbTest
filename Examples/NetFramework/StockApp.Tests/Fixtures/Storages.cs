using DbTest.Example.Models;

namespace DbTest.Example.Tests.Fixtures
{
    public class Storages : IModelFixture<Storage>
    {
        public string TableName => "Storages";

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
