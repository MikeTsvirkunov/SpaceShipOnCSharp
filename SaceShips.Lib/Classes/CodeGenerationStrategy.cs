using SaceShips.Lib.Interfaces;
namespace SaceShips.Lib.Classes;
using System.Collections.Generic;
using System.Linq;
using Scriban;
public class CodeGenerationStrategy : SaceShips.Lib.Interfaces.IStartegy
{
    object template;
    object template_builder;
    public CodeGenerationStrategy(object template, object template_builder)
    {
        this.template = template;
    }
    public object execute(object[] args)
    {
        return Hwdtech.IoC.Resolve<SaceShips.Lib.Interfaces.IStartegy>("SpaceShip.Lib.Get.TemplateBuilderStrategy", this.template).execute(Hwdtech.IoC.Resolve<System.Collections.Generic.List<System.Object>>("SpaceShip.Lib.GenerateCode.Modules.Preprocessing", args));
    }
}
