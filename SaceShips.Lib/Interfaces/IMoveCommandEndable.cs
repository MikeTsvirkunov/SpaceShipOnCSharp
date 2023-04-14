namespace SaceShips.Lib.Interfaces;

public interface IMoveCommandEndable : ICommand
{
    IEnumerable<object> properties { set; get; }
    ICommand move_command { get; }
    IUObject uobj { get; }
}
