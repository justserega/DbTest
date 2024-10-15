using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DbTest.EFCore
{
    public class EFCoreAccessLayer<T> : IDataAccessLayer where T : DbContext
    {
        public DbContext Db { get; private set; }
        public EFCoreAccessLayer(T db)
        {
            Db = db;            
        }

        public void CreateDatabase()
        {
            Db.Database.Migrate();
        }

        public void Execute(string query)
        {
            Db.Database.ExecuteSqlRaw(query);
        }

        public IDatabasePreparer GetDatabasePreparer()
        {
            // TODO: bad provider detection
            var connectionType = Db.Database.GetDbConnection().GetType().FullName;

            switch (connectionType)
            {
                case "System.Data.SqlClient.SqlConnection": return new SqlServerPreparer();
                case "Npgsql.NpgsqlConnection": return new PostgresqlPreparer(Db);
                default: throw new Exception("Don't detect provider");
            }
        }

        public List<ColumnInfo> GetColumns(Type type)
        {
            var entityType = Db.Model.FindEntityType(type);

            // Table info 
            var tableName = entityType.GetTableName();
            var tableSchema = entityType.GetSchema();

            // Column info 
            //foreach (var property in entityType.GetProperties())
            //{
            //    var columnName = property.Relational().ColumnName;
            //    var columnType = property.Relational().ColumnType;            
            //}

            return entityType.GetProperties()
                .Select(x => new ColumnInfo { Property = x.PropertyInfo, ColumnName = x.GetColumnName(), TypeName = x.GetColumnType() })
                .ToList();

        }
    }
}
