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
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Comands.GenerableComand", (object[] args) => new GenerableComand((IUObject)args[0], (string[])args[1])).Execute();
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
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Comands.Cmd1", (object[] args) => cmd1.Object).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Comands.Cmd2", (object[] args) => cmd2.Object).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Comands.Cmd3", (object[] args) => cmd3.Object).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Comands.Cmd4", (object[] args) => cmd4.Object).Execute();
        var UObject = new Mock<IUObject>();
        var checking_list_of_functions = new string[]{"SpaceShip.Lib.Comands.Cmd1", "SpaceShip.Lib.Comands.Cmd2", "SpaceShip.Lib.Comands.Cmd3", "SpaceShip.Lib.Comands.Cmd4"};
        Hwdtech.IoC.Resolve<SaceShips.Lib.Interfaces.ICommand>("SpaceShip.Lib.Comands.GenerableComand", UObject.Object, checking_list_of_functions).action();
        cmd1.Verify();
        cmd2.Verify();
        cmd3.Verify();
        cmd4.Verify();
    }

    [Fact]
    public void test_generate_command_with_incorrect_command_names()
    {
        Init_Score_Env();
        var UObject = new Mock<IUObject>();
        var checking_list_of_functions = new string[] { "SpaceShip.Lib.Comands.Cmd1", "SpaceShip.Lib.Comands.CmdSmth", "SpaceShip.Lib.Comands.Cmd3", "SpaceShip.Lib.Comands.Cmd4" };
        Assert.Throws<System.ArgumentException>(() => Hwdtech.IoC.Resolve<SaceShips.Lib.Interfaces.ICommand>("SpaceShip.Lib.Comands.GenerableComand", UObject.Object, checking_list_of_functions).action());
    }

    [Fact]
    public void test_generate_command_with_incorrect_object()
    {
        Init_Score_Env();
        var UObject = new Mock<IUObject>();
        var checking_list_of_functions = new string[] { "SpaceShip.Lib.Comands.Cmd1", "SpaceShip.Lib.Comands.CmdSmth", "SpaceShip.Lib.Comands.Cmd3", "SpaceShip.Lib.Comands.Cmd4" };
        Assert.Throws<System.InvalidCastException>(() => Hwdtech.IoC.Resolve<SaceShips.Lib.Interfaces.ICommand>("SpaceShip.Lib.Comands.GenerableComand", 1, checking_list_of_functions).action());
    }
}
