using Hwdtech;
using SaceShips.Lib.Interfaces;

namespace SaceShips.Lib.Classes;
public class CollisionCheckCommand : SaceShips.Lib.Interfaces.ICommand
{
    private IUObject obj1, obj2;

    public CollisionCheckCommand(IUObject first, IUObject second)
    {
        this.obj1 = first;
        this.obj2 = second;
    }

    public void action()
    {
        var firstPos = IoC.Resolve<Vector>("SaceShips.GetProperty", "Position", obj1);
        var secondPos = IoC.Resolve<Vector>("SaceShips.GetProperty", "Position", obj2);
        var firstVel = IoC.Resolve<Vector>("SaceShips.GetProperty", "Velocity", obj1);
        var secondVel = IoC.Resolve<Vector>("SaceShips.GetProperty", "Velocity", obj2);

        bool checkCollision = IoC.Resolve<bool>("SaceShips.CheckCollision", firstPos - secondPos, firstVel - secondVel);

        if (checkCollision) throw new Exception();
    }
}
