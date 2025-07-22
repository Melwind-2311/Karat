using System;
using System.IO;
using System.Diagnostics;
using System.Threading.Tasks;
public class ProblematicSyncIoInAsync 

{ 

    private static readonly string filePath = "sync_io_problem.txt"; 

 

    public async Task RunProblemScenario() 

    { 

        Console.WriteLine("\n ASynchronous I/O in an async Method ---"); 

        Stopwatch sw = Stopwatch.StartNew(); 

 

        try 

        { 

            Console.WriteLine("Starting asynchronous file write..."); 

            await File.WriteAllText(filePath, "This data was written asynchronously.\n"); 

            Console.WriteLine($"Problem: Wrote data asynchronously. Elapsed: {sw.ElapsedMilliseconds}ms"); 

 

            Console.WriteLine("Starting asynchronous file read..."); 

            string content = await File.ReadAllText(filePath); 

            Console.WriteLine($"Read data asynchronously. Content: '{content.Trim()}'. Elapsed: {sw.ElapsedMilliseconds}ms"); 

        } 

        catch (Exception ex) 

        { 

            Console.WriteLine($"Problematic I/O error: {ex.Message}"); 

        } 

        finally 

        { 

            sw.Stop(); 

            if (File.Exists(filePath)) File.Delete(filePath);  

        } 

    } 

 

    public static async Task Main(string[] args) 

    { 

        await new ProblematicSyncIoInAsync().RunProblemScenario(); 

        Console.WriteLine("\nProblem scenario complete. Notice potential blocking."); 

    } 

} 

 
