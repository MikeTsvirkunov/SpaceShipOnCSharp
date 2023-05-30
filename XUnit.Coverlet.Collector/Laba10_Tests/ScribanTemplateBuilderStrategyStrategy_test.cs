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

public class ScribanTemplateBuilderStrategyStrategy_test
{
    [Fact]
    public void standart_get_code_generation(){
        new Hwdtech.Ioc.InitScopeBasedIoCImplementationCommand().Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", Hwdtech.IoC.Resolve<object>("Scopes.New", Hwdtech.IoC.Resolve<object>("Scopes.Root"))).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Get.Atributes.From.Interface", (object[] args) => ((System.Type)args[0]).GetProperties().Where(m => !m.IsSpecialName).Select(i => Regex.Replace(i.ToString(), @"`\d\[([^\[\]]+)\]", "<$1>")).ToList<System.String>()).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.AttributesGetStrategy", (object[] args) => new AttributesGetStrategy((System.Type)args[0])).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Get.Methods.From.Interface", (object[] args) => ((System.Type)args[0]).GetMethods().Where(m => !m.IsSpecialName).Select(i => Regex.Replace(Regex.Replace(i.ToString(), @"`\d\[([^\[\]]+)\]", "<$1>"), "\\(.*\\)", String.Empty)).ToList<System.String>()).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.MethodsGetStrategy", (object[] args) => new MethodsGetStrategy((System.Type)args[0])).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Get.TypeOf.ModulesMass", (object[] args) => args[0]).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Get.AttributesAndMethods", (object[] args) => args[0]).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.ScribanTemplateBuilderStrategy", (object[] args) => new ScribanTemplateBuilderStrategy(args[0].ToString())).Execute();
        // Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Get.TemplateBuilderStrategy", (object[] args) => new ScribanTemplateBuilderStrategy((System.String)args[0])).Execute();
        // Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Get.GenerateCode.Modules.Preprocessing", (object[] args) => args).Execute();
        // Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Get.Dictionary.MembersNameMembersProperty", (object[] args) => args).Execute();
        // Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Get.Methods.From.Interface", (object[] args) => typeof(args[0]).GetMethods(BindingFlags.Instance | BindingFlags.Public).ToList().Where(m => !m.IsSpecialName).Select(i => Regex.Replace(i.ToString(), pattern, String.Empty))).Execute();
        string test_template = @"
public class TestClassName{
{{ for attributes_name in attributes  }}
public {{attributes_name}};
{{ end }}
{{ for method_name in methods  }}
public {{method_name}};
{{ end }}
}
";
        string expected_results = @"
public class TestClassName{

public System.Object step_forward;


public System.Collections.Generic.Dictionary<System.Object,System.Object> nexts;

}
";
        var methods_get_strategy_test_object = Hwdtech.IoC.Resolve<MethodsGetStrategy>("SpaceShip.Lib.MethodsGetStrategy", typeof(ISource));
        var attributes_get_strategy_test_object = Hwdtech.IoC.Resolve<AttributesGetStrategy>("SpaceShip.Lib.AttributesGetStrategy", typeof(ISource));
        var test_dictionary_of_attributes_and_methods = new Dictionary<String, IEnumerable<String>>(){{"attributes", (List<String>)methods_get_strategy_test_object.execute()}, { "methods", (List<String>)attributes_get_strategy_test_object.execute() } };
        var scriban_builder = Hwdtech.IoC.Resolve<ScribanTemplateBuilderStrategy>("SpaceShip.Lib.ScribanTemplateBuilderStrategy", test_template);
        
        Assert.Equal(expected_results, scriban_builder.execute(test_dictionary_of_attributes_and_methods));

    }

}
