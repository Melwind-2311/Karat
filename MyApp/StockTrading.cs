using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
public class Stock
{
public string Symbol { get; set; }
public string Name { get; set; }
public Stock(string symbol, string name)
{
Symbol = symbol;
Name = name;
}
public override string ToString()
{
return Name;
}
}
public class PriceRecord
{
public Stock Stock { get; set; }
public int Price { get; set; }
public string Date { get; set; }
public PriceRecord(Stock stock, int price, string date)
{
Stock = stock;
Price = price;
Date = date;
}
public override string ToString()
{
return $"Stock: {Stock} Price: {Price} date: {Date}";
}
}
public class StockCollection
{
public List<PriceRecord> PriceRecords { get; set; }
public Stock Stock { get; set; }
public StockCollection(Stock stock)
{
PriceRecords = new List<PriceRecord>();
Stock = stock;
}
public int GetNumPriceRecords()
{
return PriceRecords.Count;
}
public void AddPriceRecord(PriceRecord priceRecord)
{
if (!priceRecord.Stock.Equals(Stock))
throw new ArgumentException("PriceRecord's Stock is not the same as the StockCollection's");
PriceRecords.Add(priceRecord);
}
public int? GetMaxPrice()
{
return PriceRecords.Count > 0 ? (int?)PriceRecords.Max(priceRecord => priceRecord.Price) : null;
}
public int? GetMinPrice()
{
return PriceRecords.Count > 0 ? (int?)PriceRecords.Min(priceRecord => priceRecord.Price) : null;
}
public double? GetAvgPrice()
{
return PriceRecords.Count > 0 ? (double?)PriceRecords.Average(priceRecord => priceRecord.Price) : null;
}
public Tuple<int, string, string> GetBiggestChange()
{
    if (PriceRecords.Count < 2)
    return new Tuple<int, string, string>(0, "", "");
    
    var Sorted = PriceRecords.OrderBy(record => DateTime.Parse(record.Date)).ToList();
    int Max = 0;
    string startDate = "";
    string endDate = "";
    for (int i =1; i<Sorted.Count; i++)
    {
        int change = Math.Abs(Sorted[i].Price - Sorted[i-1].Price);
        if(change >= Max)
        {
            Max = change;
            startDate = Sorted[i-1].Date;
            endDate = Sorted[i].Date;
        }
    }
    return new Tuple<int, string, string>(Max, startDate, endDate);
}
}
public class Solution
{
public static void Main(String[] args) {
TestPriceRecord();
TestStockCollection();
TestGetBiggestChange();
}
public static void TestPriceRecord()
{
Console.WriteLine("Running TestPriceRecord");
// Test basic PriceRecord functionality
Stock TestStock = new Stock("AAPL", "Apple Inc.");
PriceRecord TestPriceRecord = new PriceRecord(TestStock, 100, "2023-07-01");
Debug.Assert(TestPriceRecord.Stock == TestStock);
Debug.Assert(TestPriceRecord.Price == 100);
Debug.Assert(TestPriceRecord.Date == "2023-07-01");
}
public static StockCollection MakeStockCollection(Stock Stock, List<Tuple<int, string>> PriceData)
{
// Create a new StockCollection for test purposes.
// Stock: The Stock object this StockCollection is for
// PriceData: a list of tuples. Each tuple represents a price record for a single date.
StockCollection StockCollection = new StockCollection(Stock);
foreach (Tuple<int, string> PriceRecordData in PriceData)
{
PriceRecord PriceRecord = new PriceRecord(Stock, PriceRecordData.Item1, PriceRecordData.Item2);
StockCollection.AddPriceRecord(PriceRecord);
}
return StockCollection;
}
public static void TestStockCollection()
{
Console.WriteLine("Running TestStockCollection");
// Test basic StockCollection functionality
Stock TestStock = new Stock("AAPL", "Apple Inc.");
StockCollection StockCollection = new StockCollection(TestStock);
Debug.Assert(StockCollection.GetNumPriceRecords() == 0);
Debug.Assert(StockCollection.GetMaxPrice() == null);
Debug.Assert(StockCollection.GetMinPrice() == null);
Debug.Assert(StockCollection.GetAvgPrice() == null);
// Price Records:
// Price: 110 112 90 105
// Date: 2023-06-29 2023-07-01 2023-06-28 2023-07-06
List<Tuple<int, string>> PriceData = new List<Tuple<int, string>>
{
new Tuple<int, string>(110, "2023-06-29"),
new Tuple<int, string>(112, "2023-07-01"),
new Tuple<int, string>(90, "2023-06-28"),
new Tuple<int, string>(105, "2023-07-06")
};
TestStock = new Stock("AAPL", "Apple Inc.");
StockCollection = MakeStockCollection(TestStock, PriceData);
Debug.Assert(StockCollection.GetNumPriceRecords() == PriceData.Count);
Debug.Assert(StockCollection.GetMaxPrice() == 112);
Debug.Assert(StockCollection.GetMinPrice() == 90);
Debug.Assert(Math.Abs((decimal)StockCollection.GetAvgPrice().GetValueOrDefault() - 104.25m) < 0.1m);
}
public static void TestGetBiggestChange()
{
Console.WriteLine("Running TestGetBiggestChange");
// Test the get_biggest_change method
Stock TestStock = new Stock("AAPL", "Apple Inc.");
StockCollection StockCollection = new StockCollection(TestStock);
Debug.Assert(StockCollection.GetBiggestChange().Equals(new Tuple<int,string,string>(0, "", "")));
// Price Records:
// Price: 110 112 90 105
// Date: 2023-06-29 2023-07-01 2023-06-25 2023-07-06
List<Tuple<int, string>> PriceData = new List<Tuple<int, string>>
{
new Tuple<int, string>(110, "2023-06-29"),
new Tuple<int, string>(112, "2023-07-01"),
new Tuple<int, string>(90, "2023-06-25"),
new Tuple<int, string>(105, "2023-07-06")
};
StockCollection = MakeStockCollection(TestStock, PriceData);
Debug.Assert(StockCollection.GetBiggestChange().Equals(new Tuple<int,string,string>(20, "2023-06-25", "2023-06-29")));
// Price Records:
// Price: 200 210 190 180
// Date: 2000-01-04 1999-12-30 2000-01-03 2000-01-01
PriceData = new List<Tuple<int, string>>
{
new Tuple<int, string>(200, "2000-01-04"),
new Tuple<int, string>(210, "1999-12-30"),
new Tuple<int, string>(190, "2000-01-03"),
new Tuple<int, string>(180, "2000-01-01")
};
StockCollection = MakeStockCollection(TestStock, PriceData);
Debug.Assert(StockCollection.GetBiggestChange().Equals(new Tuple<int,string,string>(30, "1999-12-30", "2000-01-01")));
}
}


