using System.Collections.Concurrent;
namespace SaceShips.Lib;

public interface ICommand
{
    void action();
}

public interface IMoveable{
    Vector frontSpeed {get; set;}
    Vector coord {get; set;}
}

public interface IRotateable{
    int angleSpeed {get; set;}
    int angle {get; set;}
}