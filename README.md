# DbTest

DbTest is a tiny test library for .NET application. It helps you to write tests with a real database very easy and native. It does only dirty work with the database, and you still need [NUnit](https://github.com/nunit/nunit), [XUnit](https://github.com/xunit/xunit) or any of your favorite test framework.

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
To define initial state you can use fixtures in txt files. Any changes in project can broke those fixtures, because compiler don't process them. DbTest offer a way to describe fixtures in code. You will get autocomplete, refactoring and type checking when you write and maintain fixtures.
  
- **Idea to make test case**
The main idea to create high level builder that fill tables in database. Each method in this builder create one business model. See more details in [Concepts](https://github.com/justserega/DbTest/blob/master/docs/CoreConcepts.md#model-builder)


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
    public string TableName => "Countries";

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
    using (var context = new MyContext());
    var dbTest = new EFTestDatabase<MyContext>(context);

    dbTest.ResetWithFixtures(
        new CountriesFixture(),
        new ManufacturersFixture(),
        new GoodsFixture()
    );  
```


## World

`World` is a conception of test environment. First of all, it is a point to prepare database - clean, load initial fixtures.
Secondly, you can create some singletons, set values to static variable and so on. For example, for `asp.net mvc` you can set HttpContext 
with a user. It is not included in `DbTest`, you have to create it.

```cs
static class World
{
    public static void InitDatabase()
    {
        using (var context = new MyContext());
        var dbTest = new EFTestDatabase<MyContext>(context);

        dbTest.ResetWithFixtures(
            new CountriesFixture(),
            new ManufacturersFixture(),
            new GoodsFixture()
        );        
    }

    public static void InitContextWithUser()
    {
        HttpContext.Current = new HttpContext(
            new HttpRequest("", "http://your-domain.com", ""),
            new HttpResponse(new StringWriter())
        );
        HttpContext.Current.User = new GenericPrincipal(
            new GenericIdentity("root"),
            new string[0]
            );
    }
}
```

## Model builder

`Model builder` is a helper to create test case. It is not included in `DbTest`, you have to create it. Each methods of this class must 
create and return one entity:

```cs
public class ModelBuilder
{
    public MoveDocument CreateDocument(string time, Storage source, Storage dest)
    {
        var document = new MoveDocument
        {
            Number = "#",

            SourceStorageId = source.Id,
            DestStorageId = dest.Id,

            Time = ParseTime(time),
            IsDeleted = false
        };

        using (var db = new MyContext())
        {
            db.MoveDocuments.Add(document);
            db.SaveChanges();
        }

        return document;
    }

    public MoveDocumentItem AddGood(MoveDocument document, Good good, decimal count)
    {
        var item = new MoveDocumentItem
        {
            MoveDocumentId = document.Id,
            GoodId = good.Id,
            Count = count
        };

        using (var db = new MyContext())
        {
            db.MoveDocumentItems.Add(item);
            db.SaveChanges();
        }

        return item;
    }
}
```

## Use in test 

```cs
[SetUp]
public void SetUp()
{
    World.InitDatabase(); // Main part, prepare database for test
}

[Test]
public void CalculateRemainsForMoveDocuments()
{
    // prepare test
    var builder = new ModelBuilder();           

    var doc1 = builder.CreateDocument("15.01.2016 10:00:00", Storages.MainStorage, Storages.RemoteStorage);
    builder.AddGood(doc1, Goods.JackDaniels, 10);
    builder.AddGood(doc1, Goods.FamousGrouseFinest, 15);
               
    var doc2 = builder.CreateDocument("16.01.2016 20:00:00", Storages.RemoteStorage, Storages.MainStorage);
    builder.AddGood(doc2, Goods.FamousGrouseFinest, 7);

    // test
    var remains = RemainsService.GetRemainFor(Storages.RemoteStorage, new DateTime(2016, 02, 01));

    // assert
    Assert.AreEqual(2,  remains.Count);
    Assert.AreEqual(10, remains.Single(x => x.GoodId == Goods.JackDaniels.Id).Count);
    Assert.AreEqual(8,  remains.Single(x => x.GoodId == Goods.FamousGrouseFinest.Id).Count);
}
```
