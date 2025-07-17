using System; 
using System.Diagnostics; 
using System.Threading; 
using System.Threading.Tasks; 
public class ProblematicCounter 
{
    private int counter = 0; 
    private readonly object lockObj = new object();
    public void Increment() 
    { 
        lock (lockObj)
        {
            counter++;
        }
    } 
    public int GetCounter() 
    {
        return counter; 
    } 
} 
public class RaceConditionDemonstration 
{ 
    public static void Main(string[] args) 
    { 
        Console.WriteLine("--- Race Condition with Shared Variable ---"); 
        ProblematicCounter counter = new ProblematicCounter(); 
        const int iterations = 100000; // A large number to highlight the issue 
        // Create multiple tasks to increment the counter concurrently 
        Task[] tasks = new Task[5]; 
        for (int i = 0; i < tasks.Length; i++) 
        { 
            tasks[i] = Task.Run(() => 
            { 
                for (int j = 0; j < iterations; j++) 
                { 
                    counter.Increment(); 
                } 
            }); 
        } 
        Task.WaitAll(tasks);  
        Console.WriteLine($"Expected counter value: {iterations * tasks.Length}"); 
        Console.WriteLine($"Actual counter value: {counter.GetCounter()}"); 
        Console.WriteLine($"Difference: {(iterations * tasks.Length) - counter.GetCounter()}"); 
        Console.WriteLine("\n--- End Race Condition Demonstration ---\n"); 
        Console.ReadKey(); 
    } 
} 
