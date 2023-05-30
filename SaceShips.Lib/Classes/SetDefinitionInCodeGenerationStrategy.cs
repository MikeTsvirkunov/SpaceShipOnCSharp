using SaceShips.Lib.Interfaces;
namespace SaceShips.Lib.Classes;
using System.Collections.Generic;
using System.Linq;
using Scriban;
public class SetDefinitionInCodeGenerationStrategy : SaceShips.Lib.Interfaces.IStartegy
{
    object definition;
    public SetDefinitionInCodeGenerationStrategy(object definition)
    {
        this.definition = definition;
    }
    public object execute(params object[] args)
    {
        return Hwdtech.IoC.Resolve<System.Object>("SpaceShip.Lib.Add.Definition.To.Modules", this.definition, args[0]);
    }
}
