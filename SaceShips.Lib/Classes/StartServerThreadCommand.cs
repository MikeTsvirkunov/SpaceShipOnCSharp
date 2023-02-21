using SaceShips.Lib.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading;

namespace SaceShips.Lib.Classes;

public class StartServerThreadCommand : SaceShips.Lib.Interfaces.ICommand
{
    Thread server_thread;

    public StartServerThreadCommand(Thread thread){
        this.server_thread = thread;
    }

    public void action(){
        this.server_thread.Start();
    }
}
