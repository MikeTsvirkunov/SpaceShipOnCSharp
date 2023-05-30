using SaceShips.Lib.Interfaces;
namespace SaceShips.Lib.Classes;
using System.Collections.Generic;
using System.Linq;
using Scriban;
public class IterGeneratorCommand : SaceShips.Lib.Interfaces.ICommand
{
    object iterator;
    object stop;
    public IterGeneratorCommand(object iterator, object stop)
    {
        this.iterator = iterator;
        this.stop = stop;
    }
    public void action()
    {
        Hwdtech.IoC.Resolve<SaceShips.Lib.Interfaces.ICommand>("SpaceShip.Lib.IterGeneratorCommand.Run", this.iterator, this.stop, this).action();
    }
}
