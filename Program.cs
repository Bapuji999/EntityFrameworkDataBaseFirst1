using System;
using EntityFrameWorkDatabaseFirst.Data;
using EntityFrameWorkDatabaseFirst.Models;

namespace EntityFramWorkDatabaseFirst
{
    internal class Pogram
    {
        static void Main(string[] args)
        {
            using DemoCodeFirstContext demoCodeFirstContext = new DemoCodeFirstContext();
            foreach (Product p in demoCodeFirstContext.Products)
            {
                Console.WriteLine(p.Name);
            }
        }
    }
}
