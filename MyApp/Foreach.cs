using System; 
using System.Collections.Generic; 
using System.Linq; 
public class ProblematicCollectionModification 
{ 
    public static void Main(string[] args) 
    { 
        Console.WriteLine("--- Problematic Collection Modification ---"); 
        List<string> names = new List<string> { "Alice", "Bob", "Charlie", "David" }; 
        Console.WriteLine("Original list:"); 
        names.ForEach(n => Console.Write($"{n} ")); 
        Console.WriteLine("\n"); 
        try 
        { 
            for (int i=0;i<names.Count;i++) 
            { 
                if (names[i] == "Bob") 
                { 
                    names.RemoveAt(i);  
                } 
                Console.WriteLine($"Processing: {names[i]}"); 
            } 
        } 
        catch (InvalidOperationException ex) 
        { 
            Console.WriteLine($"Caught Expected Exception: {ex.Message}"); 
        } 
        Console.WriteLine("\n--- End Collection Modification ---\n"); 
        Console.ReadKey(); 
    } 
} 
