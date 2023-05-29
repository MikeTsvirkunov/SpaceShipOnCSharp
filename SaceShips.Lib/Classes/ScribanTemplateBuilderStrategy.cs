using SaceShips.Lib.Interfaces;
namespace SaceShips.Lib.Classes;
using System.Collections.Generic;
using System.Linq;
using Scriban;
public class ScribanTemplateBuilderStrategy : SaceShips.Lib.Interfaces.IStartegy
{
    Scriban.Template template_compiled;
    public ScribanTemplateBuilderStrategy(string template)
    {
        this.template_compiled = Template.Parse(template);
    }
    public object execute(params object[] args)
    {
        //Hwdtech.IoC.Resolve<System.Type>("SpaceShip.Lib.Get.TypeOf.ModulesMass", args[0])
        return this.template_compiled.Render(Hwdtech.IoC.Resolve<System.Object>("SpaceShip.Lib.Get.AttributesAndMethods", args[0]));
    }
}
