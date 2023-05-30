using SaceShips.Lib.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading;

namespace SaceShips.Lib.Classes;

public class StartServerThreadCommand : SaceShips.Lib.Interfaces.ICommand
{
    ServerThreadStrategy server_thread;

    public StartServerThreadCommand(ServerThreadStrategy thread){
        this.server_thread = thread;
    }

    public void action(){
        ((Thread)this.server_thread.execute()).Start();
    }
}
