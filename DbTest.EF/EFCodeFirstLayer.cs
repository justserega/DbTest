using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace DbTest.EF
{   public class EFCodeFirstLayer<T> : IDataAccessLayer where T : DbContext
    {
        DbContext _db;
        public EFCodeFirstLayer(T db)
        {
            _db = db;
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<T, MigrationConfig<T>>());
        }

        public void CreateDatabase()
        {
            // nothing to do - EF migrations create schema
        }

        public void Execute(string query)
        {
            _db.Database.ExecuteSqlCommand(query);
        }

        public IDatabasePreparer GetDatabasePreparer()
        {
            // TODO: bad provider detection
            var connectionType = _db.Database.Connection.GetType().FullName;

            switch (connectionType)
            {
                case "System.Data.SqlClient.SqlConnection": return new SqlServerPreparer();
                case "Npgsql.NpgsqlConnection": return new PostgresqlPreparer();
                default: throw new Exception("Don't detect provider");
            }
        }

        public List<ColumnInfo> GetColumns(Type type)
        {            
            // TODO: get info from metadata
            var columns = type.GetProperties().Where(x => x.PropertyType.IsValueType || x.PropertyType == typeof(string));

            return columns
                .Where(x => !x.GetCustomAttributes(true).OfType<NotMappedAttribute>().Any())
                .Select(x => new ColumnInfo { Property = x, ColumnName = x.Name }) 
                .ToList();
        }

        class MigrationConfig<TContext> : DbMigrationsConfiguration<TContext> where TContext : DbContext
        {
            public MigrationConfig()
            {
                AutomaticMigrationsEnabled = true;
                AutomaticMigrationDataLossAllowed = true;
            }
        }
    }
}
