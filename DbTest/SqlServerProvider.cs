using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DbTest
{
    public class SqlServerProvider : IDatabaseProvider
    {
        public void BeforeLoad(IConnection connection)
        {
            connection.Execute("EXEC sp_msforeachtable 'ALTER TABLE ? NOCHECK CONSTRAINT all'");
            connection.Execute("EXEC sp_msforeachtable 'DELETE FROM ?'");
        }

        public void AfterLoad(IConnection connection)
        {
            connection.Execute("EXEC sp_msforeachtable 'ALTER TABLE ? WITH CHECK CHECK CONSTRAINT all'");
        }

        public void InsertObjects(IConnection connection, string tableName, List<object> objects, List<PropertyInfo> columns)
        {
            var columnNames = string.Join(",", columns.Select(x => x.Name));
            var rows = new List<string>();
            foreach (var obj in objects)
            {
                var values = new List<string>();                
                foreach (var column in columns)
                {
                    var val = column.GetValue(obj);
                    var valType = val.GetType();
                    if (val == null)
                    {
                        values.Add("null");
                    }
                    else if (valType == typeof(string) || valType == typeof(Guid))
                    {
                        values.Add($"'{val}'");
                    }
                    else if (valType == typeof(bool))
                    {
                        values.Add((bool)val ? "1" : "0");
                    }
                    else
                    {
                        values.Add(val.ToString());
                    }
                }

                var sql = $@"
                    SET IDENTITY_INSERT {tableName} ON;
                    INSERT INTO {tableName} ({columnNames}) VALUES ({string.Join(",", values)});
                    SET IDENTITY_INSERT {tableName} OFF;";
                connection.Execute(sql);
            }
        }
    }
}
