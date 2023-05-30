using Xunit;
using SaceShips.Lib.Classes;
using SaceShips.Lib.Interfaces;
using Moq;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System;
using System.Diagnostics;
using Hwdtech;
using System.Linq;
using Scriban;
namespace XUnit.Coverlet.Collector;
using System.Linq;
using System.Text.RegularExpressions;

public class SetDefinitionInCodeGenerationStrategy_test
{
    [Fact]
    public void standart_get_code_generation()
    {
        new Hwdtech.Ioc.InitScopeBasedIoCImplementationCommand().Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", Hwdtech.IoC.Resolve<object>("Scopes.New", Hwdtech.IoC.Resolve<object>("Scopes.Root"))).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Get.Atributes.From.Interface", (object[] args) => ((System.Type)args[0]).GetProperties().Where(m => !m.IsSpecialName).Select(i => Regex.Replace(i.ToString(), @"`\d\[([^\[\]]+)\]", "<$1>")).ToList<System.String>()).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.AttributesGetStrategy", (object[] args) => new AttributesGetStrategy((System.Type)args[0])).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Get.Methods.From.Interface", (object[] args) => ((System.Type)args[0]).GetMethods().Where(m => !m.IsSpecialName).Select(i => Regex.Replace(Regex.Replace(i.ToString(), @"`\d\[([^\[\]]+)\]", "<$1>"), "\\(.*\\)", String.Empty)).ToList<System.String>()).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.MethodsGetStrategy", (object[] args) => new MethodsGetStrategy((System.Type)args[0])).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Get.TypeOf.ModulesMass", (object[] args) => args[0]).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Get.AttributesAndMethods", (object[] args) => args[0]).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.SetDefinitionInCodeGenerationStrategy", (object[] args) => new SetDefinitionInCodeGenerationStrategy(args[0])).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Add.Definition.To.Modules", (object[] args) => ((List<String>)args[0]).Zip(((List<String>)args[1]), (a, b) => a + b).ToList()).Execute();
        
        var methods_get_strategy_test_object = Hwdtech.IoC.Resolve<MethodsGetStrategy>("SpaceShip.Lib.MethodsGetStrategy", typeof(ISource));
        var definition_add_strategy_test_object = Hwdtech.IoC.Resolve<SetDefinitionInCodeGenerationStrategy>("SpaceShip.Lib.SetDefinitionInCodeGenerationStrategy", methods_get_strategy_test_object.execute());
        var expected_results = new List<String>() { "System.Object step_forward() => {return 2 + 2}" };
        definition_add_strategy_test_object.execute((List<String>)methods_get_strategy_test_object.execute());
        Assert.Equal(expected_results, definition_add_strategy_test_object.execute(new List<String>() { "() => {return 2 + 2}" }));  
    }
}
