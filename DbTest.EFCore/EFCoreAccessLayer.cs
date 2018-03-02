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
            _db.Database.ExecuteSqlCommand(query);
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

        public List<PropertyInfo> GetColumns(Type type)
        {
            var columns = type.GetProperties().Where(x => x.PropertyType.IsValueType || x.PropertyType == typeof(string));

            return columns.Where(x => !x.GetCustomAttributes(true).OfType<NotMappedAttribute>().Any()).ToList();
        }
    }
}
