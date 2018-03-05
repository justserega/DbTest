using DbTest.Example.Models;

namespace DbTest.Example.Tests.Fixtures
{
    public class Goods : IModelFixture<Good>
    {
        public string TableName => "Goods";

        public static Good JackDaniels => new Good
        {
            Id = 1,
            Name = "Jack Daniels, 0.5l",
            ManufacturerId = Manufacturers.BrownForman.Id,
            UnitId = Units.Pcs.Id,
            IsDeleted = false
        };

        public static Good JohnnieWalker => new Good
        {
            Id = 2,
            Name = "Johnnie Walker Red Label, 0.7l",
            ManufacturerId = Manufacturers.Diageo.Id,
            UnitId = Units.Pcs.Id,
            IsDeleted = false
        };

        public static Good FamousGrouseFinest => new Good
        {
            Id = 3,
            Name = "The Famous Grouse Finest, 0.5l",
            ManufacturerId = Manufacturers.TheEdringtonGroup.Id,
            UnitId = Units.Pcs.Id,
            IsDeleted = false
        };
    }
}
