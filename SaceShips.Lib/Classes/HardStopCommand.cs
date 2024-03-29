using SaceShips.Lib.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading;

namespace SaceShips.Lib.Classes;

public class HardStopServerThreadCommand : SaceShips.Lib.Interfaces.ICommand
{
    IStopable thread; 
    public HardStopServerThreadCommand(ServerThreadStrategy thread){
        this.thread = thread;
    }
    public void action()
    {
        this.thread.Stop();
    }
}
