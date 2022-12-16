using SaceShips.Lib.Interfaces;
namespace SaceShips.Lib.Classes;


public class StartRotateCommand : ICommand
{
    ISpeedChangeable obj;

    public StartRotateCommand(ISpeedChangeable obj)
    {
        this.obj = obj;
    }

    public void action()
    {
        // this.rotateable_obj.angle += this.rotateable_obj.angleSpeed;
    }
}
