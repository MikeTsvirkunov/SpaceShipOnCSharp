using SaceShips.Lib.Interfaces;
namespace SaceShips.Lib.Classes;
using System.Collections.Generic;
using Hwdtech;

public class StartRotateCommand : SaceShips.Lib.Interfaces.ICommand
{
    IRotateCommandStartable obj;

    public StartRotateCommand(IRotateCommandStartable obj)
    {
        this.obj = obj;
    }

    public void action()
    {
        Hwdtech.IoC.Resolve<SaceShips.Lib.Interfaces.ICommand>("SpaceShip.Lib.Comands.ChangeAngleSpeed", this.obj.obj, this.obj.angleSpeed);
        var smth_cmd = Hwdtech.IoC.Resolve<SaceShips.Lib.Interfaces.ICommand>("SpaceShip.Lib.Comands.Rotate", obj.obj);
        var q = Hwdtech.IoC.Resolve<Queue<SaceShips.Lib.Interfaces.ICommand>>("SpaceShip.Vars.Queue");
        this.obj.queue.Enqueue(smth_cmd);
    }
}
