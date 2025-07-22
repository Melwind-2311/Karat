using System;
using System.Threading.Tasks;
public class Issue1_Problem 

{ 

    private async Task<string> GetStringAfterDelayAsync() 

    { 

        await Task.Delay(1000);  

        return "Data after delay"; 

    } 

 

    public async Task RunProblemScenario() 

    { 

        Console.WriteLine("--- Issue 1 ---"); 

        Console.WriteLine("Start Fetching Data (Problematic)..."); 

        await Task.Delay(2000);  

        Console.WriteLine("dataA from Task.Delay().Result: '{dataA}'"); 

        Console.WriteLine("Main thread was blocked for 2 seconds here."); 

        string dataTaskB = await GetStringAfterDelayAsync();  

        Console.WriteLine($"Problem B: dataTaskB Status (will be 'WaitingForActivation' or similar): {dataTaskB.Status}"); 

        Console.WriteLine("End Fetching Data "); 

    } 

 

    public static async Task Main(string[] args) 

    { 

        await new Issue1_Problem().RunProblemScenario(); 

    } 

} 
