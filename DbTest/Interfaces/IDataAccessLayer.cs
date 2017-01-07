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
        List<PropertyInfo> GetColumns(Type type);
    }
}
