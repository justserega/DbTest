using DbTest.EFCore;
using Microsoft.EntityFrameworkCore;
using StockApp;
using StockAppCore.Tests.Fixtures;

namespace StockAppCore.Tests
{
    static class SandBox
    {
        const string ConnectionString = "User ID=test;Password=test;Host=localhost;Port=5432;Database=test;Pooling=true;";
        public static void InitDatabase()
        {
            new EFTestDatabase<StockDbContext>(GetStockDbContext())
                .ResetWithFixtures(
                    new UnitsFixture(),
                    new CountriesFixture(),
                    new ManufacturersFixture(),
                    new GoodsFixture(),
                    new TestEntityFixtures()
                );

            new EFTestDatabase<StockDbContext>(GetStockDbContext())
                .ResetWithFixtures(
                    new UnitsFixture(),
                    new CountriesFixture(),
                    new ManufacturersFixture(),
                    new GoodsFixture(),
                    new TestEntityFixtures()
                );
        }

        public static AuthDbContext GetAuthDbContext() => GetContext<AuthDbContext>(options => new AuthDbContext(options), "auth");
        public static StockDbContext GetStockDbContext() => GetContext<StockDbContext>(options => new StockDbContext(options), "stock");

        private static T GetContext<T>(Func<DbContextOptions<T>, T> create, string schemaName) where T : DbContext
        {
            var optionsBuilder = new DbContextOptionsBuilder<T>();
            optionsBuilder.UseNpgsql(ConnectionString, x => x.MigrationsHistoryTable("__EFMigrationsHistory", schemaName));

            return create(optionsBuilder.Options);
        }
    }
}
