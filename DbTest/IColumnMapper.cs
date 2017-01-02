using System;
using System.Collections.Generic;
using System.Reflection;

namespace DbTest
{
    public interface IColumnMapper
    {
        List<PropertyInfo> GetColumns(Type type);
    }
}
