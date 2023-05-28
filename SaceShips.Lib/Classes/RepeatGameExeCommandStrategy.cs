using SaceShips.Lib.Interfaces;
using System.Collections.Generic;
using Hwdtech;

namespace SaceShips.Lib.Classes;

public class RepeatGameExeCommandStrategy : SaceShips.Lib.Interfaces.ICommand
{
    object game_exe_command;
    public RepeatGameExeCommandStrategy(object game_exe_command)
    {
        this.game_exe_command = game_exe_command;
    }
    public void action()
    {
    }
}
