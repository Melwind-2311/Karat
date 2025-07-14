using System;
using System.Collections.Generic;
using System.Linq;
class SalesTransaction
{
   public int ProductId { get; set; }
   public string Category { get; set; }
   public decimal Amount { get; set; }
   public DateTime TransactionDate { get; set; }
}
class Program
{
   static void Main()
   {
       
       var transactions = new List<SalesTransaction>
       {
           new SalesTransaction { ProductId = 1, Category = "Electronics", Amount = 5000, TransactionDate = new DateTime(DateTime.Now.Year, 1, 10) },
           new SalesTransaction { ProductId = 2, Category = "Electronics", Amount = 10000, TransactionDate = new DateTime(DateTime.Now.Year, 2, 15) },
           new SalesTransaction { ProductId = 3, Category = "Clothing", Amount = 7500, TransactionDate = new DateTime(DateTime.Now.Year, 3, 20) },
           new SalesTransaction { ProductId = 4, Category = "Electronics", Amount = 20000, TransactionDate = new DateTime(DateTime.Now.Year, 5, 5) },
           new SalesTransaction { ProductId = 5, Category = "Books", Amount = 3000, TransactionDate = new DateTime(DateTime.Now.Year - 1, 6, 12) },
       };
       int currentYear = DateTime.Now.Year;
       var report = transactions
           .Where(t => t.TransactionDate.Year == currentYear)
           .GroupBy(t => new
           {
               Quarter = GetQuarter(t.TransactionDate),
               t.Category
           })
           .Select(g => new
           {
               Quarter = g.Key.Quarter,
               Category = g.Key.Category,
               TotalRevenue = g.Sum(t => t.Amount)
           })
           .OrderBy(r => r.Quarter) 
           .ThenBy(r => r.Category)
           .ToList();
       
       foreach (var item in report)
       {
           Console.WriteLine($"{{ Quarter = \"{item.Quarter}\", Category = \"{item.Category}\", TotalRevenue = {item.TotalRevenue} }}");
       }
   }
   
   static string GetQuarter(DateTime date)
   {
       if (date.Month <= 3) return "Q1";
       else if (date.Month <= 6) return "Q2";
       else if (date.Month <= 9) return "Q3";
       else return "Q4";
   }
}
