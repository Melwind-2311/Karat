using System; 
using System.Threading.Tasks; 
public sealed class ProblematicSingleton 
{ 
    private static ProblematicSingleton instance = null; 
    private static readonly object padlock = new object();
    private ProblematicSingleton() 
    { 
        Console.WriteLine("ProblematicSingleton instance created."); 
    } 
    public static ProblematicSingleton Instance 
    { 
        get 
        { 
            lock (padlock)
            {
                if (instance == null) 
            { 
                instance = new ProblematicSingleton(); 
            } 
            return instance;
            }
             
        } 
    } 
    public void ShowMessage(string message) 
    { 
        Console.WriteLine($"Singleton message: {message}"); 
    } 
} 
public class SingletonDemonstration 
{ 
    public static void Main(string[] args) 
    { 
        Console.WriteLine("--- Problematic Singleton Demonstration-1 ---"); 
        Parallel.Invoke( 
            () => ProblematicSingleton.Instance.ShowMessage("Hello from Thread 1"), 
            () => ProblematicSingleton.Instance.ShowMessage("Hello from Thread 2"), 
            () => ProblematicSingleton.Instance.ShowMessage("Hello from Thread 3") 
        ); 
        Console.WriteLine("\n--- end Singleton Demonstration ---\n");
        Console.ReadKey();  
    } 
} 
