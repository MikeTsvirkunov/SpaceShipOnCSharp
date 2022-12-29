using SaceShips.Lib.Interfaces;
namespace SaceShips.Lib.Classes;
using System.Collections.Generic;
using Hwdtech;
using System.Linq;
public class MacroCommand : SaceShips.Lib.Interfaces.ICommand
{
    List<SaceShips.Lib.Interfaces.ICommand> cmds;

    public MacroCommand(List<SaceShips.Lib.Interfaces.ICommand> cmd_names)
    {
        this.cmds = cmd_names;
    }

    public void action()
    {
        cmds.ForEach(c => c.action());
    }
}
