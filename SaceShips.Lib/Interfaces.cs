using System.Collections.Concurrent;
namespace SaceShips.Lib;

public interface Movement
{
    void move(int t);
}

public interface InterfaceObject
{
    ConcurrentDictionary<string, dynamic> GetAllParams();
    dynamic GetParam(string key);
    void SetParam(string key, dynamic value);
    bool ParamExist(string key);
}

