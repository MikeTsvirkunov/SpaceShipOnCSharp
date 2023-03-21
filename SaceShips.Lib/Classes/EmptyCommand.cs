using SaceShips.Lib.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading;

namespace SaceShips.Lib.Classes;

public class EmptyCommand: SaceShips.Lib.Interfaces.ICommand
{
    public EmptyCommand(){
    }
    public void action()
    {
    }
}
