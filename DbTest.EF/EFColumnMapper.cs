using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;

namespace DbTest.EF
{
    public class EFColumnMapper : IColumnMapper
    {
        public List<PropertyInfo> GetColumns(Type type)
        {
            var columns = type.GetProperties().Where(x => x.PropertyType.IsValueType || x.PropertyType == typeof(string));

            return columns.Where(x => !x.GetCustomAttributes(true).OfType<NotMappedAttribute>().Any()).ToList();
        }
    }
}
