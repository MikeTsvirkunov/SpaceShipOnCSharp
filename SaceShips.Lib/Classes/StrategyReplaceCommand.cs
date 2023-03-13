using SaceShips.Lib.Interfaces;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading;
using Hwdtech;

namespace SaceShips.Lib.Classes;

public class StrategyReplaceCommand : SaceShips.Lib.Interfaces.ICommand
{
    IMethodChangeable thread;
    IStartegy f;
    public StrategyReplaceCommand(IMethodChangeable thread, IStartegy f)
    {
        this.thread = thread;
        this.f = f;
    }
    public void action()
    {
        thread.ChangeMethod(this.f);
    }
}
