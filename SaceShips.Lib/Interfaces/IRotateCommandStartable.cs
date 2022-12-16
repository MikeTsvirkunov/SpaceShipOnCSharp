using SaceShips.Lib.Classes;
namespace SaceShips.Lib.Interfaces;

public interface IRotateCommandStartable: ICommand
{
    IUObject obj { get; }
    Fraction angleSpeed { get; }
    Queue<ICommand> queue { get; }
}


//Реализован интерфейс MoveCommandEndable, который возвращает команду Move, которую необходимо завершить, объект, который движется с помощью данной команды, очередь команд Queue<Command>
