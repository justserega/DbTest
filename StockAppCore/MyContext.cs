using Microsoft.EntityFrameworkCore;

namespace StockAppCore.Models
{
    public class MyContext : DbContext
    {
        public DbSet<Unit> Units { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Good> Goods { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Storage> Storages { get; set; }

        public DbSet<MoveDocument> MoveDocuments { get; set; }
        public DbSet<MoveDocumentItem> MoveDocumentItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(@"User ID=test;Password=test;Host=localhost;Port=5432;Database=test;Pooling=true;");
        }
    }
}
