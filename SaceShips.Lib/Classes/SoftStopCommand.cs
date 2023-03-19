using SaceShips.Lib.Interfaces;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading;
using Hwdtech;

namespace SaceShips.Lib.Classes;

public class SoftStopServerThreadCommand : SaceShips.Lib.Interfaces.ICommand
{
    BlockingCollection<SaceShips.Lib.Interfaces.ICommand> queue;
    ServerThreadStrategy thread;
    public SoftStopServerThreadCommand(ServerThreadStrategy thread, BlockingCollection<SaceShips.Lib.Interfaces.ICommand> queue)
    {
        this.thread = thread;
        this.queue = queue;
    }
    public void action()
    {
        // this.queue.Add(new HardStopServerThreadCommand(this.thread));
        // Hwdtech.IoC.Resolve<SaceShips.Lib.Interfaces.ICommand>("SpaceShip.Lib.HardStopServerThreadCommand", this.thread);
        // Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.HardStopServerThreadCommand", (object[] args) => new HardStopServerThreadCommand((ServerThreadStrategy)args[0])).Execute();
        this.queue.Add(Hwdtech.IoC.Resolve<SaceShips.Lib.Interfaces.ICommand>("SpaceShip.Lib.HardStopServerThreadCommand", this.thread));
    }
}
