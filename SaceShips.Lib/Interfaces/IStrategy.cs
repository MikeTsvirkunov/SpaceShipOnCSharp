using SaceShips.Lib.Classes;
namespace SaceShips.Lib.Interfaces;
public interface IStartegy
{
    public object execute(params object[] args);
}
