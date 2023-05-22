using CoreWCF;
using System.Collections.Generic;
using Hwdtech;
using SaceShips.Lib.Interfaces;

namespace SaceShips.Lib.Classes;

[ServiceBehavior(IncludeExceptionDetailInFaults = true)]
public class GameCommandMessageGetter : IGameCommandEndPoint
{
    public object get_message(GameCommandMessage param)
    {
        try{
            return Hwdtech.IoC.Resolve<SaceShips.Lib.Interfaces.IStartegy>("SpaceShip.Lib.Strategies.GameCommandMessagePreprocessing").execute(param);
        }
        catch (System.Exception e){
            return Hwdtech.IoC.Resolve<SaceShips.Lib.Interfaces.IStartegy>("SpaceShip.Lib.Strategies.GameCommandMassegeGetterExceptionHandler").execute(e, param);
        }
        
    }
}
