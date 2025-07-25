using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
public class FileProcessor
{
    private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(3); 
    public async Task ProcessFilesAsync(List<string> files)
    {
        List<Task> tasks = new List<Task>();
        foreach (var file in files)
        {
            tasks.Add(ProcessFileWithConcurrencyControlAsync(file));
        }
        await Task.WhenAll(tasks);
    }
    private async Task ProcessFileWithConcurrencyControlAsync(string file)
    {
        await _semaphore.WaitAsync(); 
        try
        {
            Console.WriteLine($"[START] Processing file: {file}");
            await ProcessFileAsync(file);
            Console.WriteLine($"[DONE]  Finished file: {file}");
        }
        finally
        {
            _semaphore.Release(); 
        }
    }
    private async Task ProcessFileAsync(string file)
    {
      
        await Task.Delay(2000);
    }
}
public class Program
{
    public static async Task Main(string[] args)
    {
        var files = new List<string> { "file1.txt", "file2.txt", "file3.txt", "file4.txt", "file5.txt" };
        var processor = new FileProcessor();
        await processor.ProcessFilesAsync(files);
    }
}
