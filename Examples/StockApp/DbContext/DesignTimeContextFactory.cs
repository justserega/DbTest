using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace StockApp
{

    public class AuthContextFactory : IDesignTimeDbContextFactory<AuthDbContext>
    {
        public AuthDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AuthDbContext>();
            optionsBuilder.UseNpgsql(@"User ID=test;Password=test;Host=localhost;Port=5432;Database=test;Pooling=true;", x => x.MigrationsHistoryTable("__EFMigrationsHistory", "stock"));

            return new AuthDbContext(optionsBuilder.Options);
        }
    }
    public class StockContextFactory : IDesignTimeDbContextFactory<StockDbContext>
    {
        public StockDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<StockDbContext>();
            optionsBuilder.UseNpgsql(@"User ID=test;Password=test;Host=localhost;Port=5432;Database=test;Pooling=true;", x => x.MigrationsHistoryTable("__EFMigrationsHistory", "stock"));

            return new StockDbContext(optionsBuilder.Options);
        }
    }
}
