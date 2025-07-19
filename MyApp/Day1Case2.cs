using System;

class TicketBooking
{
    public double totalFare = 0;

    public bool Validation(int age, int quantity, double pricePerTicket)
    {
        if (quantity <= 0 || pricePerTicket < 0)
        {
            Console.WriteLine("Invalid quantity or price.");
            return false;
        }

        if (age < 0)
        {
            Console.WriteLine("Invalid age.");
            return false;
        }
        return true;
    }
    public double Discount(int age)
    {
        double discount = 0;
        if (age < 5)
            discount = 0.5;
        else if (age >= 60)
            discount = 0.3;
        return discount;
    }
    public double TotalFare(int quantity, double pricePerTicket, double dis)
    {
        double finalPrice = quantity * pricePerTicket * (1 - dis);
        return finalPrice;
    }
    public void BookTicket(string passengerName, int age,  int quantity, double pricePerTicket)
    {
        if(!Validation(age, quantity, pricePerTicket))
        return;
        double dis = Discount(age);
        double finalPrice = TotalFare(quantity, pricePerTicket, dis);
        totalFare += finalPrice;

        Console.WriteLine(passengerName + " booked " + quantity + " tickets. Total fare: " + totalFare);
    }
}

class Program
{
    static void Main()
    {
        TicketBooking booking = new TicketBooking();
        booking.BookTicket("Alice", 3, 2, 100);
        booking.BookTicket("Bob", -1, 1, 150); // Invalid age
        booking.BookTicket("Charlie", 30, 0, 200); // Invalid quantity
    }
}
