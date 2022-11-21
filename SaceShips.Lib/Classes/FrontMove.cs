using SaceShips.Lib.Interfaces;
namespace SaceShips.Lib.Classes;


public class FrontMove : ICommand
{
    private IMoveable moveable_obj;

    public FrontMove(IMoveable o)
    {
        this.moveable_obj = o;
    }

    public void action()
    {
        this.moveable_obj.coord += this.moveable_obj.frontSpeed;
    }
}



