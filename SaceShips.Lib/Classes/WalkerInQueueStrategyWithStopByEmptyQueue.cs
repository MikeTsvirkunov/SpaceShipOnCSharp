using SaceShips.Lib.Interfaces;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading;
using Hwdtech;

namespace SaceShips.Lib.Classes;

public class WalkerInQueueStrategyWithStopByEmptyQueue : SaceShips.Lib.Interfaces.IStartegy
{
    Func<object, object> f_mod;
    ServerThreadStrategy thread;
    public WalkerInQueueStrategyWithStopByEmptyQueue(Func<object, object> f_mod, ServerThreadStrategy thread){
        this.f_mod = f_mod;
        this.thread = thread;
    }
    public object execute(params object[] args)
    {
        if (Hwdtech.IoC.Resolve<System.Boolean>("SpaceShip.Lib.IsQueueEmpty", args[0])) return Hwdtech.IoC.Resolve<SaceShips.Lib.Interfaces.ICommand>("SpaceShip.Lib.HardStopServerThreadCommand", this.thread);
        else return f_mod(args[0]);
    }
}
