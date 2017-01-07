using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DbTest
{
    public abstract class TestDatabase
    {
        IDataAccessLayer _dataAccessLayer;
        IDatabasePreparer _databasePreparer;
        public TestDatabase(IDataAccessLayer dataAccessLayer)
        {            
            _dataAccessLayer = dataAccessLayer;
            _databasePreparer = _dataAccessLayer.GetDatabasePreparer();
                        
            _dataAccessLayer.CreateDatabase();
        }

        public void ResetWithFixtures(params IModelFixture[] fixtures)
        {
            _databasePreparer.BeforeLoad(_dataAccessLayer);
            foreach (var fixture in fixtures) LoadFixture(fixture);
            _databasePreparer.AfterLoad(_dataAccessLayer);
        }

        private void LoadFixture(IModelFixture fixture)
        {
            var tableName = fixture.TableName;

            var columns = _dataAccessLayer.GetColumns(fixture.ModelType);

            List<object> objects = new List<object>();
            var propertyInfoes = fixture.GetType().GetProperties(BindingFlags.Public | BindingFlags.Static);
            foreach (var pInfo in propertyInfoes)
            {
                if (pInfo.PropertyType == fixture.ModelType)
                {
                    objects.Add(pInfo.GetValue(null, null));
                }
            }

            var columnNames = columns.Select(x => x.Name).ToList();
            var rows = new List<object[]>(objects.Count);
            foreach (var obj in objects)
            {
                var row = new object[columns.Count];
                for (var i = 0; i < columns.Count; i++)
                {
                    row[i] = columns[i].GetValue(obj);
                }
                rows.Add(row);
            }

            _databasePreparer.InsertObjects(_dataAccessLayer, tableName, columnNames, rows);           
        }
    }
}
