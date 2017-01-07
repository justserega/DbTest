using System.Data.Entity;

namespace DbTest.EF
{
    public class EFTestDatabase<TContext> : TestDatabase where TContext: DbContext
    {        
        public EFTestDatabase(TContext db)
            : base(new EFCodeFirstLayer<TContext>(db))
        {
        }
    }
}
