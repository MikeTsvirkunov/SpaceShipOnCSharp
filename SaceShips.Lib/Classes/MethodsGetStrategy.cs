using SaceShips.Lib.Interfaces;
namespace SaceShips.Lib.Classes;
using System.Collections.Generic;
using System.Linq;
using Scriban;
public class MethodsGetStrategy : SaceShips.Lib.Interfaces.IStartegy
{
    System.Type design_class;
    public MethodsGetStrategy(System.Type design_class)
    {
        this.design_class = design_class;
    }
    public object execute(params object[] args)
    {
        return Hwdtech.IoC.Resolve<System.Object>("SpaceShip.Lib.Get.Methods.From.Interface", this.design_class);
    }
}
