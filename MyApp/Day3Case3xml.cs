using System;
using System.Xml;
using System.IO;
public class Program
{
   public static void Main()
   {
       string xmlContent = @"<settings>
                           <setting key='maxUsers'>100</setting>
                           <setting key='isEnabled'>true</setting>
                           <setting key='launchDate'>2025-07-01</setting>
                           </settings>";
       string tempPath = "config.xml";
       File.WriteAllText(tempPath, xmlContent);
       ConfigReader config = new ConfigReader(tempPath);
       int maxUsers = config.GetValue("maxUsers", 10);
       bool isEnabled = config.GetValue("isEnabled", false);
       DateTime launchDate = config.GetValue("launchDate", DateTime.Now);
       Console.WriteLine($"Max Users: {maxUsers}");
       Console.WriteLine($"Is Enabled: {isEnabled}");
       Console.WriteLine($"Launch Date: {launchDate:yyyy-MM-dd}");
   }
}
public class ConfigReader
{
   private readonly XmlDocument _doc;
   public ConfigReader(string xmlPath)
   {
       _doc = new XmlDocument();
       _doc.Load(xmlPath);
   }
   public T GetValue<T>(string key, T defaultValue)
   {
       string val = _doc.SelectSingleNode($"//setting[@key='{key}']")?.InnerText;
       if (string.IsNullOrWhiteSpace(val))
           return defaultValue;
       try
       {
           if (typeof(T) == typeof(int))
               return (T)(object)int.Parse(val);
           if (typeof(T) == typeof(bool))
               return (T)(object)bool.Parse(val);
           if (typeof(T) == typeof(DateTime))
               return (T)(object)DateTime.Parse(val);
           return (T)Convert.ChangeType(val, typeof(T));
       }
       catch
       {
           return defaultValue;
       }
   }
}
