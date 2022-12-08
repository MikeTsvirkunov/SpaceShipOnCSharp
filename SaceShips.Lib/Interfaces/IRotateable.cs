using SaceShips.Lib.Classes;
namespace SaceShips.Lib.Interfaces;
public interface IRotateable
{
    Fraction angleSpeed { get; set; }
    Fraction angle { get; set; }
}
