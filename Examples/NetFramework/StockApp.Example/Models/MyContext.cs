using System.Data.Entity;

namespace DbTest.Example.Models
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
    }
}
