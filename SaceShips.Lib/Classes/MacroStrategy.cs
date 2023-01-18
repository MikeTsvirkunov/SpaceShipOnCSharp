using SaceShips.Lib.Interfaces;
namespace SaceShips.Lib.Classes;
using System.Collections.Generic;
using Hwdtech;
using System.Linq;
public class MacroStartegy : SaceShips.Lib.Interfaces.IStartegy
{
    List<SaceShips.Lib.Interfaces.IStartegy> strategies;

    public MacroStartegy(List<SaceShips.Lib.Interfaces.IStartegy> strategies)
    {
        this.strategies = strategies;
    }

    public object execute(object[] args)
    {
        var cmds = Hwdtech.IoC.Resolve<List<SaceShips.Lib.Interfaces.ICommand>>("SpaceShip.Lib.Get.EmptyICommandList");
        var x = Hwdtech.IoC.Resolve<List<KeyValuePair<SaceShips.Lib.Interfaces.IStartegy, object[]>>>("SpaceShip.Lib.Get.KeyPairStrategyParamsList", strategies.Zip((List<object[]>)args[1], (a, b) => {return new KeyValuePair<SaceShips.Lib.Interfaces.IStartegy, object[]>(a, b);}));
        x.ForEach(c => cmds.Add((SaceShips.Lib.Interfaces.ICommand)c.Key.execute((IUObject) args[0], c.Value)));
        return (object)cmds;
    }
}
