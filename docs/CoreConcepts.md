# Core concepts

* Prepare database
* Fixtures
* World
* Model builder
* Use in test

## Prepare database

You need some database to tests. For little project `sqlite` could be a good choice (as a compromise between performance and reliability of tests). For complex scenarious you need same database which you use in production. As usual it is not problem:
Mysql, Postgresql - lite enough, MSSQL - has LocalDb mode.

In now days DbTest work only with MSSQL out-of-box, but it can configurate to work with any database throw IDatabasePrepare interface.
DbTest has methods to reset database and load initial fixtures:

```cs
var dbTest = new TestDatabase(dataLayer);

dbTest.ResetWithFixtures(
    new Products(),
    new Customers()
);
```

## Fixtures

A real system has many relations between models, we need to fill many tables before create one row in a target table. 
For example, Products can be related to Manufacturers which related to Country.

Let's start from model which has no relations: 

```cs
public class Countries : IModelFixture<Country>
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
class Manufacturers : IModelFixture<Manufacturer>
{
    public string TableName => "Manufacturers";

    public static Manufacturer BrownForman => new Manufacturer
    {
        Id = 1,
        Name = "Brown-Forman",
        CountryId = Countries.USA.Id,
        IsDeleted = false
    };

    public static Manufacturer TheEdringtonGroup => new Manufacturer
    {
        Id = 2,
        Name = "The Edrington Group",
        CountryId = Countries.Scotland.Id,
        IsDeleted = false
    };
}

public class Goods : IModelFixture<Good>
{
    public string TableName => "Goods";

    public static Good JackDaniels => new Good
    {
        Id = 1,
        Name = "Jack Daniels, 0.5l",
        ManufacturerId = Manufacturers.BrownForman.Id,
        IsDeleted = false
    };

    public static Good FamousGrouseFinest => new Good
    {
        Id = 2,
        Name = "The Famous Grouse Finest, 0.5l",
        ManufacturerId = Manufacturers.TheEdringtonGroup.Id,
        IsDeleted = false
    };
}
```
Pay attention to external keys in models we do not set it explicitly instead take value from another fixture. You give advise from  intellisence when create your fixtures and check from compiler when your system change and fixtures must changed too.

## World

`World` is a conception of test environment. First of all, it is a point to prepare database - clean, load initial fixtures.
Secondly, you can create singletons, set values to static variable and so on. For example, for `asp.net mvc` you can set HttpContext 
with a user:

```cs
static class World
{
    public static void InitDatabase()
    {
        using (var context = new MyContext())
        {
            var dbTest = new EFTestDatabase<MyContext>(context);

            dbTest.ResetWithFixtures(
                new Countries(),
                new Manufacturers(),
                new Goods()
            );
        }
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

## Use in test [In progress]

```cs
[SetUp]
public void SetUp()
{
    World.InitDatabase(); // подготавливаем базу к каждому тесту
}

[Test]
public void CalculateRemainsForMoveDocuments()
{
    /// ARRANGE - создаем тестовую ситуацию
    var builder = new ModelBuilder();           

    // Приход товаров на удаленный склад
    var doc1 = builder.CreateDocument("15.01.2016 10:00:00", Storages.MainStorage, Storages.RemoteStorage);
    builder.AddGood(doc1, Goods.JackDaniels, 10);
    builder.AddGood(doc1, Goods.FamousGrouseFinest, 15);
           
    // Расход товаров с удаленного склада
    var doc2 = builder.CreateDocument("16.01.2016 20:00:00", Storages.RemoteStorage, Storages.MainStorage);
    builder.AddGood(doc2, Goods.FamousGrouseFinest, 7);

    /// ACT - вызываем тестируемую функцию
    var remains = RemainsService.GetRemainFor(Storages.RemoteStorage, new DateTime(2016, 02, 01));

    /// ASSERT - проверяем результат
    Assert.AreEqual(2,  remains.Count);
    Assert.AreEqual(10, remains.Single(x => x.GoodId == Goods.JackDaniels.Id).Count);
    Assert.AreEqual(8,  remains.Single(x => x.GoodId == Goods.FamousGrouseFinest.Id).Count);
}
```
