using SaceShips.Lib.Classes;
namespace SaceShips.Lib.Interfaces;
public interface IUObject
{
    void set_property(string key, object value);
    object get_property(string key);
}
