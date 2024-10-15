using DbTest;
using StockAppCore.Models;

namespace StockAppCore.Tests.Fixtures
{
    public class UnitsFixture : IModelFixture<Unit>
    {
        public static Unit Pcs => new Unit
        {
            Id = 1,
            Name = "Pieces",
            ShortName = "pcs",
            IsDeleted = false
        };

        public static Unit Kg => new Unit
        {
            Id = 2,
            Name = "Kilogram",
            ShortName = "Kg",
            IsDeleted = false
        };

    }
}
