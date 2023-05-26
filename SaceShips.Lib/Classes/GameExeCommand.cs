using SaceShips.Lib.Interfaces;
using System.Collections.Generic;
using Hwdtech;

namespace SaceShips.Lib.Classes;

public class GameExeCommand : SaceShips.Lib.Interfaces.ICommand, IMethodChangeable, IStopable
{
    object queue;
    object scope;
    bool run = true;
    IStartegy f;
    public GameExeCommand(object queue, IStartegy f, object scope)
    {
        this.queue = queue;
        this.f = f;
        this.scope = scope;
    }
    public void action()
    {
        Hwdtech.IoC.Resolve<SaceShips.Lib.Interfaces.ICommand>("SpaceShip.Lib.SetScope", this.scope).action();
        var time_counter = Hwdtech.IoC.Resolve<System.Object>("SpaceShip.Lib.GameExeCommand.TimeCounter");
        // if (StopWatc)
        Hwdtech.IoC.Resolve<SaceShips.Lib.Interfaces.ICommand>("SpaceShip.Lib.GameExeCommand.TimeCounter.Start", time_counter).action();
        while (Hwdtech.IoC.Resolve<System.Boolean>("SpaceShip.Lib.GameExeCommand.Run", this.run, time_counter, queue))
        {
            // Hwdtech.IoC.Resolve<SaceShips.Lib.Interfaces.ICommand>("SpaceShip.Lib.Execute.GameExeCommandStrategy", this.f, queue).action();
            try{
                Hwdtech.IoC.Resolve<SaceShips.Lib.Interfaces.ICommand>("SpaceShip.Lib.Execute.GameExeCommandStrategy", this.f, queue).action();
            }
            catch (System.Exception e){
                Hwdtech.IoC.Resolve<SaceShips.Lib.Interfaces.ICommand>("SpaceShip.Lib.DefaultExceptionHandler", e).action();
            }
        }
    }
    public void ChangeMethod(IStartegy f)
    {
        this.f = f;
    }
    public void Stop()
    {
        this.run = false;
    }
}
