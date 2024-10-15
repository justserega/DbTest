using System;
using System.Collections.Generic;
using System.Globalization;
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

        public void InsertObjects(IDataAccessLayer connection, string tableName, List<ColumnInfo> columns, List<object[]> rows)
        {
            var columnNames = string.Join(",", columns.Select(x => x.ColumnName));

            foreach (var row in rows)
            {
                var values = new List<string>();
                foreach (var val in row)
                { 
                    switch(val)
                    {
                        case null:
                            values.Add("null");
                            break;
                        case String str:
                            values.Add($"N'{str}'");
                            break;
                        case Guid guid:
                            values.Add($"'{guid}'");
                            break;
                        case double d:
                            values.Add(d.ToString(CultureInfo.InvariantCulture));
                            break;
                        case decimal d:
                            values.Add(d.ToString(CultureInfo.InvariantCulture));
                            break;
                        case float f:
                            values.Add(f.ToString(CultureInfo.InvariantCulture));
                            break;
                        case bool boolVal:
                            values.Add(boolVal ? "1" : "0");
                            break;
                        case DateTime dateVal:
                            values.Add($"'{dateVal.ToString("yyyy-MM-dd HH:mm:ss")}'");
                            break;
                        case int i:
                            values.Add(i.ToString());
                            break;
                        default:
                            var valType = val.GetType();

                            if (valType.GetTypeInfo().IsEnum)
                            {
                                var enumVal = (int)val;
                                values.Add(enumVal.ToString());
                            }
                            else
                            {
                                values.Add(val.ToString());
                            }
                            break;
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
