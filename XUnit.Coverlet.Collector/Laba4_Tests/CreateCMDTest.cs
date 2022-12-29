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
public class CreatCMDTest
{
    [Fact]
    public void Init_Score_Env()
    {
        new Hwdtech.Ioc.InitScopeBasedIoCImplementationCommand().Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", Hwdtech.IoC.Resolve<object>("Scopes.New", Hwdtech.IoC.Resolve<object>("Scopes.Root"))).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Comands.MacroCommand", (object[] args) => new MacroCommand((IUObject)args[0], (List<SaceShips.Lib.Interfaces.IStartegy>)args[1])).Execute();
    }

    [Fact]
    public void test_generate_command()
    {
        Init_Score_Env();
        var UObject = new Mock<IUObject>();

        var cmd1 = new Mock<SaceShips.Lib.Interfaces.ICommand>();
        cmd1.Setup(p => p.action()).Verifiable();
        var strategy1 = new Mock<IStartegy>();
        strategy1.Setup(p => p.execute(It.IsAny<IUObject>())).Returns(cmd1.Object);

        var cmd2 = new Mock<SaceShips.Lib.Interfaces.ICommand>();
        cmd2.Setup(p => p.action()).Verifiable();
        var strategy2 = new Mock<IStartegy>();
        strategy2.Setup(p => p.execute(It.IsAny<IUObject>())).Returns(cmd2.Object);

        var cmd3 = new Mock<SaceShips.Lib.Interfaces.ICommand>();
        cmd3.Setup(p => p.action()).Verifiable();
        var strategy3 = new Mock<IStartegy>();
        strategy3.Setup(p => p.execute(It.IsAny<IUObject>())).Returns(cmd3.Object);


        List<SaceShips.Lib.Interfaces.IStartegy> mass_of_checking_cmds = new List<SaceShips.Lib.Interfaces.IStartegy>(){strategy1.Object, strategy2.Object, strategy3.Object};
        Hwdtech.IoC.Resolve<SaceShips.Lib.Interfaces.ICommand>("SpaceShip.Lib.Comands.MacroCommand", UObject.Object, mass_of_checking_cmds).action();
        cmd1.Verify();
        cmd2.Verify();
        cmd3.Verify();
    }

    [Fact]
    public void test_MacroCommand_with_empty_mass()
    {
        List<SaceShips.Lib.Interfaces.IStartegy> mass_of_checking_cmds = new List<SaceShips.Lib.Interfaces.IStartegy>();
        Hwdtech.IoC.Resolve<SaceShips.Lib.Interfaces.ICommand>("SpaceShip.Lib.Comands.MacroCommand", It.IsAny<IUObject>(), mass_of_checking_cmds).action();
    }

    [Fact]
    public void test_MacroCommand_with_incorrect_list()
    {
        Assert.Throws<System.InvalidCastException>(() => Hwdtech.IoC.Resolve<SaceShips.Lib.Interfaces.ICommand>("SpaceShip.Lib.Comands.MacroCommand", 1).action());
    }
}
