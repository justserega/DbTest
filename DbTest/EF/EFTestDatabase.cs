using Microsoft.EntityFrameworkCore;

namespace DbTest.EFCore
{
    public class EFTestDatabase<TContext> : TestDatabase where TContext : DbContext
    {
        public EFTestDatabase(TContext db)
            : base(new EFCoreAccessLayer<TContext>(db))
        {
        }
    }
}
