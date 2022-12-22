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
        //give to object speed

        // Hwdtech.IoC.Resolve<SaceShips.Lib.Interfaces.ICommand>("SpaceShip.Lib.Comands.Set", obj.obj, "angleSpeed", obj.speed_change);
        Hwdtech.IoC.Resolve<SaceShips.Lib.Interfaces.ICommand>("SpaceShip.Lib.Comands.ChangeAngleSpeed", this.obj.obj, this.obj.angleSpeed);
        // return some command
        var smth_cmd = Hwdtech.IoC.Resolve<SaceShips.Lib.Interfaces.ICommand>("SpaceShip.Lib.Comands.Rotate", obj.obj);
        // push cmd to queue
        var q = Hwdtech.IoC.Resolve<Queue<SaceShips.Lib.Interfaces.ICommand>>("SpaceShip.Vars.Queue");
        // this.obj.queue = q;
        this.obj.queue.Enqueue(smth_cmd);
    }
}
