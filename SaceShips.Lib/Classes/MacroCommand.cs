using SaceShips.Lib.Interfaces;
namespace SaceShips.Lib.Classes;
using System.Collections.Generic;
using Hwdtech;
using System.Linq;
public class MacroCommand : SaceShips.Lib.Interfaces.ICommand
{
    IUObject obj;
    List<SaceShips.Lib.Interfaces.IStartegy> strategies;

    public MacroCommand(IUObject obj, List<SaceShips.Lib.Interfaces.IStartegy> strategies)
    {
        this.obj = (IUObject)obj;
        this.strategies = strategies;
    }

    public void action()
    {
        // foreach (var c in strategies)
        // {
        //     SaceShips.Lib.Interfaces.ICommand com = (SaceShips.Lib.Interfaces.ICommand)((IStartegy)c).execute(obj);
        //     com.action();
        // }
        strategies.ForEach(c => {((SaceShips.Lib.Interfaces.ICommand)((IStartegy)c).execute(obj)).action();});
    }
}
