using SaceShips.Lib.Interfaces;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading;
using Hwdtech;

namespace SaceShips.Lib.Classes;

public class StandartChangeThreadMethodCommand : SaceShips.Lib.Interfaces.ICommand
{
    ServerThreadStrategy thread;
    IStartegy new_strategy;
    public StandartChangeThreadMethodCommand(ServerThreadStrategy thread, IStartegy new_strategy)
    {
        this.thread = thread;
        this.new_strategy = new_strategy;
    }
    public void action()
    {
        this.thread.ChangeMethod(this.new_strategy);
    }
}
