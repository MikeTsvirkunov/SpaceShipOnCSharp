using SaceShips.Lib.Interfaces;
using System.Diagnostics;

namespace SaceShips.Lib.Classes;

public class StartStandartGameExeCommandTimerCommand
{
    Stopwatch timer;
    public StartStandartGameExeCommandTimerCommand(Stopwatch timer){
        this.timer = timer;
    }

    public void action(){
        this.timer.Start();
    }
}