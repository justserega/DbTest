using System;
using System.Collections.Generic;
using System.Reflection;

namespace DbTest
{
    public interface IDataAccessLayer
    {
        void Execute(string query);
        void CreateDatabase();
        IDatabasePreparer GetDatabasePreparer();
        List<ColumnInfo> GetColumns(Type type);
    }

    public class ColumnInfo
    {
        public PropertyInfo Property { get; set; }
        public string ColumnName { get; set; }
    }
}
