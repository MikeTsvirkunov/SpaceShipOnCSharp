using SaceShips.Lib.Interfaces;
namespace SaceShips.Lib.Classes;
public class MoveCommand : ICommand
{
    private readonly IMovable ObjMove;
    public MoveCommand(IMovable movable)
    {
        ObjMove = movable;
    }
    public void action()
    {
        ObjMove.position += ObjMove.velocity;
    }
}
