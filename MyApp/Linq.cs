using System;
using System.Collections.Generic;
using System.Linq;
class Department 
{ 
    public int Id; 
    public string Name; 
}
class Employee 
{
     public int Id; 
     public int DepartmentId; 
     public string Name; 
}
class Program
{
   static void Main()
   {
       var departments = GetDepartments(); 
       var employees = GetEmployees();     
       var employeeLookup = employees
           .ToLookup(e => e.DepartmentId);
       foreach (var dept in departments)
       {
           var emps = employeeLookup[dept.Id];
           Console.WriteLine($"{dept.Name}: {emps.Count()} employees");
       }
   }
   static List<Department> GetDepartments()
   {
       var list = new List<Department>();
       for (int i = 1; i <= 100; i++)
       {
           list.Add(new Department { Id = i, Name = $"Dept-{i}" });
       }
       return list;
   }
   static List<Employee> GetEmployees()
   {
       var list = new List<Employee>();
       var rand = new Random();
       for (int i = 1; i <= 10000; i++)
       {
           list.Add(new Employee
           {
               Id = i,
               Name = $"Emp-{i}",
               DepartmentId = rand.Next(1, 101) 
           });
       }
       return list;
   }
}


