using System;

namespace DbTest
{
    public interface IModelFixture<out T>
    {
        string TableName { get; }
    }

    //public interface IModelFixture
    //{
    //    string TableName { get; }
    //    Type ModelType { get; }
    //}
}
