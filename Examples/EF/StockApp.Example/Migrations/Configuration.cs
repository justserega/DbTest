namespace DbTest.Example.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<DbTest.Example.Models.MyContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }        
    }
}
