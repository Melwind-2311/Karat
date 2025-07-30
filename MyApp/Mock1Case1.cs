using System; 
using System.Threading.Tasks; 
public class Logger 
{ 
    private static Logger _instance;
    private static readonly object obj = new object();
 
    private Logger() 
    { 
        // Private constructor 
    } 
 
    public static Logger Instance 
    {
        get 
        { 
            if (_instance == null) 
            {
                lock (obj)
                {
                    if(_instance==null)
                    {
                        _instance = new Logger();    
                    }
                } 
            }
            return _instance; 
        }
    }
 
    public void Log(string message) 
    {
       Console.WriteLine($"[{DateTime.Now}] {message}");
    }   
} 
 
// Usage example: 
public class Program 
{ 
    public static void Main() 
    { 
        Parallel.For(0, 10, i => 
        { 
            Logger.Instance.Log($"Log message {i}"); 
        }); 
    } 
} 
