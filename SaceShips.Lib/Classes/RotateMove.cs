using SaceShips.Lib.Interfaces;
namespace SaceShips.Lib.Classes;


public class RotateMove : ICommand
{
    private IRotateable moveable_obj;

    public RotateMove(IRotateable o)
    {
        this.moveable_obj = o;
    }

    public void action()
    {
        this.moveable_obj.angle += this.moveable_obj.angleSpeed;
    }
}
