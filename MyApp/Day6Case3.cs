using System;
using System.Collections.Generic;
public class ConfigurationManager 
{ 
    private static readonly Lazy<ConfigurationManager> _instance = new Lazy<ConfigurationManager>(() => new ConfigurationManager());
    private readonly Lazy<Dictionary<string, string>> _configData; 
 
    private ConfigurationManager() 
    { 
        _configData = new Lazy<Dictionary<string, string>>(LoadConfiguration);
    } 
    
    public static ConfigurationManager Instance => _instance.Value;
 
    public string GetValue(string key) 
    { 
        return _configData.Value.ContainsKey(key) ? _configData.Value[key] : null; 
    } 
    
    private Dictionary<string, string> LoadConfiguration()
    {
        Console.WriteLine("Loading Configuration...");
        return new Dictionary<string, string>
        {
            {"ApiUrl", "https://api.example.com"},
            {"ApiKey", "ABC123"},
            {"Timeout", "30"}
        };
    }
}
public class Program
{
    public static void Main()
    {
        var config = ConfigurationManager.Instance;
        string apiUrl = config.GetValue("ApiUrl");
        Console.WriteLine(apiUrl);
    }
}
