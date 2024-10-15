using DbTest;
using StockAppCore.Models;

namespace StockAppCore.Tests.Fixtures
{
    public class GoodsFixture : IModelFixture<Good>
    {
        public static Good JackDaniels => new Good
        {
            Id = 1,
            Name = "Jack Daniels, 0.5l",
            ManufacturerId = ManufacturersFixture.BrownForman.Id,
            UnitId = UnitsFixture.Pcs.Id,
            IsDeleted = false
        };

        public static Good JohnnieWalker => new Good
        {
            Id = 2,
            Name = "Johnnie Walker Red Label, 0.7l",
            ManufacturerId = ManufacturersFixture.Diageo.Id,
            UnitId = UnitsFixture.Pcs.Id,
            IsDeleted = false
        };

        public static Good FamousGrouseFinest => new Good
        {
            Id = 3,
            Name = "The Famous Grouse Finest, 0.5l",
            ManufacturerId = ManufacturersFixture.TheEdringtonGroup.Id,
            UnitId = UnitsFixture.Pcs.Id,
            IsDeleted = false
        };
    }
}
