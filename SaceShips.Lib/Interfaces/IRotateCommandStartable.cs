using SaceShips.Lib.Classes;
namespace SaceShips.Lib.Interfaces;

public interface IRotateCommandStartable: ICommand
{
    IUObject obj { get; }
    Fraction angleSpeed { get; }
    Queue<ICommand> queue { get; }
}
