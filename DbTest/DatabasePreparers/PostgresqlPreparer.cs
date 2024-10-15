using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql;
using Npgsql.EntityFrameworkCore.PostgreSQL.Migrations.Internal;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text.Json;

namespace DbTest
{
    public class PostgresqlPreparer : IDatabasePreparer
    {
        private readonly DbContext _dbContext;

        public PostgresqlPreparer(DbContext dbContext)
        {            
            _dbContext = dbContext;
        }

        public void BeforeLoad(IDataAccessLayer dataAccessLayer)
        {
            var historyRepository = _dbContext.GetService<IHistoryRepository>();
            var type = historyRepository.GetType();

            var tableName = type.GetProperty("TableName", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(historyRepository);
            var tableScheme = type.GetProperty("TableSchema", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(historyRepository) ?? "public";

            dataAccessLayer.Execute(@$"
                    do
                    $$
                    declare
                      l_stmt text;
                    begin
                      select 'truncate ' || string_agg(format('%I.%I', schemaname, tablename), ',')
                        into l_stmt
                      from pg_tables
                      where schemaname in ('{tableScheme}') and tablename != '{tableName}';

                      execute l_stmt;
                    end;
                    $$
                ");
        }

        public void AfterLoad(IDataAccessLayer dataAccessLayer)
        {
        }

        public void InsertObjects(IDataAccessLayer dataAccessLayer, string tableName, List<ColumnInfo> columns, List<object[]> rows)
        {
            var connection = dataAccessLayer.Db.Database.GetDbConnection();
            if (connection.State != System.Data.ConnectionState.Open) connection.Open();

            foreach (var row in rows)
            {
                using var command = connection.CreateCommand() as NpgsqlCommand;

                var values = new List<string>();
                for(var i = 0; i < columns.Count; i++)
                {
                    var valueName = "@v" + i;
                    values.Add(valueName);
                    
                    var column = columns[i];
                    var val = row[i];

                    if (column.TypeName == "jsonb")
                    {
                        var serialized = JsonSerializer.Serialize(val);
                        command.Parameters.AddWithValue(valueName, NpgsqlDbType.Jsonb, serialized);
                    }
                    else if (column.Property.PropertyType.IsEnum)
                    {
                        command.Parameters.AddWithValue(valueName, NpgsqlDbType.Integer, (int)val);
                    }
                    else
                    {
                        var dbType = GetDbType(column.TypeName);
                        command.Parameters.AddWithValue(valueName, dbType, val ?? DBNull.Value);
                    }
                }
                
                var columnNames = string.Join(",", columns.Select(x => $"\"{x.ColumnName}\""));
                command.CommandText = $@"INSERT INTO {tableName} ({columnNames}) VALUES ({string.Join(",", values)});";

                try
                {
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception(command.CommandText, ex);
                }
            }
        }

        private NpgsqlDbType GetDbType(string typeName)
        {
            if (Enum.TryParse(typeof(NpgsqlDbType), typeName, true, out var result))
            {
                return (NpgsqlDbType)result;
            }

            return typeName switch
            {
                "double precision" => NpgsqlDbType.Double,
                "timestamp with time zone" => NpgsqlDbType.TimestampTz,
                _ => throw new ArgumentException($"Invalid NpgsqlDbType value: {typeName}")
            };

            

            //switch (typeName)
            //{
            //    case "integer":
            //        return NpgsqlDbType.Integer;
            //    case "text":
            //        return NpgsqlDbType.Text;                    
            //    case "boolean":
            //        return NpgsqlDbType.Boolean;
            //    case "double":
            //        return NpgsqlDbType.Double;
            //}

            //return 
            //throw new NotImplementedException(typeName);
        }
    }
}
