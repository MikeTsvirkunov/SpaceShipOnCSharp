using SaceShips.Lib.Interfaces;

namespace SaceShips.Lib.Classes;
public class GetPropertyStrategy : SaceShips.Lib.Interfaces.IStartegy
{
    public object execute(params object[] args)
    {
        string prop = (string)args[0];
        IUObject obj = (IUObject)args[1];
        return (obj.get_property(prop));
    }
}
