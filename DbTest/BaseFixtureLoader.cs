using System;
using System.Collections.Generic;
using System.Reflection;

namespace DbTest
{
    public abstract class BaseFixtureLoader : IDisposable
    {
        IConnection _connection;
        IDatabaseProvider _databaseProvider;
        IColumnMapper _mapper;
        public BaseFixtureLoader(IConnection connection, IDatabaseProvider databaseProvider, IColumnMapper mapper)
        {
            _databaseProvider = databaseProvider;
            _mapper = mapper;
            _connection = connection;

            _connection.CreateDatabase();
            _databaseProvider.BeforeLoad(_connection);
        }
        
        public void Dispose()
        {
            _databaseProvider.AfterLoad(_connection);
        }

        public void Load(params IModelFixture[] fixtures)
        {
            foreach (var fixture in fixtures) Load(fixture);
        }

        public void Load(IModelFixture fixture)
        {
            var tableName = fixture.TableName;

            var columns = _mapper.GetColumns(fixture.ModelType);

            List<object> objects = new List<object>();
            var propertyInfoes = fixture.GetType().GetProperties(BindingFlags.Public | BindingFlags.Static);
            foreach (var pInfo in propertyInfoes)
            {
                if (pInfo.PropertyType == fixture.ModelType)
                {
                    objects.Add(pInfo.GetValue(null, null));
                }
            }

            _databaseProvider.InsertObjects(_connection, tableName, objects, columns);           
        }
    }
}
