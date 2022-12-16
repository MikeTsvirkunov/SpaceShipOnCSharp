using SaceShips.Lib.Classes;
namespace SaceShips.Lib.Interfaces;
public interface ISpeedChangeable
{
    Fraction speed { get; }
    void speed_change();
}
