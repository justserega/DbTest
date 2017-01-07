using System.Collections.Generic;
using System.Reflection;

namespace DbTest
{
    public interface IDatabasePreparer
    {
        void BeforeLoad(IDataAccessLayer dataAccessLayer);
        void AfterLoad(IDataAccessLayer dataAccessLayer);
        void InsertObjects(IDataAccessLayer connection, string tableName, List<string> columnNames, List<object[]> values);
    }
}
