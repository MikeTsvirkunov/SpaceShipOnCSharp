using SaceShips.Lib.Interfaces;
using Hwdtech;

namespace SaceShips.Lib.Classes;

public class StandartSetScopeCommand: SaceShips.Lib.Interfaces.ICommand
{
    object scope;
    public StandartSetScopeCommand(object scope)
    {
        this.scope = scope;
    }

    public void action()
    {
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", this.scope).Execute();
    }
}
