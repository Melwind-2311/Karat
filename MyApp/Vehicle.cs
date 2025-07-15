public class Vehicle 
{ 
    public virtual void StartEngine() 
    { 
        Console.WriteLine("Vehicle engine started."); 
    }}  
public class Car : Vehicle 
{ 
    public override void StartEngine() 
    {        Console.WriteLine("Car-specific checks..."); 
        base.StartEngine();    }} 
public class Program
{
  public static void Main()
  {
    Car myCar = new Car();
    myCar.StartEngine();
  }
}
