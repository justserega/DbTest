using DbTest.EFCore;
using Microsoft.EntityFrameworkCore;
using StockAppCore.Models;
using StockAppCore.Tests.Fixtures;

namespace StockAppCore.Tests
{
    static class World
    {
        public static void InitDatabase()
        {


            using (var context = GetContext())
            {
                var dbTest = new EFTestDatabase<MyContext>(context);

                dbTest.ResetWithFixtures(
                    new Units(),
                    new Countries(),
                    new Manufacturers(),
                    new Goods()
                );
            }
        }

        public static MyContext GetContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<MyContext>();
            optionsBuilder.UseNpgsql(@"User ID=test;Password=test;Host=localhost;Port=5432;Database=test;Pooling=true;");

            return new MyContext(optionsBuilder.Options);
        }
    }
}
