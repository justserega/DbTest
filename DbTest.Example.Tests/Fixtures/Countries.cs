using DbTest.Example.Models;

namespace DbTest.Example.Tests.Fixtures
{
    public class Countries : IModelFixture<Country>
    {
        public string TableName => "Countries";

        public static Country Scotland => new Country
        {
            Id = 1,
            Name = "Scotland",
            IsDeleted = false
        };

        public static Country Ireland => new Country
        {
            Id = 2,
            Name = "Ireland",
            IsDeleted = false
        };

        public static Country USA => new Country
        {
            Id = 3,
            Name = "USA",
            IsDeleted = false
        };
    }
}
