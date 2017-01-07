using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DbTest
{
    public class SqlServerPreparer : IDatabasePreparer
    {
        public void BeforeLoad(IDataAccessLayer connection)
        {
            connection.Execute("EXEC sp_msforeachtable 'ALTER TABLE ? NOCHECK CONSTRAINT all'");
            connection.Execute("EXEC sp_msforeachtable @command1='DELETE FROM ?', @whereand='and o.name not like \"[_][_]%\"' ");
        }

        public void AfterLoad(IDataAccessLayer connection)
        {
            connection.Execute("EXEC sp_msforeachtable 'ALTER TABLE ? WITH CHECK CHECK CONSTRAINT all'");
        }

        public void InsertObjects(IDataAccessLayer connection, string tableName, List<object> objects, List<PropertyInfo> columns)
        {
            var columnNames = string.Join(",", columns.Select(x => x.Name));
            var rows = new List<string>();
            foreach (var obj in objects)
            {
                var values = new List<string>();                
                foreach (var column in columns)
                {
                    var val = column.GetValue(obj);
                    
                    if (val == null)
                    {
                        values.Add("null");
                        continue;
                    }

                    var valType = val.GetType();
                    if (valType == typeof(string) || valType == typeof(Guid))
                    {
                        values.Add($"'{val}'");
                    }
                    else if (valType == typeof(bool))
                    {
                        var boolVal = (bool)val;
                        values.Add(boolVal ? "1" : "0");
                    }
                    else if (valType == typeof(DateTime))
                    {
                        var dateVal = (DateTime)val;
                        values.Add($"'{dateVal.ToString("yyyy-MM-dd HH:mm:ss")}'");
                    }
                    else if (valType.IsEnum)
                    {
                        var enumVal = (int)val;
                        values.Add(enumVal.ToString());
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

        public void InsertObjects(IDataAccessLayer connection, string tableName, List<string> columnNames, List<object[]> rows)
        {
            var columns = string.Join(",", columnNames);

            foreach (var row in rows)
            {
                var values = new List<string>();
                foreach (var val in row)
                {
                    if (val == null)
                    {
                        values.Add("null");
                        continue;
                    }

                    var valType = val.GetType();
                    if (valType == typeof(string) || valType == typeof(Guid))
                    {
                        values.Add($"'{val}'");
                    }
                    else if (valType == typeof(bool))
                    {
                        var boolVal = (bool)val;
                        values.Add(boolVal ? "1" : "0");
                    }
                    else if (valType == typeof(DateTime))
                    {
                        var dateVal = (DateTime)val;
                        values.Add($"'{dateVal.ToString("yyyy-MM-dd HH:mm:ss")}'");
                    }
                    else if (valType.IsEnum)
                    {
                        var enumVal = (int)val;
                        values.Add(enumVal.ToString());
                    }
                    else
                    {
                        values.Add(val.ToString());
                    }
                }

                var sql = $@"
                    SET IDENTITY_INSERT {tableName} ON;
                    INSERT INTO {tableName} ({columns}) VALUES ({string.Join(",", values)});
                    SET IDENTITY_INSERT {tableName} OFF;";
                connection.Execute(sql);
            }
        }
    }
}
