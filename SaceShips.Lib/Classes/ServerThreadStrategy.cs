using SaceShips.Lib.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Collections.Concurrent;

namespace SaceShips.Lib.Classes;

public class ServerThreadStrategy: IStartegy{//, IMethodChangeable{

    SaceShips.Lib.Interfaces.IStartegy f;
    // bool r = true;
    Thread thread;
    BlockingCollection<SaceShips.Lib.Interfaces.ICommand> queue;
// Gregory endruse основы многопоточного программирования
    public ServerThreadStrategy(SaceShips.Lib.Interfaces.IStartegy f, BlockingCollection<SaceShips.Lib.Interfaces.ICommand> x){
        this.f = f;
        this.queue = x;
        this.thread = new Thread(() => {((SaceShips.Lib.Interfaces.ICommand)this.f.execute(this.queue)).action();});
        // this.thread = new Thread(() => {while (this.r && this.queue.Count() > 0){((SaceShips.Lib.Interfaces.ICommand)(this.queue.Take())).action();}});
    }

    public void ChangeMethod(SaceShips.Lib.Interfaces.IStartegy f){
        this.f = f;
    }

    // public void Stop()
    // {
    //     this.r = false;
    // }

    public object execute(params object[] args){
        return (object)this.thread;
    }
}
