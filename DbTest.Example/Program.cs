using DbTest.Example.Models;
using System;
using System.Linq;

namespace DbTest.Example
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new MyContext())
            {
                var products = context.Products.Where(x => x.IsDeleted == false);

                foreach (var p in products) Console.WriteLine(p.Name);
                Console.ReadKey();
            }
        }
    }
}
