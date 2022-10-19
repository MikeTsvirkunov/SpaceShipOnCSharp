using System.Collections.Concurrent;
using System.Collections.Generic;


namespace SaceShips.Lib;

public class UIObject : InterfaceObject
{
    // Dictionary with obj params
    public ConcurrentDictionary<string, dynamic> parametrs {get; set;}
    // private ConcurrentDictionary<string, dynamic> parametrs;
    public UIObject(Dictionary<string, dynamic> p)
    {
        this.parametrs = new ConcurrentDictionary<string, dynamic>(p);
    }
    public ConcurrentDictionary<string, dynamic> GetAllParams() { return parametrs; }
    public dynamic GetParam(string key) { return parametrs[key]; }
    public void SetParam(string key, dynamic value) { parametrs[key] = value; }
    public bool ParamExist(string key) { return this.parametrs.ContainsKey(key); }
}