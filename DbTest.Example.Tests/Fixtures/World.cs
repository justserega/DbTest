using DbTest.EF;
using DbTest.Example.Models;

namespace DbTest.Example.Tests.Fixtures
{
    static class World
    {
        public static void Init()
        {
            using (var loader = new EFFixtureLoader(new MyContext()))
            {
                loader.Load(
                    new Products(),
                    new Customers()
                );
            }
        }
    }
}
