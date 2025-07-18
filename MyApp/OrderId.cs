using System;
using System.Collections.Generic;
public class Program
{
    public static void Main()
    {
        OrderProcessor process = new OrderProcessor();
        process.ProcessOrders();
    }
}

public class OrderProcessor
{
    public void ProcessOrders()
    {
        List<int> orderIds = new List<int>();
        for (int i = 0; i < 100000; i++)
        {
            orderIds.Add(i); 
        }
        foreach (object id in orderIds)
        {
            int orderId = (int)id; 
            // Process order...
        }
    }
}
