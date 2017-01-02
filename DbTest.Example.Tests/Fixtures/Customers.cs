using System;
using DbTest.Example.Models;

namespace DbTest.Example.Tests.Fixtures
{

    public class Customers : IModelFixture
    {
        public Type ModelType => typeof(Customer);
        public string TableName => "Customers";

        public static Customer Apple => new Customer
        {
            Id = 1,
            Name = "Apple Inc."
        };
    }
}
