using SaceShips.Lib.Interfaces;
using System.Collections.Generic;
using Hwdtech;
using System.Linq;

namespace SaceShips.Lib.Classes;

public class GameCommandMessagePreprocessingStrategy : SaceShips.Lib.Interfaces.IStartegy
{
    public GameCommandMessagePreprocessingStrategy(){}
    public object execute(params object[] args)
    {
        return new Dictionary<string, object>(){{ "game_id", Hwdtech.IoC.Resolve<System.Object>("SpaceShip.Lib.Get.GameId.FromGameCommandMessage", args[0])},
                                                { "command", Hwdtech.IoC.Resolve<System.Object>("SpaceShip.Lib.Get.CommandName.FromGameCommandMessage", args[0]) },
                                                { "args", Hwdtech.IoC.Resolve<System.Object>("SpaceShip.Lib.Get.Args.FromGameCommandMessage", args[0]) }};
    }
}
