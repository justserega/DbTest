using Microsoft.EntityFrameworkCore;
using StockAppCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAppCore
{
    class Program
    {
        static void Main(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MyContext>();
            optionsBuilder.UseNpgsql(@"User ID=dev;Password=dev;Host=localhost;Port=5432;Database=dev;Pooling=true;");

            using (var context = new MyContext(optionsBuilder.Options))
            {
                var goods = context.Storages.Where(x => x.IsDeleted == false);

                foreach (var g in goods) Console.WriteLine(g.Name);
                Console.ReadKey();
            }
        }
    }
}
