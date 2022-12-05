using SaceShips.Lib.Interfaces;
namespace SaceShips.Lib.IOC_container;
public interface IStrategy
{
    string name {get; set;}
    ICommand cmd {get; set;}

    object execute();
}