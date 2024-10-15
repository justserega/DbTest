using Microsoft.EntityFrameworkCore;
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

        public void ResetWithFixtures(params IModelFixture<object>[] fixtures)
        {
            _databasePreparer.BeforeLoad(_dataAccessLayer);
            foreach (var fixture in fixtures) LoadFixture(fixture);
            _databasePreparer.AfterLoad(_dataAccessLayer);
        }

        private void LoadFixture<T>(IModelFixture<T> fixture) where T : class 
        {
            var modelType = fixture.GetType().GetTypeInfo().GetInterfaces()
                .First(x => x.IsConstructedGenericType && x.GetGenericTypeDefinition() == typeof(IModelFixture<>))
                .GenericTypeArguments[0];

            var tableName = GetTableName(_dataAccessLayer.Db, modelType);
            var columns = _dataAccessLayer.GetColumns(modelType);

            List<object> objects = new List<object>();
            var propertyInfoes = fixture.GetType().GetTypeInfo().GetProperties(BindingFlags.Public | BindingFlags.Static);
            foreach (var pInfo in propertyInfoes)
            {
                if (pInfo.PropertyType == modelType)
                {
                    objects.Add(pInfo.GetValue(null, null));
                }
            }

            var rows = new List<object[]>(objects.Count);
            foreach (var obj in objects)
            {
                var row = new object[columns.Count];
                for (var i = 0; i < columns.Count; i++)
                {
                    row[i] = columns[i].Property.GetValue(obj);
                }
                rows.Add(row);
            }

            _databasePreparer.InsertObjects(_dataAccessLayer, tableName, columns, rows);           
        }

        public string GetTableName(DbContext context, Type modelType)
        {
            // Получаем метаданные о типе сущности
            var entityType = context.Model.FindEntityType(modelType);

            if (entityType == null)
                throw new InvalidOperationException($"Entity type '{modelType.Name}' not found in the current DbContext.");

            // Получаем имя таблицы и схему
            var tableName = entityType.GetTableName();
            var schema = entityType.GetSchema();

            return schema != null ? $"\"{schema}\".\"{tableName}\"" : $"\"{tableName}\"";
        }
    }
}
