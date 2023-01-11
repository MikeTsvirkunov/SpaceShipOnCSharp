using SaceShips.Lib.Interfaces;
namespace SaceShips.Lib.Classes;
using System.Collections.Generic;
using System.Linq;
public class MacroCommand : SaceShips.Lib.Interfaces.ICommand
{
    List<SaceShips.Lib.Interfaces.ICommand> cmds;
    public MacroCommand(object cmds)
    {
        this.cmds = (List<SaceShips.Lib.Interfaces.ICommand>) cmds;
    }

    public void action()
    {
        cmds.ForEach(c => c.action());
    }
}