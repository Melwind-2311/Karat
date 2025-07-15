using System;
using System.Collections.Generic;
using System.Text;
class ReportBuilder
{
   public string BuildReport(List<string> lines)
   {
       StringBuilder report = new StringBuilder();
       foreach (var line in lines)
       {
           report.AppendLine(line);  
       }
       return report.ToString();
   }
}
class Program
{
   static void Main()
   {
       List<string> reportLines = new List<string>
       {
           "Sales Report",
           "-------------",
           "Product A: 120 units",
           "Product B: 85 units",
           "Product C: 200 units"
       };
       ReportBuilder builder = new ReportBuilder();
       string report = builder.BuildReport(reportLines);
       Console.WriteLine("Generated Report:\n");
       Console.WriteLine(report);
   }
}
