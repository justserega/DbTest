using System;

namespace DbTest
{
    public interface IModelFixture<T>
    {
        string TableName { get; }
    }

    public interface IModelFixture
    {
        string TableName { get; }
        Type ModelType { get; }
    }
}
