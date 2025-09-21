
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

/*
We are writing software to analyze logs for toll booths on a highway. This highway is a divided highway with limited access; the only way on to or off of the highway is through a toll booth.

There are three types of toll booths:
* ENTRY (E in the diagram) toll booths, where a car goes through a booth as it enters the highway.
* EXIT (X in the diagram) toll booths, where a car goes through a booth as it exits the highway.
* MAINROAD (M in the diagram), which have sensors that record a license plate as a car drives through at full speed.


       Exit Booth                         Entry Booth
           |                                   |
           X                                   E
            \                                 /
---<------------<---------M---------<-----------<---------<----
                                        (West-bound side)

===============================================================

                                        (East-bound side)
------>--------->---------M--------->--------->--------->------
            /                                 \
           E                                   X
           |                                   |
       Entry Booth                         Exit Booth
*/

/*
For our first task:
1-1) Read through and understand the code and comments below. Feel free to run the code and tests.
1-2) The tests are not passing due to a bug in the code. Make the necessary changes to LogEntry to fix the bug.
*/

public class LogEntry
{
   /**
   * Represents an entry from a single log line. Log lines look like this in the file:
   *
   * 34400.409 SXY288 210E ENTRY
   *
   * Where:
   * * 34400.409 is the timestamp in seconds since the software was started.
   * * SXY288 is the license plate of the vehicle passing through the toll booth.
   * * 210E is the location and traffic direction of the toll booth. Here, the toll
   *     booth is at 210 kilometers from the start of the tollway, and the E indicates
   *     that the toll booth was on the east-bound traffic side. Tollbooths are placed
   *     every ten kilometers.
   * * ENTRY indicates which type of toll booth the vehicle went through. This is one of
   *     "ENTRY", "EXIT", or "MAINROAD".
   **/

   public double Timestamp { get; private set; }
   public string LicensePlate { get; private set; }
   public string BoothType { get; private set; }
   public int Location { get; private set; }
   public string Direction { get; private set; }

   public LogEntry(string logLine)
   {
       string[] tokens = logLine.Split(" ");
       Timestamp = double.Parse(tokens[0]);
       LicensePlate = tokens[1];
       BoothType = tokens[3];
       Location = int.Parse(tokens[2].Substring(0, tokens[2].Length - 1));
       char directionLetter = tokens[2][tokens[2].Length - 1];
       Console.WriteLine("Running LogEntry");
       if (directionLetter == 'E')
       {
           Direction = "EAST";
       }
       else if (directionLetter == 'W')
       {
           Direction = "WEST";
       }
       else
       {
           Debug.Assert(false, "Invalid direction");
       }
   }

   public override string ToString()
   {
       return string.Format("<LogEntry timestamp: {0}  license: {1}  location: {2}  direction: {3}  booth type: {4}>",
           Timestamp, LicensePlate, Location, Direction, BoothType);
   }
}

public class LogFile : List<LogEntry>
{
   /*
   * Represents a file containing a number of log lines, converted to LogEntry
   * objects.
   */
   public LogFile(StreamReader fileHandle)
   {
       Console.WriteLine("Running LogFile");
       string line;
       while ((line = fileHandle.ReadLine()) != null)
       {
           LogEntry logEntry = new LogEntry(line.Trim());
           Add(logEntry);
       }
   }
}

public class Solution
{
   public static void TestLogFile()
   {
       Console.WriteLine("Running TestLogFile");
       using (StreamReader fileHandle = new StreamReader("/content/test/tollbooth_small.log"))
       {
           LogFile logFile = new LogFile(fileHandle);
           Debug.Assert(logFile.Count == 13, "Length check failed");
           foreach (LogEntry entry in logFile)
           {
               Debug.Assert(entry is LogEntry, "Type check failed");
           }
       }
   }

   public static void TestLogEntry()
   {
       Console.WriteLine("Running TestLogEntry");

       string logLine = "44776.619 KTB918 310E MAINROAD";
       LogEntry logEntry = new LogEntry(logLine);
       Console.WriteLine($"actual {logEntry.Timestamp}");
       Debug.Assert(logEntry.Timestamp.Equals(44776.619));
       Debug.Assert(logEntry.LicensePlate.Equals("KTB918"));
       Debug.Assert(logEntry.Location.Equals(310));
       Debug.Assert(logEntry.Direction.Equals("EAST"));
       Debug.Assert(logEntry.BoothType.Equals("MAINROAD"));

       logLine = "52160.132 ABC123 400W ENTRY";
       logEntry = new LogEntry(logLine);
       Debug.Assert(logEntry.Timestamp.Equals(52160.132));
       Debug.Assert(logEntry.LicensePlate.Equals("ABC123"));
       Debug.Assert(logEntry.Location.Equals(400));
       Debug.Assert(logEntry.Direction.Equals("WEST"));
       Debug.Assert(logEntry.BoothType.Equals("ENTRY"));
   }

   public static void Main()
   {
       TestLogFile();
       TestLogEntry();
       Console.WriteLine("All tests passed.");
   }
}
