using System;
using System.Collections.Generic;
using System.Linq;
public class Order
{
   public List<OrderItem> Items { get; set; } = new List<OrderItem>();
   public double GetTotalPrice()
   {
       double total = Items.Sum(item => item.Price * item.Quantity);
       if (total > 100)
       {
           total -= total*0.1; 
       }
       return total;
   }
}
public class OrderItem
{
   public string Name { get; set; }
   public double Price { get; set; }
   public int Quantity { get; set; }
}
public class EmailService
{
   public void SendConfirmation(string emailAddress)
   {
       Console.WriteLine($"Confirmation email sent to {emailAddress}");
   }
}
public class OrderProcessor
{
   private readonly EmailService emailService;
   public OrderProcessor(EmailService service)
   {
       emailService = service;
   }
   public void ProcessOrder(Order order, string customerEmail)
   {
       double total = order.GetTotalPrice();
       Console.WriteLine($"Order total: ${total:F2}");
       emailService.SendConfirmation(customerEmail);
   }
}
class Program
{
   static void Main(string[] args)
   {
       Order order = new Order();
       order.Items.Add(new OrderItem { Name = "Laptop", Price = 75, Quantity = 1 });
       order.Items.Add(new OrderItem { Name = "Headphones", Price = 50, Quantity = 1 });
       EmailService emailService = new EmailService();
       OrderProcessor processor = new OrderProcessor(emailService);
       string customerEmail = "customer@example.com";
       processor.ProcessOrder(order, customerEmail);
   }
}
