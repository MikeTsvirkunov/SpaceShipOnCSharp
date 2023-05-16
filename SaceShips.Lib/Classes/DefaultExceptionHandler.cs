using System.Collections.Generic;
using Hwdtech;
using SaceShips.Lib.Interfaces;

namespace SaceShips.Lib.Classes;

public class DefaultExceptionHandler: SaceShips.Lib.Interfaces.IStartegy
{
    object exception_container;
    public DefaultExceptionHandler(object exception_container){
        this.exception_container = exception_container;
    }

    public object execute(params object[] args){
        return Hwdtech.IoC.Resolve<System.Object>("SpaceShip.Lib.Get.Result.FromDefaultExceptionHandler", this.exception_container, args[0]);
    }
}
