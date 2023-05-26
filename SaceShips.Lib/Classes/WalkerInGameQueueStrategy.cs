using SaceShips.Lib.Interfaces;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading;
using Hwdtech;

namespace SaceShips.Lib.Classes;

public class WalkerInGameQueueStrategy : SaceShips.Lib.Interfaces.IStartegy
{
    Func<object, object> f_mod;
    public WalkerInGameQueueStrategy(Func<object, object> f_mod)
    {
        this.f_mod = f_mod;
    }
    public object execute(params object[] args)
    {
        return this.f_mod(args[0]);
    }
}