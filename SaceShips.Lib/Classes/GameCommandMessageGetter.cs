using CoreWCF;
using System.Collections.Generic;
using Hwdtech;
using SaceShips.Lib.Interfaces;

namespace SaceShips.Lib.Classes;

[ServiceBehavior(IncludeExceptionDetailInFaults = true)]
public class GameCommandMessageGetter : IWebApi
{
    public object get_message(IMessage param)
    {
        return Hwdtech.IoC.Resolve<SaceShips.Lib.Interfaces.IStartegy>("SpaceShip.Lib.Strategies.GameCommandMessagePreprocessing").execute(param);
    }
}
