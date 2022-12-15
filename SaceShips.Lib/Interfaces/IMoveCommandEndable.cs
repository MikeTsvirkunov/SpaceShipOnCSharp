// using SaceShips.Lib.Classes;
namespace SaceShips.Lib.Interfaces;

public interface IMoveCommandEndable: ICommand, IInjectable
{
    object Obj { get; set; }
    ICommand Cmd { get; set; }
    Queue<IMoveCommandEndable> queue { get; set; }
}


//Реализован интерфейс MoveCommandEndable, который возвращает команду Move, которую необходимо завершить, объект, который движется с помощью данной команды, очередь команд Queue<Command>
