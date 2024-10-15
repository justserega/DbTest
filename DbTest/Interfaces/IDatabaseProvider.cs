using System.Collections.Generic;
using System.Reflection;

namespace DbTest
{
    public interface IDatabasePreparer
    {
        void BeforeLoad(IDataAccessLayer dataAccessLayer);
        void AfterLoad(IDataAccessLayer dataAccessLayer);
        void InsertObjects(IDataAccessLayer dataAccessLayer, string tableName, List<ColumnInfo> columns, List<object[]> values);
    }
}
