using SaceShips.Lib.Interfaces;
namespace SaceShips.Lib.Classes;
using System.Collections.Generic;
using Hwdtech;
using System.Linq;
public class GenerableComand : SaceShips.Lib.Interfaces.ICommand
{
    IUObject obj;
    string[] cmd_names;

    public GenerableComand(IUObject obj, string[] cmd_names)
    {
        this.obj = obj;
        this.cmd_names = cmd_names;
    }

    public void action()
    {
        foreach (var item in cmd_names)
        {
            Hwdtech.IoC.Resolve<SaceShips.Lib.Interfaces.ICommand>(item, obj).action();
        }
    }
}
