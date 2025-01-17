# DbTest

DbTest is a tiny test library for integration test with EntityFrameworkCore in .NET application. It helps you to write tests with a real database very easy and native. It does only dirty work with the database, and you still need [NUnit](https://github.com/nunit/nunit), [XUnit](https://github.com/xunit/xunit) or any of your favorite test framework.

DbTest gives you clean database for each test. It offers easy and maintainable way to create initial fixtures and test cases.

### Motivation

Common approach to test .NET application that work with database is to separate data access code from business logic code. This Data Access Layer will be substitute by mocks in tests. It is very power and flexible method, but it also has some disadvantages: producing many boilerplate types, complexity growth and so on. You have to separate business logic and data access even you don't want it. And the saddest thing you still need to test Data Access Layer.

There is a simpler approach which widespead in dynamic language world. Instead of create and control database abstraction, 
it offers use real database and control it's state. Test framework gives you clean database before each tests and you can create test case by filling it. This approach is significantly more convenient and understandable. Tests with real database are simpler and give more confidence.

### Influences

DbTest is inspired by the test approach from  [Ruby on Rails](https://github.com/rails/rails), [Django](https://github.com/django/django), [Yii2](https://github.com/yiisoft/yii2) and many other perfect dynamic language frameworks. This approach is a very popular in dynamic languages.

### What's you get

- **Clean real database**
Real database has many aspects that can not be emulated by Data Access Layer: constraints, triggers, complex SQL queries and so on. DbTest reset database to initial state before each test. SqlServer and Postgresql are supported now.

- **Strong type fixtures**
There are some libraries that can create initial state throught txt files. Any changes in project can broke those fixtures, because compiler don't process them. DbTest offer a way to describe fixtures in code. You will get autocomplete, refactoring and type checking when you write and maintain fixtures.
  
- **Clean and understandable tests**
Write your tests as a book, even non technical people can read and participate.


### Installation


Install in your `*.Tests` project:

```
Install-Package DbTest
```

You can see usages in [Examples](https://github.com/justserega/DbTest/tree/master/Examples)

## Fixtures

A real system has many relations between models, we need to fill many tables before create one row in a target table. 
For example, Products can be related to Manufacturers which related to Country.

Let's start from model which has no relations: 

```cs
public class CountriesFixture : IModelFixture<Country>
{
    public static Country Scotland => new Country
    {
        Id = 1,
        Name = "Scotland",
        IsDeleted = false
    };

    public static Country USA => new Country
    {
        Id = 2,
        Name = "USA",
        IsDeleted = false
    };
}
```
First of all we need class that realize `IModelFixture<T>` interface. Each model instances are declared as static to give easy access to it from any part of other fixtures or tests. You need set identifiers explicitly and control uniqueness between one class of model.

Now, we are ready to create Manufacturers and Products.
```cs
class ManufacturersFixture : IModelFixture<Manufacturer>
{

    public static Manufacturer BrownForman => new Manufacturer
    {
        Id = 1,
        Name = "Brown-Forman",
        CountryId = CountriesFixture.USA.Id,
        IsDeleted = false
    };

    public static Manufacturer TheEdringtonGroup => new Manufacturer
    {
        Id = 2,
        Name = "The Edrington Group",
        CountryId = CountriesFixture.Scotland.Id,
        IsDeleted = false
    };
}

public class GoodsFixture : IModelFixture<Good>
{
    public static Good JackDaniels => new Good
    {
        Id = 1,
        Name = "Jack Daniels, 0.5l",
        ManufacturerId = ManufacturersFixture.BrownForman.Id,
        IsDeleted = false
    };

    public static Good FamousGrouseFinest => new Good
    {
        Id = 2,
        Name = "The Famous Grouse Finest, 0.5l",
        ManufacturerId = ManufacturersFixture.TheEdringtonGroup.Id,
        IsDeleted = false
    };
}
```
Pay attention to external keys in models we do not set it explicitly instead take value from another fixture. You give advise from  intellisence when create your fixtures and check from compiler when your system change and fixtures must changed too.

## Prepare database for test

DbSet apply migrations and clean database before every test, to prepare call this

```cs    
    new EFTestDatabase<MyContext>(SandBox.GetContext());
        .ResetWithFixtures(
            new CountriesFixture(),
            new ManufacturersFixture(),
            new GoodsFixture()
        );
```


## SandBox

`SandBox` is a conception of test environment. It has connection to test database and methods to clean and load initial fixtures

```cs
static class SandBox
{
    const string ConnectionString = @"User ID=test;Password=test;Host=localhost;Port=5432;Database=test;Pooling=true;";

    public static void InitDatabase()
    {        
        new EFTestDatabase<MyContext>(GetContext())
            .ResetWithFixtures(
                new CountriesFixture(),
                new ManufacturersFixture(),
                new GoodsFixture()
            );
    }

    public MyContext GetContext()
    {
        var optionsBuilder = new DbContextOptionsBuilder<StockDbContext>();
        optionsBuilder.UseNpgsql(ConnectionString);

        return new StockDbContext(optionsBuilder.Options);
    }
}
```

## Model builder

`Model builder` is a helper to create test case. It is not included in `DbTest`, you have to create it. Each methods of this class must 
create and return one entity:

```cs
public class DocumentBuilder
{
    public int DocumentId { get; private set; }
    public DocumentBuilder(int documentId) 
    {
        DocumentId = documentId;
    }

    public static DocumentBuilder CreateMoveDocument(string time, Storage source, Storage dest)
    {
        var document = new MoveDocument
        {
            Number = "#",

            SourceStorageId = source.Id,
            DestStorageId = dest.Id,

            Time = ParseTime(time),
            IsDeleted = false
        };

        using (var db = SandBox.GetStockDbContext())
        {
            db.MoveDocuments.Add(document);
            db.SaveChanges();
        }

        return new DocumentBuilder(document.Id);
    }

    public DocumentBuilder AddGood(Good good, decimal count)
    {
        var item = new MoveDocumentItem
        {
            MoveDocumentId = DocumentId,
            GoodId = good.Id,
            Count = count
        };

        using (var db = SandBox.GetStockDbContext())
        {
            db.MoveDocumentItems.Add(item);
            db.SaveChanges();
        }

        return this;
    }
}
```

## Use in test 

```cs
[SetUp]
public void SetUp()
{
    SandBox.InitDatabase();
}

[Test]
public void CalculateRemainsForMoveDocuments()
{
    // Income 2 goods to storage B
    DocumentBuilder
        .CreateMoveDocument("2016-01-15 10:00:00", StoragesFixture.StorageA, StoragesFixture.StorageB)
        .AddGood(GoodsFixture.JackDaniels, 10)
        .AddGood(GoodsFixture.JohnnieWalker, 15);

    // Outcome 1 good from storage B
    DocumentBuilder
        .CreateMoveDocument("2016-01-16 20:00:00", StoragesFixture.StorageB, StoragesFixture.StorageC)
        .AddGood(GoodsFixture.JohnnieWalker, 7);

    // test remains on storage B
    var remainsService = new RemainsService(SandBox.GetStockDbContext());
    var remains = remainsService.GetRemainFor(StoragesFixture.StorageB, ParseTime("2016-02-01 00:00:00"));

    // assert
    Assert.AreEqual(2, remains.Count);
    Assert.AreEqual(10, remains.Single(x => x.GoodId == GoodsFixture.JackDaniels.Id).Count);
    Assert.AreEqual(8, remains.Single(x => x.GoodId == GoodsFixture.JohnnieWalker.Id).Count);
}
```
