namespace SaceShips.Lib.Classes;
using Hwdtech;

public class EndMoveCommand : ICommand
{
    private IMoveCommandEndable obj;

    public EndMoveCommand(IMoveCommandEndable obj)
    {
        this.obj = obj;
    }

    public void Execute()
    {
        obj.properties.ToList().ForEach(p => IoC.Resolve<ICommand>("SpaceBattle.RemoveProperty", obj.uobject, p).Execute());
        IoC.Resolve<IInjectable>("SpaceBattle.Commands.SetupCommand", obj.uobject).Inject(IoC.Resolve<ICommand>("SpaceBattle.Commands.Empty"));
    }

}
