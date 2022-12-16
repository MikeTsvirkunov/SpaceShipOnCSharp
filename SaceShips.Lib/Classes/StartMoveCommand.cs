using SaceShips.Lib.Interfaces;
namespace SaceShips.Lib.Classes;


public class StartRotateCommand : ICommand
{
    private IRotateCommandStartable moveable_obj;

    public StartRotateCommand(IRotateable o)
    {
        this.moveable_obj = o;
    }

    public void action()
    {
        
    }
}
