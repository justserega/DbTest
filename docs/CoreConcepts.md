# Core concepts

* Prepare database
* Fixtures
* World
* Model builder
* Use in test

## Prepare database

[In progress]

## Fixtures

A real system has many relations between models, we need to fill many tables before create one row in a target table. 
For example, Products can be related to Manufacturers which related to Country.

Let's start from model which has no relations: 

```cs
public class Countries : IModelFixture<Country>
{
    public string TableName => "Countries";

    public static Country USA => new Country {
        Id = 11,
        Name = "United States of America",
        ShortName = "USA"
    }
    
    public static Country SouthKorea => new Country {
        Id = 12,
        Name = "Republic of Korea",
        ShortName = "South Korea"
    }
}
```
First of all we need class that realize `IModelFixture<T>` interface. Each model instances are declared as static to give easy access to it from any part of other fixtures or tests. You need set identifiers explicitly and control uniqueness between one class of model.

Now, we are ready to create Manufacturers and Products.
```cs
public class Manufacturers : IModelFixture<Manufacturer>
{
    public string TableName => "Manufacturers";

    public static Manufacturer AppleInc => new Manufacturer {
        Id = 101,
        Name = "Apple Inc.",
        CountryId = Countries.USA.Id
    }
    
    public static Manufacturer SamsungGroup => new Manufacturer {
        Id = 102,
        Name = "Samsung Group",
        CountryId = Countries.SouthKorea.Id    
}

public class Products : IModelFixture<Product>
{
    public string TableName => "Products";

    public static Product iPhone => new Product {
        Id = 1001,
        Name = "iPhone XXX",
        ManufacturerId = Manufacturers.AppleInc.Id
    }
    
    public static Manufacturer Galaxy => new Manufacturer {
        Id = 1002,
        Name = "Galaxy XXX",
        CountryId = Manufacturers.SamsungGroup.Id    
}
```
Pay attention to external keys in models we do not set it explicitly instead take value from another fixture. You give advise from  intellisence when create your fixtures and check from compiler when your system change and fixtures must changed too.

## World

`World` is a conception of test environment. First of all, it is a point to prepare database - clean, load initial fixtures.
Secondly, you can create singletons, set values to static variable and so on. For example, for `asp.net mvc` you can set HttpContext 
with a user:

```cs
public class World
{
    public static void InitDatabase()
    {
        using (var context = new MyContext())
        {
            var dbTest = new EFTestDatabase<MyContext>(context);

            dbTest.ResetWithFixtures(
                new Products(),
                new Customers()
            );
        }
    }

    public static void InitContextWithUser()
    {
        HttpContext.Current = new HttpContext(
            new HttpRequest("", "http://your-domain.com", ""),
            new HttpResponse(new StringWriter())
        );

        // User is logged in
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
    public Product CreateProduct(Manufacturer manufacturer, string productName, Unit unit)
    {
        var product = new Product
        {
            Name = productName,
            ManufacturerId = manufacturer.Id,
            UnitId = unitId,            
        }
        
        using (var db = new DbContext())
        {
            db.Products.Add(product);
            db.SaveChanges();
        }
        return product;
    }
}
```

## Use in test [In progress]

```cs
public class CreateSale
{
    [SetUp]
    public void SetUp()
    {
        World.InitDatabase();
       
    }

    [Test]
    public void TestMethod()
    {
      var builder = new ModelBuilder();
      var sale = builder.CreateSale(Customers.MrBond)
    }
}
```
