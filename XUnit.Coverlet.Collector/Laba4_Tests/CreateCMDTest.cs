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
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Comands.MacroCommand", (object[] args) => new MacroCommand((List<SaceShips.Lib.Interfaces.ICommand>)args[0])).Execute();
    }

    [Fact]
    public void test_generate_command()
    {
        Init_Score_Env();
        var cmd1 = new Mock<SaceShips.Lib.Interfaces.ICommand>();
        cmd1.Setup(p => p.action());
        var cmd2 = new Mock<SaceShips.Lib.Interfaces.ICommand>();
        cmd2.Setup(p => p.action());
        var cmd3 = new Mock<SaceShips.Lib.Interfaces.ICommand>();
        cmd3.Setup(p => p.action());
        var cmd4 = new Mock<SaceShips.Lib.Interfaces.ICommand>();
        cmd4.Setup(p => p.action()).Verifiable();
        var UObject = new Mock<IUObject>();
        List<SaceShips.Lib.Interfaces.ICommand> mass_of_checking_cmds = new List<SaceShips.Lib.Interfaces.ICommand>(){cmd1.Object, cmd2.Object, cmd3.Object, cmd4.Object};
        Hwdtech.IoC.Resolve<SaceShips.Lib.Interfaces.ICommand>("SpaceShip.Lib.Comands.MacroCommand", mass_of_checking_cmds).action();
        cmd1.Verify();
        cmd2.Verify();
        cmd3.Verify();
        cmd4.Verify();
    }

    [Fact]
    public void test_MacroCommand_with_empty_mass()
    {
        List<SaceShips.Lib.Interfaces.ICommand> mass_of_checking_cmds = new List<SaceShips.Lib.Interfaces.ICommand>();
        Hwdtech.IoC.Resolve<SaceShips.Lib.Interfaces.ICommand>("SpaceShip.Lib.Comands.MacroCommand", mass_of_checking_cmds).action();
    }

    [Fact]
    public void test_MacroCommand_with_incorrect_list()
    {
        Assert.Throws<System.InvalidCastException>(() => Hwdtech.IoC.Resolve<SaceShips.Lib.Interfaces.ICommand>("SpaceShip.Lib.Comands.MacroCommand", 1).action());
    }
}
