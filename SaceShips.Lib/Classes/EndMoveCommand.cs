using SaceShips.Lib.Interfaces;

namespace SaceShips.Lib.Classes;
public class EndMoveCommand : SaceShips.Lib.Interfaces.ICommand
{
    IMoveCommandEndable stop_obj;
    public EndMoveCommand(IMoveCommandEndable obj)
    {
        stop_obj = obj;
    }

    public void action()
    {
        var obj = stop_obj.uobj;
        var cmd = stop_obj.move_command;
        var properties = stop_obj.properties;

        properties.ToList().ForEach(property => Hwdtech.IoC.Resolve<SaceShips.Lib.Interfaces.ICommand>("Game.Object.DeleteProperty", obj, property));
        Hwdtech.IoC.Resolve<SaceShips.Lib.Interfaces.ICommand>("Game.Commands.InjectEmptyCommand", cmd);
    }
}
