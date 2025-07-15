using System;
using System.Collections.Generic;
using System.Linq;

class Product
{
   public string Name { get; set; }
   public double Price { get; set; }
}

class ProductAnalyzer
{
   public void Analyze(List<Product> products)
   {
       int count = 0;
       double sum = 0;
       foreach (var p in products)
       {
           if (p.Price > 1000)
           {
               count++;
               sum += p.Price;
           }
       }
       Console.WriteLine("Expensive Count: " + count);
       Console.WriteLine("Average Price: " + (count > 0 ? sum / count : 0));
   }
}

class Program
{
   static void Main()
   {
       var products = new List<Product>
       {
           new Product { Name = "Laptop", Price = 1200 },
           new Product { Name = "Tablet", Price = 950 },
           new Product { Name = "Monitor", Price = 1300 },
           new Product { Name = "Mouse", Price = 50 }
       };

       var analyzer = new ProductAnalyzer();
       analyzer.Analyze(products);
   }
}
