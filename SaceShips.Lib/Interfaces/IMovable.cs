using SaceShips.Lib.Classes;
namespace SaceShips.Lib.Interfaces;
public interface IMovable
    {
        Vector position { get; set; }
        Vector velocity { get; }
    }
