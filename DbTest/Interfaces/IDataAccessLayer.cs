using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace DbTest
{
    public interface IDataAccessLayer
    {
        DbContext Db { get; }
        void Execute(string query);

        void CreateDatabase();
        IDatabasePreparer GetDatabasePreparer();
        List<ColumnInfo> GetColumns(Type type);
    }

    public class ColumnInfo
    {
        public string TypeName { get; set; }
        public PropertyInfo Property { get; set; }
        public string ColumnName { get; set; }
    }
}
