using DbTest;
using StockAppCore.Models;

namespace StockAppCore.Tests.Fixtures
{
    class ManufacturersFixture : IModelFixture<Manufacturer>
    {
        public static Manufacturer BrownForman => new Manufacturer
        {
            Id = 1,
            Name = "Brown-Forman",
            CountryId = CountriesFixture.USA.Id,
            IsDeleted = false
        };

        public static Manufacturer TheEdringtonGroup => new Manufacturer
        {
            Id = 2,
            Name = "The Edrington Group",
            CountryId = CountriesFixture.Scotland.Id,
            IsDeleted = false
        };

        public static Manufacturer Diageo => new Manufacturer
        {
            Id = 3,
            Name = "Diageo",
            CountryId = CountriesFixture.Scotland.Id,
            IsDeleted = false
        };

    }
}
