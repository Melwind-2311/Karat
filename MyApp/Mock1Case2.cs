using System; 
using System.Collections.Generic; 
using System.Linq; 
 
public class Employee 
{ 
    public int Id { get; set; } 
    public string Department { get; set; } 
    public double Salary { get; set; } 
} 
 
public class Repository<T> 
{ 
    private List<T> _items; 
 
    public Repository(List<T> items) 
    { 
        _items = items; 
    }
 
    public List<T> Filter(Func<T, bool> predicate) 
    { 
        return _items.Where(predicate).ToList(); 
    } 
} 
 
public class Program 
{ 
    public static void Main() 
    { 
        var employees = new List<Employee> 
        { 
            new Employee { Id = 1, Department = "HR", Salary = 50000 }, 
            new Employee { Id = 2, Department = "IT", Salary = 80000 }, 
            new Employee { Id = 3, Department = "HR", Salary = 55000 }, 
            new Employee { Id = 4, Department = "Finance", Salary = 60000 }, 
            new Employee { Id = 5, Department = "IT", Salary = 90000 } 
        }; 
 
        var repository = new Repository<Employee>(employees); 
 
        // Find top 2 highest paid employees in IT department 
        var topIT = repository.Filter(e => e.Department == "IT") 
                              .OrderByDescending(e => e.Salary) 
                              .Take(2); 
                 
 
        foreach (var emp in topIT) 
        { 
            Console.WriteLine($"ID: {emp.Id}, Salary: {emp.Salary}"); 
        } 
    } 
} 
