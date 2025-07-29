using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

public enum MembershipStatus
{
/*
Membership Status is of three types: BASIC, PRO and ELITE.
BASIC is the default membership a new member gets.
PRO and ELITE are paid memberships for the gym.
*/
BASIC = 1,
PRO = 2,
ELITE = 3
}

public class Member
{
/* Data about a gym member. */
public int MemberId { get; set; }
public string Name { get; set; }
public MembershipStatus MembershipStatus { get; set; }

public Member(int memberId, string name, MembershipStatus membershipStatus)
{
MemberId = memberId;
Name = name;
MembershipStatus = membershipStatus;
}

public override string Tostring ()
{
return $"Member ID: {MemberId}, Name: {Name}, Membership Status: {MembershipStatus}";
}
}

public class Membership
{
/*
Data for managing a gym membership, and methods which staff can
use to perform any queries or updates.
*/
private List<Member> members;
public Dictionary<int, List<Workout>> workoutRegister = new Dictionary<int, List<Workout>>();

public Membership()
{
members = new List<Member>();
}

public void AddMember(Member member)
{
members.Add(member);
}

public void UpdateMembership(int memberId, MembershipStatus membershipStatus)
{
Member memberToUpdate = members.Find(member => member.MemberId == memberId) ?? throw new ArgumentException();
if (memberToUpdate != null)
{
memberToUpdate.MembershipStatus = membershipStatus;
}
}

public Dictionary<string, double> GetMembershipStatistics()
{
int totalMembers = members.Count;
int totalPaidMembers = members.Count(member => member.MembershipStatus == MembershipStatus.ELITE || member.MembershipStatus == MembershipStatus.PRO);
double conversionRate = (double)totalPaidMembers / totalMembers * 100;

return new Dictionary<string, double>
{
{ "total_members", totalMembers },
{ "total_paid_members", totalPaidMembers },
{ "conversion_rate", conversionRate }
};
}
}

public class Workout
{
/*
* This class represents a single workout session for a member.
* Each object of the Workout class has a unique ID, as well as
* a start time and end time that are represented in the number
* of minutes spent from the start of the day.
*/

public int Id { get; private set; }
public int StartTime { get; private set; }
public int EndTime { get; private set; }

public Workout(int id, int startTime, int endTime)
{
Id = id;
StartTime = startTime;
EndTime = endTime;
}

public int GetDuration()
{
return EndTime - StartTime;
}
}

 public void AddWorkout(int memberId, Workout workout)
   {
       if (!members.Any(m => m.MemberId == memberId))
           return;
       if (!workoutRegister.ContainsKey(memberId))
       {
           workoutRegister[memberId] = new List<Workout>();
       }
       workoutRegister[memberId].Add(workout);
   }
   public Dictionary<int, double> GetAverageWorkoutDurations()
   {
       var result = new Dictionary<int, double>();
       foreach (var kvp in workoutRegister)
       {
           int memberId = kvp.Key;
           List<Workout> workouts = kvp.Value;
           if (workouts.Count > 0)
           {
               double avg = workouts.Average(w => w.GetDuration());
               result[memberId] = avg;
           }
       }
       return result;
   }
}
public class TestSuite
{
/*
This is not a complete test suite, but tests some basic functionality of
the code and shows how to use it.
*/
public static void Main()
{
TestMember();
TestMembership();
TestGetAverageWorkoutDurations();
}
	
public static void TestMember()
{
Console.WriteLine("Running TestMember");
Member testMember = new Member(1, "John Doe", MembershipStatus.BASIC);
Debug.Assert(testMember.MemberId == 1);
Debug.Assert(testMember.Name == "John Doe");
Debug.Assert(testMember.MembershipStatus == MembershipStatus.BASIC);
}

public static void TestMembership()
{
Console.WriteLine("Running TestMembership");
Membership testMembership = new Membership();
Member testMember = new Member(1, "John Doe", MembershipStatus.BASIC);
testMembership.AddMember(testMember);
Debug.Assert(testMembership.GetMembershipStatistics()["total_members"] == 1);

testMembership.UpdateMembership(1, MembershipStatus.PRO);
Debug.Assert(testMembership.GetMembershipStatistics()["total_paid_members"] == 1);

Member testMember2 = new Member(2, "Alex C", MembershipStatus.BASIC);
testMembership.AddMember(testMember2);

Member testMember3 = new Member(3, "Marie C", MembershipStatus.ELITE);
testMembership.AddMember(testMember3);

Member testMember4 = new Member(4, "Joe D", MembershipStatus.PRO);
testMembership.AddMember(testMember4);

Dictionary<string, double> attendanceStats = testMembership.GetMembershipStatistics();
Debug.Assert(attendanceStats["total_members"] == 4);
Debug.Assert(attendanceStats["total_paid_members"] == 3); 
Debug.Assert(Math.Abs(attendanceStats["conversion_rate"] - 75.00) < 0.1);
}


public static void TestGetAverageWorkoutDurations()
{
Console.WriteLine("Running TestGetAverageWorkoutDurations");
Membership testMembership = new Membership();
Member testMember = new Member(12, "John Doe", MembershipStatus.PRO);
testMembership.AddMember(testMember);

Member testMember2 = new Member(22, "Alex C", MembershipStatus.BASIC);
testMembership.AddMember(testMember2);

Member testMember3 = new Member(31, "Marie C", MembershipStatus.ELITE);
testMembership.AddMember(testMember3);

Workout testWorkout1 = new Workout(11, 10, 20);
Workout testWorkout2 = new Workout(24, 15, 35);
Workout testWorkout3 = new Workout(32, 50, 90);
Workout testWorkout4 = new Workout(47, 100, 155);
Workout testWorkout5 = new Workout(56, 120, 200);
Workout testWorkout6 = new Workout(62, 300, 400);
Workout testWorkout7 = new Workout(78, 2000, 2010);
Workout testWorkout8 = new Workout(80, 2010, 2045);

testMembership.AddWorkout(12, testWorkout1);
testMembership.AddWorkout(22, testWorkout2);
testMembership.AddWorkout(31, testWorkout3);
testMembership.AddWorkout(12, testWorkout4);
testMembership.AddWorkout(22, testWorkout5);
testMembership.AddWorkout(31, testWorkout6);
testMembership.AddWorkout(12, testWorkout7);
testMembership.AddWorkout(4, testWorkout8);
   var averages = testMembership.GetAverageWorkoutDurations();
       foreach (var kvp in averages)
       {
           Console.WriteLine($"Member ID: {kvp.Key}, Avg Workout Duration: {kvp.Value} minutes");
       }
       Debug.Assert(Math.Abs(averages[12] - ((10 + 55 + 10) / 3.0)) < 0.01); // (10 + 55 + 10) / 3 = 25
       Debug.Assert(Math.Abs(averages[22] - ((20 + 80) / 2.0)) < 0.01);     // (20 + 80) / 2 = 50
       Debug.Assert(Math.Abs(averages[31] - ((40 + 100) / 2.0)) < 0.01);   // (40 + 100) / 2 = 70
}
}
