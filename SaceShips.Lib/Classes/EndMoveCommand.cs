using SaceShips.Lib.Interfaces;
namespace SaceShips.Lib.Classes; 
//Реализована команда EndMoveCommand, которая
// удаляет значение скорости из движущегося объекта.
// Указание.Удаляемая команда должна быть реализована с помощью паттерна Bridge.Команда хранит внутри ссылку на команду, которая и обеспечивает необходимое действие.При удалении - удаляется не сама команда, а переписывается ссылка на внутреннюю команду - 
public class EndMoveCommand: ICommand{
    object obj;
    Queue<IMoveCommandEndable> queue;
    public EndMoveCommand(Queue<IMoveCommandEndable> queue, object obj){
        this.obj = obj;
        this.queue = queue;
    }
    public void action(){
        IMoveCommandEndable cmd_to_end = (from cmd in queue where cmd.Obj.Equals(this.obj) select cmd).First();
        cmd_to_end.inject(new NullCommand());
    }
}