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

## World

`World` is a conception of test environment. First of all, it is a point to prepare database - clean, load initial fixtures.
Secondly, you can create singletons, set values to static variable and so on. For example, for `asp.net mvc` you can set HttpContext 
with a user:

```
public class World
{
    public static void InitDatabase()
    {
        using (var db = new Db())
        using (var loader = new EFFixtureLoader<Db>(db))
        {
            loader.Load(
                new Countries(),
                new Manufacturers(),
                new Products()
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

```
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

```
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
