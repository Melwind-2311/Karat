using System;
public class Logger
{
    private static Logger instance;
    private static readonly object lockObj = new object();
    private Logger() { } 
 
    public static Logger GetInstance() 
    {
        lock (lockObj)
        {
            if (instance == null) 
            { 
                instance = new Logger(); 
            } 
            return instance;    
        }
    }
    public void Log(string message)
    {
        Console.WriteLine($"Log: {message}");
    }
}
public class program
{
    public static void Main(string[] args)
    {
        Logger log1 = Logger.GetInstance();
        log1.Log("This is the first message");
        Logger log2 = Logger.GetInstance();
        log2.Log("This is second message");
        Console.WriteLine($"Same Instance? {object.ReferenceEquals(log1, log2)}");
    }
}

