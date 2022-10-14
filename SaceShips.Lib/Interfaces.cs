using System.Collections.Concurrent;
namespace SaceShips.Lib;

public interface ICommand
{
    void action(int t);
}

public interface InterfaceObject
{
    ConcurrentDictionary<string, dynamic> GetAllParams();
    dynamic GetParam(string key);
    void SetParam(string key, dynamic value);
    bool ParamExist(string key);
}


public interface IVectorise
{
    
}
