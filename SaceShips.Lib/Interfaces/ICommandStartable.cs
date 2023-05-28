using SaceShips.Lib.Classes;
namespace SaceShips.Lib.Interfaces;

public interface ICommandStartable : ICommand
{
    object queue { get; }
}
