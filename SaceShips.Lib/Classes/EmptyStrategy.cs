using SaceShips.Lib.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading;

namespace SaceShips.Lib.Classes;

public class EmptyStrategy : SaceShips.Lib.Interfaces.IStartegy
{
    
    public object execute(params object[] args)
    {
        return (object)(new EmptyCommand());
    }
}

public class EmptyCommand : SaceShips.Lib.Interfaces.ICommand
{
    public void action(){}
}
