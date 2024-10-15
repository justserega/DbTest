

using DbTest;
using StockAppCore.Models;

namespace StockAppCore.Tests.Fixtures
{
    public class CountriesFixture : IModelFixture<Country>
    {
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
