﻿using DbTest;
using StockApp.Models;

namespace StockAppCore.Tests.Fixtures
{
    public class TestEntityFixtures : IModelFixture<TestEntity>
    {
        public static TestEntity Pcs => new TestEntity
        {
            Id = Guid.NewGuid(),
            Time = DateTime.UtcNow,
            Date = DateOnly.FromDateTime(DateTime.UtcNow),
            TimeOnly = TimeOnly.FromDateTime(DateTime.UtcNow),
        };

    }
}
