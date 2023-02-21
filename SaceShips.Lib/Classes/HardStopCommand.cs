using SaceShips.Lib.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading;

namespace SaceShips.Lib.Classes;

public class HardStopServerThreadCommand : SaceShips.Lib.Interfaces.ICommand
{
    public void action()
    {
        Thread.CurrentThread.Interrupt();
    }
}