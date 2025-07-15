using System.Collections.Generic;
using Xunit;
public class User 
{ 
    public string Name { get; set; } 
    public int Age { get; set; } 
    public string Country { get; set; } 
}  
public class UserDirectory 
{ 
    private Dictionary<string, User> _users = new Dictionary<string, User>();  
    public void AddUser(User user) 
    {        _users[user.Name] = user;    }  
    public List<User> GetEligibleUsers() 
    {        return _users.Values 
                     .Where(u => u.Age > 59) 
                     .ToList();    }} 

public class UserDirectoryTests
{
    [Fact]
    public void GetEligibleUsers_ReturnsOnlyUsersOver59()
    {
        var directory = new UserDirectory();
        var user1 = new User { Name = "Alice", Age = 65, Country = "USA" };
        var user2 = new User { Name = "Bob", Age = 40, Country = "UK" };
        var user3 = new User { Name = "Charlie", Age = 60, Country = "India" };
        var user4 = new User { Name = "David", Age = 59, Country = "Canada" };
        directory.AddUser(user1);
        directory.AddUser(user2);
        directory.AddUser(user3);
        directory.AddUser(user4);
        List<User> eligibleUsers = directory.GetEligibleUsers();
        Assert.Equal(2, eligibleUsers.Count);
        Assert.Contains(eligibleUsers, u => u.Name == "Alice");
        Assert.Contains(eligibleUsers, u => u.Name == "Charlie");
        Assert.DoesNotContain(eligibleUsers, u => u.Name == "Bob");
        Assert.DoesNotContain(eligibleUsers, u => u.Name == "David");
    }
}
 
