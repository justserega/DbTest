using System.Collections.Generic;
using System.Reflection;

namespace DbTest
{
    public interface IDatabaseProvider
    {
        void BeforeLoad(IConnection connection);
        void AfterLoad(IConnection connection);
        void InsertObjects(IConnection connection, string tableName, List<object> objects, List<PropertyInfo> columns);
    }
}
