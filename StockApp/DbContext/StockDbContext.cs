﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using StockAppCore.Models;

namespace StockApp
{
    public class StockDbContext : DbContext
    {
        public DbSet<Unit> Units { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Good> Goods { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Storage> Storages { get; set; }

        public DbSet<MoveDocument> MoveDocuments { get; set; }
        public DbSet<MoveDocumentItem> MoveDocumentItems { get; set; }

        public StockDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}