using DbTest.EFCore;
using StockAppCore.Models;

namespace StockAppCore.Tests.Fixtures
{
    static class World
    {
        public static void InitDatabase()
        {
            using (var context = new MyContext())
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
    }
}
