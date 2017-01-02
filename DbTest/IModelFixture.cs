using System;

namespace DbTest
{

    public interface IModelFixture
    {
        string TableName { get; }
        Type ModelType { get; }
    }
}
