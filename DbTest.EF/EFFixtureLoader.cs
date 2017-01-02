using System.Data.Entity;

namespace DbTest.EF
{
    public class EFFixtureLoader : BaseFixtureLoader
    {        
        public EFFixtureLoader(DbContext db) 
            : base(new EFConnection(db), 
                   new SqlServerProvider(),
                   new EFColumnMapper())
        {
        }
    }
}
