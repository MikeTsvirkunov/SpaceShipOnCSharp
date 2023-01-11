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
namespace XUnit.Coverlet.Collector;
public class MacroStartegyTest
{
    [Fact]
    public void Init_Score_Env()
    {
        new Hwdtech.Ioc.InitScopeBasedIoCImplementationCommand().Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", Hwdtech.IoC.Resolve<object>("Scopes.New", Hwdtech.IoC.Resolve<object>("Scopes.Root"))).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Comands.MacroStrategy", (object[] args) => new MacroStartegy((List<SaceShips.Lib.Interfaces.IStartegy>) args[0])).Execute();
        // Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Comands.MacroCommand", (object[] args) => new MacroCommand((IUObject)args[0], (List<SaceShips.Lib.Interfaces.IStartegy>)args[1])).Execute();
    }

    [Fact]
    public void test_generate_strategy_list()
    {
        Init_Score_Env();

        var cmd1 = new Mock<SaceShips.Lib.Interfaces.ICommand>();
        cmd1.Setup(p => p.action());
        var strategy1 = new Mock<IStartegy>();
        strategy1.Setup(p => p.execute(It.IsAny<IUObject>(), It.IsAny<object[]>())).Returns(cmd1.Object);

        var cmd2 = new Mock<SaceShips.Lib.Interfaces.ICommand>();
        cmd2.Setup(p => p.action());
        var strategy2 = new Mock<IStartegy>();
        strategy2.Setup(p => p.execute(It.IsAny<IUObject>(), It.IsAny<object[]>())).Returns(cmd2.Object);

        var cmd3 = new Mock<SaceShips.Lib.Interfaces.ICommand>();
        cmd3.Setup(p => p.action());
        var strategy3 = new Mock<IStartegy>();
        strategy3.Setup(p => p.execute(It.IsAny<IUObject>(), It.IsAny<object[]>())).Returns(cmd3.Object);


        var mass_of_checking_strategies = new List<SaceShips.Lib.Interfaces.IStartegy>(){strategy1.Object, strategy2.Object, strategy3.Object};
        var mass_of_expected_cmd = new List<SaceShips.Lib.Interfaces.ICommand>() { cmd1.Object, cmd2.Object, cmd3.Object };
        var mass_of_args = new List<object[]>(){ It.IsAny<object[]>(), It.IsAny<object[]>(), It.IsAny<object[]>() };
        var x = Hwdtech.IoC.Resolve<SaceShips.Lib.Interfaces.IStartegy>("SpaceShip.Lib.Comands.MacroStrategy", mass_of_checking_strategies).execute(It.IsAny<IUObject>(), mass_of_args);
        Assert.Equal(x, (object)mass_of_expected_cmd);
    }

    [Fact]
    public void test_MacroStrategy_with_empty_mass()
    {
        Init_Score_Env();
        var mass_of_args = new List<object[]>();
        var mass_of_checking_strategies = new List<SaceShips.Lib.Interfaces.IStartegy>();
        var x = Hwdtech.IoC.Resolve<SaceShips.Lib.Interfaces.IStartegy>("SpaceShip.Lib.Comands.MacroStrategy", mass_of_checking_strategies);
        Assert.Equal(x.execute(It.IsAny<IUObject>(), mass_of_args), (object)(new List<object[]>()));
    }

    [Fact]
    public void test_generate_strategy_list_with_different_size_of_strategies_and_argses()
    {
        Init_Score_Env();
        var cmd1 = new Mock<SaceShips.Lib.Interfaces.ICommand>();
        cmd1.Setup(p => p.action());
        var strategy1 = new Mock<IStartegy>();
        strategy1.Setup(p => p.execute(It.IsAny<IUObject>(), It.IsAny<object[]>())).Returns(cmd1.Object);

        var cmd2 = new Mock<SaceShips.Lib.Interfaces.ICommand>();
        cmd2.Setup(p => p.action());
        var strategy2 = new Mock<IStartegy>();
        strategy2.Setup(p => p.execute(It.IsAny<IUObject>(), It.IsAny<object[]>())).Returns(cmd2.Object);

        var cmd3 = new Mock<SaceShips.Lib.Interfaces.ICommand>();
        cmd3.Setup(p => p.action());
        var strategy3 = new Mock<IStartegy>();
        strategy3.Setup(p => p.execute(It.IsAny<IUObject>())).Returns(cmd3.Object);

        var mass_of_checking_strategies = new List<SaceShips.Lib.Interfaces.IStartegy>() { strategy1.Object, strategy2.Object, strategy3.Object };
        var mass_of_expected_cmd = new List<SaceShips.Lib.Interfaces.ICommand>() { cmd1.Object, cmd2.Object};
        var mass_of_args = new List<object[]>() { It.IsAny<object[]>(), It.IsAny<object[]>() };
        var x = Hwdtech.IoC.Resolve<SaceShips.Lib.Interfaces.IStartegy>("SpaceShip.Lib.Comands.MacroStrategy", mass_of_checking_strategies).execute(It.IsAny<IUObject>(), mass_of_args);
        Assert.Equal(x, (object)mass_of_expected_cmd);
    }
}
