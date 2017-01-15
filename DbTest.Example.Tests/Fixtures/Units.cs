using DbTest.Example.Models;

namespace DbTest.Example.Tests.Fixtures
{
    public class Units : IModelFixture<Unit>
    {
        public string TableName => "Units";

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
