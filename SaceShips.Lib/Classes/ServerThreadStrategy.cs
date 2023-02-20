using SaceShips.Lib.Interfaces;
using System.Collections.Generic;
using System.Threading;

namespace SaceShips.Lib.Classes;

class ServerThreadStrategy: IStartegy{

    Queue<SaceShips.Lib.Interfaces.ICommand> queue;

    SaceShips.Lib.Interfaces.IStartegy f;

    void ServerThreadStrategy(Queue<SaceShips.Lib.Interfaces.ICommand> queue, SaceShips.Lib.Interfaces.IStartegy f){
        this.queue = queue;
        this.f = f;
    }

    public object execute(params object[] args){
        return (object)(new Thread(this.f.execute(this.queue)));
    }
}