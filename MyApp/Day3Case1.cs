
using System;

public class Program
{
    public static void Main()
    {
        Console.Write("Enter a number: ");
        string input = Console.ReadLine();
        InputProcessor processor = new InputProcessor();
        processor.HandleInput(input);
    }
}
public class InputProcessor
{
    public void HandleInput(string input)
    {
        if (long.TryParse(input, out long number)) 
        {
            Console.WriteLine($"Number: {number}");
        }
        Console.WriteLine("Invalid number");
    }
}
