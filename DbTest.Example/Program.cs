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
                var goods = context.Goods.Where(x => x.IsDeleted == false);

                foreach (var g in goods) Console.WriteLine(g.Name);
                Console.ReadKey();
            }
        }
    }
}
