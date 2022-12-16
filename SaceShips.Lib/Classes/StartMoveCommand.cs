using SaceShips.Lib.Interfaces;
namespace SaceShips.Lib.Classes;
using Hwdtech;

public class StartRotateCommand : SaceShips.Lib.Interfaces.ICommand
{
    ISpeedChangeable obj;

    public StartRotateCommand(ISpeedChangeable obj)
    {
        this.obj = obj;
    }

    public void action()
    {
        //give to object speed
        // Hwdtech.IoC.Resolve<Hwdtech.ICommand>("SpaceShip.Lib.Comands.Set", obj.obj, obj.set_property("angleSpeed"), obj.speed_change);
        // return some command
        var smth_cmd = Hwdtech.IoC.Resolve<Hwdtech.ICommand>("SpaceShip.Lib.Comands.Rotate", obj.obj);
        // push cmd to queue
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("SpaceShip.Vars.Equeue", Hwdtech.IoC.Resolve<Hwdtech.ICommand>("SpaceShip.Vars.Queue", obj.obj), smth_cmd);
    }
}
