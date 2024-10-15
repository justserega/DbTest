using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;

namespace DbTest.EFCore
{
    public class EFCoreAccessLayer<T> : IDataAccessLayer where T : DbContext
    {
        DbContext _db;
        public EFCoreAccessLayer(T db)
        {
            _db = db;            
        }

        public void CreateDatabase()
        {
            _db.Database.Migrate();
        }

        public void Execute(string query)
        {
            _db.Database.ExecuteSqlRaw(query);
        }

        public IDatabasePreparer GetDatabasePreparer()
        {
            // TODO: bad provider detection
            var connectionType = _db.Database.GetDbConnection().GetType().FullName;

            switch (connectionType)
            {
                case "System.Data.SqlClient.SqlConnection": return new SqlServerPreparer();
                case "Npgsql.NpgsqlConnection": return new PostgresqlPreparer();
                default: throw new Exception("Don't detect provider");
            }
        }

        public List<ColumnInfo> GetColumns(Type type)
        {
            var entityType = _db.Model.FindEntityType(type);

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
                .Select(x => new ColumnInfo { Property = x.PropertyInfo, ColumnName = x.GetColumnName() })
                .ToList();

        }
    }
}
