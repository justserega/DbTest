using DbTest.EF;
using DbTest.Example.Models;

namespace DbTest.Example.Tests.Fixtures
{
    static class World
    {
        public static void InitDatabase()
        {
            using (var context = new MyContext())
            {
                var dbTest = new EFTestDatabase<MyContext>(context);

                dbTest.ResetWithFixtures(
                    new Products(),
                    new Customers()
                );
            }
        }
    }
}
