using System;
using System.Collections.Generic;
public class DataManager 
{ 
    private readonly Lazy<List<string>> _data; 
    public DataManager() 
    { 
        _data = new Lazy<List<string>>(LoadData);
    }
 
    private List<string> LoadData() 
    { 
        return new List<string> { "Alpha", "Beta", "Gamma" }; 
    } 
} 
[TestMethod] 
public void Data_ShouldBeLoadedLazily() 
{ 
    var manager = new DataManager(); 
    var lazyField = (Lazy<list<string>>)GetPrivateField(manager, "_data");
    Assert.IsFalse(lazyField.IsValueCreated);
    var data = manager.Data;
    Assert.IsTrue(lazyField.IsValueCreated);
    Assert.IsNotNull(data); 
} 
