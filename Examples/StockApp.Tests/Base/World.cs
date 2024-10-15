using DbTest.EFCore;
using Microsoft.EntityFrameworkCore;
using StockApp;
using StockAppCore.Tests.Fixtures;

namespace StockAppCore.Tests
{
    static class World
    {
        public static void InitDatabase()
        {


            using (var context = GetContext())
            {
                var dbTest = new EFTestDatabase<StockDbContext>(context);

                dbTest.ResetWithFixtures(
                    new Units(),
                    new Countries(),
                    new Manufacturers(),
                    new Goods()
                );
            }
        }

        public static StockDbContext GetContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<StockDbContext>();
            optionsBuilder.UseNpgsql(@"User ID=test;Password=test;Host=localhost;Port=5432;Database=test;Pooling=true;");

            return new StockDbContext(optionsBuilder.Options);
        }
    }
}
