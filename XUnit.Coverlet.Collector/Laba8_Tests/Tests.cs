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
using System.Threading;

namespace XUnit.Coverlet.Collector;

public class GameExeCommandTest
{
    [Fact]
    public void stope_Game_ExeCommand_by_time_test()
    {
        new Hwdtech.Ioc.InitScopeBasedIoCImplementationCommand().Execute();
        var main_scope = Hwdtech.IoC.Resolve<object>("Scopes.New", Hwdtech.IoC.Resolve<object>("Scopes.Root"));
        var game_scope = Hwdtech.IoC.Resolve<object>("Scopes.New", main_scope);
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", main_scope).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.GameExeCommand", (object[] args) => new GameExeCommand(args[0], (IStartegy)args[1], args[2])).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.GameQueue", (object[] args) => new Queue<SaceShips.Lib.Interfaces.ICommand>()).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Get.GameExeCommandStrategy", (object[] args) => new WalkerInGameQueueStrategy((object x) => x)).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.SetScope", (object[] args) => new StandartSetScopeCommand(args[0])).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", game_scope).Execute();
        var test_command_run = new Mock<SaceShips.Lib.Interfaces.ICommand>();
        test_command_run.Setup(p => p.action()).Verifiable();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.GameExeCommand.TimeCounter", (object[] args) => new Stopwatch()).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.GameExeCommand.TimeCounter.Start", (object[] args) => new StartStandartGameExeCommandTimerCommand((Stopwatch)args[0])).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.GameExeCommand.Run", (object[] args) => {test_command_run.Object.action(); return (object)((Boolean)args[0] & (((Stopwatch)args[1]).ElapsedTicks < 0));}).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Take.FromGameQueue", (object[] args) => ((Queue<SaceShips.Lib.Interfaces.ICommand>)args[0]).Peek()).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", main_scope).Execute();
        var test_queue = Hwdtech.IoC.Resolve<object>("SpaceShip.Lib.GameQueue");
        var queue_walker = Hwdtech.IoC.Resolve<SaceShips.Lib.Interfaces.IStartegy>("SpaceShip.Lib.Get.GameExeCommandStrategy");
        var game_exe_command_test = Hwdtech.IoC.Resolve<SaceShips.Lib.Interfaces.ICommand>("SpaceShip.Lib.GameExeCommand", test_queue, queue_walker, game_scope);
        game_exe_command_test.action();
        test_command_run.Verify(p => p.action());
    }

    [Fact]
    public void execute_command_in_Game_ExeCommand_test()
    {
        new Hwdtech.Ioc.InitScopeBasedIoCImplementationCommand().Execute();
        var main_scope = Hwdtech.IoC.Resolve<object>("Scopes.New", Hwdtech.IoC.Resolve<object>("Scopes.Root"));
        var game_scope = Hwdtech.IoC.Resolve<object>("Scopes.New", main_scope);
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", main_scope).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.GameExeCommand", (object[] args) => new GameExeCommand(args[0], (IStartegy)args[1], args[2])).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.GameQueue", (object[] args) => new Queue<SaceShips.Lib.Interfaces.ICommand>()).Execute();
        var empty_command = new Mock<SaceShips.Lib.Interfaces.ICommand>();
        empty_command.Setup(p => p.action());
        var f1 = (object x) => { 
            SaceShips.Lib.Interfaces.ICommand cmd = empty_command.Object; 
            if (((Queue<SaceShips.Lib.Interfaces.ICommand>)x).TryDequeue(out cmd)) return cmd; 
            else return empty_command.Object;
        };
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.GameExeCommandStrategy", (object[] args) => new WalkerInGameQueueStrategy(f1)).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.SetScope", (object[] args) => new StandartSetScopeCommand(args[0])).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", game_scope).Execute();
        var test_command_error = new Mock<SaceShips.Lib.Interfaces.ICommand>();
        test_command_error.Setup(p => p.action()).Verifiable();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.DefaultExceptionHandler", (object[] args) => test_command_error.Object).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.GameExeCommand.TimeCounter", (object[] args) => {var x = new Stopwatch(); x.Reset(); return x;}).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.GameExeCommand.TimeCounter.Start", (object[] args) => new StartStandartGameExeCommandTimerCommand((Stopwatch)args[0])).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.GameExeCommand.Run", (object[] args) => {return (object)(((Stopwatch)args[1]).ElapsedTicks < 1000000);}).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Take.FromGameQueue", (object[] args) => ((Queue<SaceShips.Lib.Interfaces.ICommand>)args[0]).Dequeue()).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Execute.GameExeCommandStrategy", (object[] args) => {return ((IStartegy)args[0]).execute((Queue<SaceShips.Lib.Interfaces.ICommand>)args[1]);}).Execute();
        var test_command = new Mock<SaceShips.Lib.Interfaces.ICommand>();
        test_command.Setup(p => p.action()).Verifiable();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", main_scope).Execute();
        var test_queue = Hwdtech.IoC.Resolve<Queue<SaceShips.Lib.Interfaces.ICommand>>("SpaceShip.Lib.GameQueue");
        test_queue.Enqueue(test_command.Object);
        var queue_walker = Hwdtech.IoC.Resolve<SaceShips.Lib.Interfaces.IStartegy>("SpaceShip.Lib.GameExeCommandStrategy");
        var game_exe_command_test = Hwdtech.IoC.Resolve<SaceShips.Lib.Interfaces.ICommand>("SpaceShip.Lib.GameExeCommand", test_queue, queue_walker, game_scope);
        game_exe_command_test.action();
        test_command.Verify(p => p.action());
    }

    [Fact]
    public void execute_error_strategy_in_Game_ExeCommand_test()
    {
        new Hwdtech.Ioc.InitScopeBasedIoCImplementationCommand().Execute();
        var main_scope = Hwdtech.IoC.Resolve<object>("Scopes.New", Hwdtech.IoC.Resolve<object>("Scopes.Root"));
        var game_scope = Hwdtech.IoC.Resolve<object>("Scopes.New", main_scope);
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", main_scope).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.GameExeCommand", (object[] args) => new GameExeCommand(args[0], (IStartegy)args[1], args[2])).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.GameQueue", (object[] args) => new Queue<SaceShips.Lib.Interfaces.ICommand>()).Execute();
        var empty_command = new Mock<SaceShips.Lib.Interfaces.ICommand>();
        empty_command.Setup(p => p.action());
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.GameExeCommandStrategy", (object[] args) => new WalkerInGameQueueStrategy((object x) => { SaceShips.Lib.Interfaces.ICommand cmd = empty_command.Object; if (((Queue<SaceShips.Lib.Interfaces.ICommand>)x).TryDequeue(out cmd)) return cmd; else return empty_command.Object; })).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.SetScope", (object[] args) => new StandartSetScopeCommand(args[0])).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", game_scope).Execute();
        var test_command_error = new Mock<SaceShips.Lib.Interfaces.ICommand>();
        test_command_error.Setup(p => p.action()).Verifiable();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.DefaultExceptionHandler", (object[] args) => test_command_error.Object).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.GameExeCommand.TimeCounter", (object[] args) => new Stopwatch()).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.GameExeCommand.TimeCounter.Start", (object[] args) => new StartStandartGameExeCommandTimerCommand((Stopwatch)args[0])).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.GameExeCommand.Run", (object[] args) => { return (object)(((Stopwatch)args[1]).ElapsedTicks < 10000000); }).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Take.FromGameQueue", (object[] args) => ((Queue<SaceShips.Lib.Interfaces.ICommand>)args[0]).Dequeue()).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Execute.GameExeCommandStrategy", (object[] args) => { throw new System.Exception("StrategyException"); return ((IStartegy)args[0]).execute((Queue<SaceShips.Lib.Interfaces.ICommand>)args[1]); }).Execute();

        var test_command = new Mock<SaceShips.Lib.Interfaces.ICommand>();
        test_command.Setup(p => p.action()).Callback(() => throw new Exception("Test exception")).Verifiable();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", main_scope).Execute();
        var test_queue = Hwdtech.IoC.Resolve<Queue<SaceShips.Lib.Interfaces.ICommand>>("SpaceShip.Lib.GameQueue");
        test_queue.Enqueue(test_command.Object);
        var queue_walker = Hwdtech.IoC.Resolve<SaceShips.Lib.Interfaces.IStartegy>("SpaceShip.Lib.GameExeCommandStrategy");
        var game_exe_command_test = Hwdtech.IoC.Resolve<SaceShips.Lib.Interfaces.ICommand>("SpaceShip.Lib.GameExeCommand", test_queue, queue_walker, game_scope);
        game_exe_command_test.action();
        test_command_error.Verify(p => p.action());
    }

    [Fact]
    public void execute_stop_cmd_in_Game_ExeCommand_test()
    {
        new Hwdtech.Ioc.InitScopeBasedIoCImplementationCommand().Execute();
        var main_scope = Hwdtech.IoC.Resolve<object>("Scopes.New", Hwdtech.IoC.Resolve<object>("Scopes.Root"));
        var game_scope = Hwdtech.IoC.Resolve<object>("Scopes.New", main_scope);
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", main_scope).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.GameExeCommand", (object[] args) => new GameExeCommand(args[0], (IStartegy)args[1], args[2])).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.GameQueue", (object[] args) => new Queue<SaceShips.Lib.Interfaces.ICommand>()).Execute();
        var empty_command = new Mock<SaceShips.Lib.Interfaces.ICommand>();
        empty_command.Setup(p => p.action());
        var f1 = (object x) =>
        {
            SaceShips.Lib.Interfaces.ICommand cmd = empty_command.Object;
            if (((Queue<SaceShips.Lib.Interfaces.ICommand>)x).TryDequeue(out cmd)) return cmd;
            else return empty_command.Object;
        };
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.GameExeCommandStrategy", (object[] args) => new WalkerInGameQueueStrategy(f1)).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.SetScope", (object[] args) => new StandartSetScopeCommand(args[0])).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", game_scope).Execute();
        var test_command_error = new Mock<SaceShips.Lib.Interfaces.ICommand>();
        test_command_error.Setup(p => p.action()).Verifiable();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.DefaultExceptionHandler", (object[] args) => test_command_error.Object).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.GameExeCommand.TimeCounter", (object[] args) => { var x = new Stopwatch(); x.Reset(); return x; }).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.GameExeCommand.TimeCounter.Start", (object[] args) => new StartStandartGameExeCommandTimerCommand((Stopwatch)args[0])).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.GameExeCommand.Run", (object[] args) => { return (object)(args[0]); }).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Take.FromGameQueue", (object[] args) => ((Queue<SaceShips.Lib.Interfaces.ICommand>)args[0]).Dequeue()).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Execute.GameExeCommandStrategy", (object[] args) => { return ((IStartegy)args[0]).execute((Queue<SaceShips.Lib.Interfaces.ICommand>)args[1]); }).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", main_scope).Execute();
        var test_queue = Hwdtech.IoC.Resolve<Queue<SaceShips.Lib.Interfaces.ICommand>>("SpaceShip.Lib.GameQueue");
        var queue_walker = Hwdtech.IoC.Resolve<SaceShips.Lib.Interfaces.IStartegy>("SpaceShip.Lib.GameExeCommandStrategy");
        var game_exe_command_test = Hwdtech.IoC.Resolve<SaceShips.Lib.Interfaces.ICommand>("SpaceShip.Lib.GameExeCommand", test_queue, queue_walker, game_scope);
        var stop_command = new Mock<SaceShips.Lib.Interfaces.ICommand>();
        stop_command.Setup(p => p.action()).Callback(() => ((IStopable)game_exe_command_test).Stop()).Verifiable();
        test_queue.Enqueue(stop_command.Object);
        game_exe_command_test.action();
        stop_command.Verify(p => p.action());
        empty_command.Verify(p => p.action(), Times.Never);

    }

    [Fact]
    public void execute_changing_strategy_in_Game_ExeCommand_test()
    {
        new Hwdtech.Ioc.InitScopeBasedIoCImplementationCommand().Execute();
        var main_scope = Hwdtech.IoC.Resolve<object>("Scopes.New", Hwdtech.IoC.Resolve<object>("Scopes.Root"));
        var game_scope = Hwdtech.IoC.Resolve<object>("Scopes.New", main_scope);
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", main_scope).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.GameExeCommand", (object[] args) => new GameExeCommand(args[0], (IStartegy)args[1], args[2])).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.GameQueue", (object[] args) => new Queue<SaceShips.Lib.Interfaces.ICommand>()).Execute();
        var empty_command = new Mock<SaceShips.Lib.Interfaces.ICommand>();
        empty_command.Setup(p => p.action());
        var check1 = new Mock<SaceShips.Lib.Interfaces.ICommand>();
        check1.Setup(p => p.action()).Verifiable();
        var check2 = new Mock<SaceShips.Lib.Interfaces.ICommand>();
        check2.Setup(p => p.action()).Verifiable();
        var f1 = (object x) =>
        {
            check1.Object.action();
            SaceShips.Lib.Interfaces.ICommand cmd = empty_command.Object;
            if (((Queue<SaceShips.Lib.Interfaces.ICommand>)x).TryDequeue(out cmd)) return cmd;
            else return empty_command.Object;
        };
        var f2 = (object x) =>
        {
            check2.Object.action();
            SaceShips.Lib.Interfaces.ICommand cmd = empty_command.Object;
            if (((Queue<SaceShips.Lib.Interfaces.ICommand>)x).TryDequeue(out cmd)) return cmd;
            else return empty_command.Object;
        };
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.GameExeCommandStrategy", (object[] args) => new WalkerInGameQueueStrategy((Func<object, object>)args[0])).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.SetScope", (object[] args) => new StandartSetScopeCommand(args[0])).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", game_scope).Execute();
        var test_command_error = new Mock<SaceShips.Lib.Interfaces.ICommand>();
        test_command_error.Setup(p => p.action()).Verifiable();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.DefaultExceptionHandler", (object[] args) => test_command_error.Object).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.GameExeCommand.TimeCounter", (object[] args) => { var x = new Stopwatch(); x.Reset(); return x; }).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.GameExeCommand.TimeCounter.Start", (object[] args) => new StartStandartGameExeCommandTimerCommand((Stopwatch)args[0])).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.GameExeCommand.Run", (object[] args) => { return (object)(args[0]); }).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Take.FromGameQueue", (object[] args) => ((Queue<SaceShips.Lib.Interfaces.ICommand>)args[0]).Dequeue()).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Execute.GameExeCommandStrategy", (object[] args) => { return ((IStartegy)args[0]).execute((Queue<SaceShips.Lib.Interfaces.ICommand>)args[1]); }).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", main_scope).Execute();
        var test_queue = Hwdtech.IoC.Resolve<Queue<SaceShips.Lib.Interfaces.ICommand>>("SpaceShip.Lib.GameQueue");
        var queue_walker1 = Hwdtech.IoC.Resolve<SaceShips.Lib.Interfaces.IStartegy>("SpaceShip.Lib.GameExeCommandStrategy", f1);
        var queue_walker2 = Hwdtech.IoC.Resolve<SaceShips.Lib.Interfaces.IStartegy>("SpaceShip.Lib.GameExeCommandStrategy", f2);
        var game_exe_command_test = Hwdtech.IoC.Resolve<SaceShips.Lib.Interfaces.ICommand>("SpaceShip.Lib.GameExeCommand", test_queue, queue_walker1, game_scope);
        var replace_command = new Mock<SaceShips.Lib.Interfaces.ICommand>();
        replace_command.Setup(p => p.action()).Callback(() => ((IMethodChangeable)game_exe_command_test).ChangeMethod(queue_walker2)).Verifiable();
        test_queue.Enqueue(replace_command.Object);
        var stop_command = new Mock<SaceShips.Lib.Interfaces.ICommand>();
        stop_command.Setup(p => p.action()).Callback(() => ((IStopable)game_exe_command_test).Stop()).Verifiable();
        test_queue.Enqueue(stop_command.Object);
        game_exe_command_test.action();
        stop_command.Verify(p => p.action());
        empty_command.Verify(p => p.action(), Times.Never);
        check1.Verify(p => p.action());
        check2.Verify(p => p.action());
    }
}
