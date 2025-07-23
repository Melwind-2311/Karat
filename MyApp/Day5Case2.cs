using System;
public class Program
{
    public static void Main(string[] args)
    {
        Base obj = new Derived();
        obj.Display();
    }
}
public class Base
{
    public virtual void Display()
    {
        Console.WriteLine("Base");
    }
}

public class Derived : Base
{
    public override void Display()
    {
        Console.WriteLine("Derived");
    }
}
