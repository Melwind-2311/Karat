using System;
using System.Collections.Generic;
public class Program
{
    public static void Main(string[] args)
    {
        List<int> numbers = new List<int>();
        for (int i = 0; i < 1000000; i++)
        {
            numbers.Add(i % 10000); // Many duplicates
        }

        HashSet<int> uniqueNumbers = new HashSet<int>();

        foreach (int number in numbers)
        {
            uniqueNumbers.Add(number);
        }
        Console.WriteLine("Unique count: " + uniqueNumbers.Count);
        //or
        HashSet<int> uniqueNumbers = new HashSet<int>(numbers);
        Console.WriteLine(string.Join(", ",uniqueNumbers));
    }
}
