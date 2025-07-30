using System; 
using System.Collections.Generic; 
using System.Linq; 
using Microsoft.VisualStudio.TestTools.UnitTesting; 
 
public enum SeatCategory { GeneralClass, SleeperClass, ThreeTierAC, TwoTierAC, OneTierAC } 
 
public class Booking 
{ 
    public string PassengerName { get; set; } 
    public int Age { get; set; } 
    public string From { get; set; } 
    public string To { get; set; } 
    public SeatCategory Category { get; set; } 
    public double Price { get; set; } 
    public bool IsCompleted { get; set; } = false; 
} 
 
public static class PriceChart 
{ 
    public static Dictionary<(string from, string to, SeatCategory category), double> Prices = new() 
    { 
        { ("CityA", "CityB", SeatCategory.GeneralClass), 100 }, 
        { ("CityA", "CityB", SeatCategory.SleeperClass), 200 }, 
        { ("CityA", "CityB", SeatCategory.ThreeTierAC), 400 }, 
        { ("CityA", "CityB", SeatCategory.TwoTierAC), 600 }, 
        { ("CityA", "CityB", SeatCategory.OneTierAC), 1000 }, 
        { ("CityA", "CityC", SeatCategory.GeneralClass), 200 }, 
        { ("CityA", "CityC", SeatCategory.SleeperClass), 400 }, 
        { ("CityA", "CityC", SeatCategory.ThreeTierAC), 800 }, 
        { ("CityA", "CityC", SeatCategory.TwoTierAC), 1200 }, 
        { ("CityA", "CityC", SeatCategory.OneTierAC), 2000 } 
    }; 
} 
 
public class BookingManager 
{ 
    private List<Booking> _bookings = new(); 
    private Dictionary<SeatCategory, int> _totalSeats = new() 
    { 
        { SeatCategory.GeneralClass, 100 }, 
        { SeatCategory.SleeperClass, 50 }, 
        { SeatCategory.ThreeTierAC, 30 }, 
        { SeatCategory.TwoTierAC, 20 }, 
        { SeatCategory.OneTierAC, 10 } 
    }; 
 
    public double CalculatePrice(string from, string to, SeatCategory category, int age) 
    { 
        var basePrice = PriceChart.Prices[(from, to, category)]; 
        if (age < 5) return basePrice * 0.5; 
        else if (age >= 60) return basePrice * 0.7; 
        else return basePrice; 
    } 
 
    public Booking MakeBooking(string passengerName, int age, string from, string to, SeatCategory category) 
    { 
        double price = CalculatePrice(from, to, category, age); 
        var booking = new Booking { PassengerName = passengerName, Age = age, From = from, To = to, Category = category, Price = price }; 
        _bookings.Add(booking); 
        return booking; 
    } 
 
    public void ModifyBooking(Booking booking, string newFrom, string newTo, SeatCategory newCategory) 
    { 
        if (booking.Category != newCategory)
        {
            booking.IsCompleted = true;
            var newPrice = CalculatePrice(newFrom, newTo, newCategory, booking.Age);
            var updatedbooking = new Booking
            {
                PassengerName = booking.PassengerName,
                Age = booking.Age,
                From = newFrom,
                To = newTo,
                Category = newCategory,
                Price = newPrice,
                IsCompleted = false
            };
            _bookings.Add(updatedbooking);
        }
        else
        {
            booking.From = newFrom;
            booking.To = newTo;
            booking.Price = CalculatePrice(newFrom, newTo, newCategory, booking.Age);
        }
    } 
 
    public void CompleteJourney(Booking booking) { booking.IsCompleted = true; } 
 
    public int GetVacantSeats(SeatCategory category) 
    { 
        int booked = _bookings.Count(b => b.Category == category && !b.IsCompleted); 
        return _totalSeats[category] - booked; 
    } 
} 
 
[TestClass] 
public class BookingManagerTests 
{ 
    [TestMethod] 
    public void CalculatePrice_ShouldApplyChildDiscount() 
    { 
        var manager = new BookingManager(); 
        double price = manager.CalculatePrice("CityA", "CityB", SeatCategory.SleeperClass, 3); 
        Assert.AreEqual(100, price); // Candidate must check this 
    } 
 
    [TestMethod] 
    public void MakeBooking_ShouldBookAndReturnCorrectPrice() 
    { 
        var manager = new BookingManager(); 
        var booking = manager.MakeBooking("Alice", 30, "CityA", "CityC", SeatCategory.TwoTierAC); 
        Assert.AreEqual(1200, booking.Price); 
    } 
 
    [TestMethod] 
    public void GetVacantSeats_ShouldReturnRemainingSeats() 
    { 
        var manager = new BookingManager(); 
        manager.MakeBooking("Bob", 25, "CityA", "CityB", SeatCategory.SleeperClass); 
        int vacant = manager.GetVacantSeats(SeatCategory.SleeperClass); 
        Assert.AreEqual(49, vacant); 
    } 
} 
