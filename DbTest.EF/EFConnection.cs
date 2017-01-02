using System;
using System.Data.Entity;

namespace DbTest.EF
{
    public class EFConnection : IConnection
    {
        DbContext _db;
        public EFConnection(DbContext db)
        {
            _db = db;
        }

        public void CreateDatabase()
        {
            _db.Database.CreateIfNotExists();
        }

        public void Execute(string query)
        {
            _db.Database.ExecuteSqlCommand(query);
        }
    }
}
