using System;
using DbTest.Example.Models;

namespace DbTest.Example.Tests.Fixtures
{
    public class Products : IModelFixture<Product>
    {
        public string TableName => "Products";

        public static Product Pen => new Product
        {
            Id = 1,
            Name = "Pen",
            IsDeleted = false
        };
    }
}
