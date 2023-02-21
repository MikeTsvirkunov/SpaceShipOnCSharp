using SaceShips.Lib.Interfaces;
using System.Collections.Generic;
using System.Threading;

namespace SaceShips.Lib.Classes;

public class ServerThreadStrategy: IStartegy{

    SaceShips.Lib.Interfaces.IStartegy f;

    public ServerThreadStrategy(SaceShips.Lib.Interfaces.IStartegy f){
        this.f = f;
    }

    public object execute(params object[] args){
        return (object)(new Thread(() => {((SaceShips.Lib.Interfaces.ICommand)this.f.execute((Queue<SaceShips.Lib.Interfaces.ICommand>)args[0])).action();}));
    }
}