using SaceShips.Lib.Interfaces;
namespace SaceShips.Lib.Classes;
using System.Collections.Generic;
using System.Linq;
using Scriban;
public class CodeGenerationStrategy : SaceShips.Lib.Interfaces.IStartegy
{
    object template;
    public CodeGenerationStrategy(object template)
    {
        this.template = template;
    }
    public object execute(params object[] args)
    {
        return Hwdtech.IoC.Resolve<SaceShips.Lib.Interfaces.IStartegy>("SpaceShip.Lib.TemplateBuilderStrategy", this.template).execute(Hwdtech.IoC.Resolve<System.Object>("SpaceShip.Lib.GenerateCode.Modules.Preprocessing", args));
    }
}
