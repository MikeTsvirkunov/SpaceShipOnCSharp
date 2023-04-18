using SaceShips.Lib.Interfaces;
using System.Collections.Generic;
using Hwdtech;
using System.Linq;

namespace SaceShips.Lib.Classes;

public class GameCommandMessagePreprocessingStrategy : SaceShips.Lib.Interfaces.IStartegy
{
    public GameCommandMessagePreprocessingStrategy(){}

    public object execute(object[] args)
    {
        return new Dictionary<string, object?>(){{ "game_id", ((GameCommandMessage)args[0]).game_id },
                                                { "command", ((GameCommandMessage)args[0]).command },
                                                { "args", ((GameCommandMessage)args[0]).args }};
    }
}
