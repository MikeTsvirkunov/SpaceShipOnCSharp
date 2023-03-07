using SaceShips.Lib.Interfaces;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading;
using Hwdtech;

namespace SaceShips.Lib.Classes;

public class StrategyReplaceCommand : SaceShips.Lib.Interfaces.ICommand
{
    ServerThreadStrategy thread;
    IStartegy f;
    public StrategyReplaceCommand(ServerThreadStrategy thread, IStartegy f)
    {
        this.thread = thread;
        this.f = f;
    }
    public void action()
    {
        thread.ChangeMethod(this.f);
    }
}
