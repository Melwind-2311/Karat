using System;

class Calculator
{
    public int total = 0;

    public void AddInput(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            Console.WriteLine("Input is empty or whitespace");
            return;
        }
        if (!int.TryParse(input, out int number))
        {
            Console.WriteLine("Invalid input");
            return;
        }
        total += number;
        Console.WriteLine("Added " + number + ". Total is now " + total);
    }
}

class Program
{
    static void Main(string[] args)
    {
        Calculator calc = new Calculator();
        calc.AddInput("20");
        calc.AddInput("abc");  // This crashes the program
        calc.AddInput("");     // This also crashes the program
    }
}
