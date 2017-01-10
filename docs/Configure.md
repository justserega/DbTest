# Configure

DbTest is a tiny pure library that does not know anything about your data access layer. To use it, you need realize 
`IDataAccessLayer.cs` interface and pass it to constructor of TestDatabase class.

```cs
public interface IDataAccessLayer
{
    void Execute(string query);
    void CreateDatabase();
    IDatabasePreparer GetDatabasePreparer();
    List<PropertyInfo> GetColumns(Type type);
}
```

* `CreateDatabase` - method run once in test session. It is place to create or migrate to latest database structure.
* `Execute` - execute sql query
* `GetDatabasePreparer` - return instance of any `IDatabasePreparer` interface. This instance realize strategy for load fixtures to specific database.
There is ready for use strategy for MSSQL (`SqlServerDatabasePrepare`) or write your own implementation.
* `GetColumns` - determine which field from models are columns in database. It is related to ORM you use.

## EntityFramework
There is nuget package (`Install-Package DbTest.EF6`) which work for Entity Framework (code-fist). Now in work only with MSSQL.
