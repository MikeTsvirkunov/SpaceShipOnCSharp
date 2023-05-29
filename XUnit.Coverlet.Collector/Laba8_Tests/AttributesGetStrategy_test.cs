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

public class AttributesGetStrategy_test
{
    [Fact]
    public void standart_get_atributes(){
        new Hwdtech.Ioc.InitScopeBasedIoCImplementationCommand().Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", Hwdtech.IoC.Resolve<object>("Scopes.New", Hwdtech.IoC.Resolve<object>("Scopes.Root"))).Execute();
        // Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Get.TemplateBuilderStrategy", (object[] args) => new ScribanTemplateBuilderStrategy((System.String)args[0])).Execute();
        // Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Get.GenerateCode.Modules.Preprocessing", (object[] args) => args).Execute();
        // Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Get.Dictionary.MembersNameMembersProperty", (object[] args) => args).Execute();
        // Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Get.Methods.From.Interface", (object[] args) => typeof(args[0]).GetMethods(BindingFlags.Instance | BindingFlags.Public).ToList().Where(m => !m.IsSpecialName).Select(i => Regex.Replace(i.ToString(), pattern, String.Empty))).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Get.Atributes.From.Interface", (object[] args) => ((System.Type)args[0]).GetProperties().Where(m => !m.IsSpecialName).Select(i => Regex.Replace(i.ToString(), @"`\d\[([^\[\]]+)\]", "<$1>")).ToList<System.String>()).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.AttributesGetStrategy", (object[] args) => new AttributesGetStrategy((System.Type)args[0])).Execute();
        var attributes_get_strategy_test_object = Hwdtech.IoC.Resolve<AttributesGetStrategy>("SpaceShip.Lib.AttributesGetStrategy", typeof(ISource));
        var expected_results = new List<System.String>(){"System.Collections.Generic.Dictionary<System.Object,System.Object> nexts"};
        Assert.Equal(expected_results, attributes_get_strategy_test_object.execute());
    }

}
