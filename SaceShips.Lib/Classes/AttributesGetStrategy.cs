using SaceShips.Lib.Interfaces;
namespace SaceShips.Lib.Classes;
using System.Collections.Generic;
using System.Linq;
using Scriban;
public class AttributesGetStrategy : SaceShips.Lib.Interfaces.IStartegy
{
    System.Type design_class;
    public AttributesGetStrategy(System.Type design_class)
    {
        this.design_class = design_class;
    }
    public object execute(params object[] args)
    {
        return Hwdtech.IoC.Resolve<IEnumerable<System.String>>("SpaceShip.Lib.Get.Atributes.From.Interface", this.design_class);
    }
}
