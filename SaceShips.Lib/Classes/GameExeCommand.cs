using SaceShips.Lib.Interfaces;
using System.Collections.Generic;
using Hwdtech;

namespace SaceShips.Lib.Classes;

public class GameExeCommand : SaceShips.Lib.Interfaces.ICommand, ICommandStartable
{
    public object queue { get; }
    object scope;
    public GameExeCommand(object queue, object scope)
    {
        this.queue = queue;
        this.scope = scope;
    }
    public void action()
    {
        try{
            Hwdtech.IoC.Resolve<SaceShips.Lib.Interfaces.ICommand>("SpaceShip.Lib.SetScope", this.scope).action();
            Hwdtech.IoC.Resolve<SaceShips.Lib.Interfaces.ICommand>("SpaceShip.Lib.Game.Queue.GetCommand", this.queue).action();
            Hwdtech.IoC.Resolve<SaceShips.Lib.Interfaces.ICommand>("SpaceShip.Lib.Game.Run", this).action();
        }
        catch (System.Exception e){
            Hwdtech.IoC.Resolve<SaceShips.Lib.Interfaces.ICommand>("SpaceShip.Lib.Game.ExceptionHandler", e).action();
        }
    }
}
