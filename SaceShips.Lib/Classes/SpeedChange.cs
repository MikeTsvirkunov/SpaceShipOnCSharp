using SaceShips.Lib.Interfaces;
namespace SaceShips.Lib.Classes;


public class AngleSpeedChange : ICommand
{
    private IUObject obj;
    private Fraction angleSpeed;

    public AngleSpeedChange(object o, Fraction angleSpeed)
    {
        this.obj = (IUObject)o;
        this.angleSpeed = angleSpeed;
    }

    public void action()
    {
        this.obj.set_property("angleSpeed", angleSpeed);
    }
}
