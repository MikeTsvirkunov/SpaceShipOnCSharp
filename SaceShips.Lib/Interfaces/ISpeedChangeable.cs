using SaceShips.Lib.Classes;
namespace SaceShips.Lib.Interfaces;
public interface ISpeedChangeable
{
    IUObject obj { get; }
    Fraction speed_change { get; }
}
