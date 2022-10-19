using System.Collections.Concurrent;
namespace SaceShips.Lib;

public interface ICommand
{
    InterfaceObject moveable_obj  { get; set; }
    void action();
}

public interface InterfaceObject
{
    ConcurrentDictionary<string, dynamic> parametrs { get; set; }
    ConcurrentDictionary<string, dynamic> GetAllParams();
    dynamic GetParam(string key);
    void SetParam(string key, dynamic value);
    bool ParamExist(string key);
}


public interface IVectorise{}
public interface IFraction{
    double GetDouble();
    int[] GetFract();
}
