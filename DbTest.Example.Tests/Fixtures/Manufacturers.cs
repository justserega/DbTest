using DbTest.Example.Models;

namespace DbTest.Example.Tests.Fixtures
{
    class Manufacturers : IModelFixture<Manufacturer>
    {
        public string TableName => "Manufacturers";

        public static Manufacturer BrownForman => new Manufacturer
        {
            Id = 1,
            Name = "Brown-Forman",
            CountryId = Countries.USA.Id,
            IsDeleted = false
        };

        public static Manufacturer TheEdringtonGroup => new Manufacturer
        {
            Id = 2,
            Name = "The Edrington Group",
            CountryId = Countries.Scotland.Id,
            IsDeleted = false
        };

        public static Manufacturer Diageo => new Manufacturer
        {
            Id = 3,
            Name = "Diageo",
            CountryId = Countries.Scotland.Id,
            IsDeleted = false
        };

    }
}
