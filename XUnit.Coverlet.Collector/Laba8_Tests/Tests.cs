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
using System.Diagnostics;

namespace XUnit.Coverlet.Collector;

public class ScopeTest
{
    [Fact]
    public void T81()
    {
        new Hwdtech.Ioc.InitScopeBasedIoCImplementationCommand().Execute();
        var x = Hwdtech.IoC.Resolve<object>("Scopes.New", Hwdtech.IoC.Resolve<object>("Scopes.Root"));
        var y = Hwdtech.IoC.Resolve<object>("Scopes.New", Hwdtech.IoC.Resolve<object>("Scopes.Root"));
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", x).Execute();
        var to = new object();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "object", (object[] args) => to).Execute();
        Assert.Equal(x.GetHashCode(), Hwdtech.IoC.Resolve<object>("Scopes.Current").GetHashCode());
        Assert.Equal(Hwdtech.IoC.Resolve<object>("object"), to);
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", y).Execute();
        Assert.Throws<System.ArgumentException>(() => Assert.Equal(Hwdtech.IoC.Resolve<object>("object"), to));
    }

    [Fact]
    public void T82()
    {
        new Hwdtech.Ioc.InitScopeBasedIoCImplementationCommand().Execute();
        var main_scope = Hwdtech.IoC.Resolve<object>("Scopes.New", Hwdtech.IoC.Resolve<object>("Scopes.Root"));
        var game_scope = Hwdtech.IoC.Resolve<object>("Scopes.New", main_scope);
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", main_scope).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.GameExeCommand", (object[] args) => new GameExeCommand(args[0], args[1], args[2])).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.GameQueue", (object[] args) => new Queue<SaceShips.Lib.Interfaces.ICommand>()).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Get.GameExeCommandStrategy", (object[] args) => new WalkerInGameQueueStrategy((object x) => x)).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", game_scope).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.SetScope", (object[] args) => new StandartSetScopeCommand(args[0])).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.GameExeCommand.TimeCounter", (object[] args) => new Stopwatch()).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.GameExeCommand.TimeCounter.Start", (object[] args) => new StartStandartGameExeCommandTimerCommand((Stopwatch)args[0])).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.GameExeCommand.Run", (object[] args) => (object)((Boolean)args[0] & (((Stopwatch)args[1]).ElapsedTicks < 1000))).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Take.FromGameQueue", (object[] args) => ((Queue<SaceShips.Lib.Interfaces.ICommand>)args[0]).Peek()).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", main_scope).Execute();
        var test_queue = Hwdtech.IoC.Resolve<object>("SpaceShip.Lib.GameQueue");
        var queue_walker = Hwdtech.IoC.Resolve<SaceShips.Lib.Interfaces.ICommand>("SpaceShip.Lib.GameQueue", t);
        var game_exe_command_test = Hwdtech.IoC.Resolve<SaceShips.Lib.Interfaces.ICommand>("SpaceShip.Lib.GameExeCommandStrategy", test_queue, queue_walker).action();
        // Assert.Equal(x.GetHashCode(), Hwdtech.IoC.Resolve<object>("Scopes.Current").GetHashCode());
        // Assert.Equal(Hwdtech.IoC.Resolve<object>("object"), to);
        // Hwdtech.IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", y).Execute();
        // Assert.Throws<System.ArgumentException>(() => Assert.Equal(Hwdtech.IoC.Resolve<object>("object"), to));
    }
}
