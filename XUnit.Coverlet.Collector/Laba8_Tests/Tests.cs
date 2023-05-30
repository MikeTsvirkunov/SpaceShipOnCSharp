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
        int game_time = 10000000;
        var game_timer = new Stopwatch();
        var empty_command = new Mock<SaceShips.Lib.Interfaces.ICommand>();
        empty_command.Setup(p => p.action());
        var test_command = new Mock<SaceShips.Lib.Interfaces.ICommand>();
        test_command.Setup(p => p.action()).Verifiable();
        var test_command_error = new Mock<SaceShips.Lib.Interfaces.ICommand>();
        test_command_error.Setup(p => p.action()).Verifiable();
        var continue_game_command_execution = (object x) =>
        {
            game_timer.Stop();
            if (game_timer.ElapsedTicks < game_time)
            {
                game_timer.Start();
                return x;
            }
            else {
                game_timer.Reset();
                return empty_command.Object;
            }
        };
        var main_scope = Hwdtech.IoC.Resolve<object>("Scopes.New", Hwdtech.IoC.Resolve<object>("Scopes.Root"));
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", main_scope).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.GameExeCommand", (object[] args) => new GameExeCommand(args[0], args[1])).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.GameQueue", (object[] args) => new Queue<SaceShips.Lib.Interfaces.ICommand>()).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.SetScope", (object[] args) => new StandartSetScopeCommand(args[0])).Execute();
        var game_scope = Hwdtech.IoC.Resolve<object>("Scopes.New", main_scope);
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", game_scope).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Game.Queue.GetCommand", (object[] args) => ((Queue<SaceShips.Lib.Interfaces.ICommand>)args[0]).Dequeue()).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Game.ExceptionHandler", (object[] args) => test_command_error.Object).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Game.Run", (object[] args) => {return continue_game_command_execution(args[0]);}).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", main_scope).Execute();
        var sw = new Stopwatch();
        sw.Start();
        var test_queue = Hwdtech.IoC.Resolve<Queue<SaceShips.Lib.Interfaces.ICommand>>("SpaceShip.Lib.GameQueue");
        test_queue.Enqueue(test_command.Object);
        var game_exe_command_test = Hwdtech.IoC.Resolve<SaceShips.Lib.Interfaces.ICommand>("SpaceShip.Lib.GameExeCommand", test_queue, game_scope);
        game_exe_command_test.action();
        test_command.Verify(p => p.action());
    }
    [Fact]
    public void get_error_Game_ExeCommand_while_running_test()
    {
        new Hwdtech.Ioc.InitScopeBasedIoCImplementationCommand().Execute();
        int game_time = 1000000;
        var game_timer = new Stopwatch();
        var empty_command = new Mock<SaceShips.Lib.Interfaces.ICommand>();
        empty_command.Setup(p => p.action());
        var test_command = new Mock<SaceShips.Lib.Interfaces.ICommand>();
        test_command.Setup(p => p.action()).Verifiable();
        var test_command_error = new Mock<SaceShips.Lib.Interfaces.ICommand>();
        test_command_error.Setup(p => p.action()).Verifiable();
        var continue_game_command_execution = (object x) =>
        {
            game_timer.Stop();
            if (game_timer.ElapsedTicks < game_time)
            {
                throw new Exception("TestException");
                game_timer.Start();
                return x;
            }
            else
            {
                game_timer.Reset();
                return empty_command.Object;
            }
        };
        var main_scope = Hwdtech.IoC.Resolve<object>("Scopes.New", Hwdtech.IoC.Resolve<object>("Scopes.Root"));
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", main_scope).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.GameExeCommand", (object[] args) => new GameExeCommand(args[0], args[1])).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.GameQueue", (object[] args) => new Queue<SaceShips.Lib.Interfaces.ICommand>()).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.SetScope", (object[] args) => new StandartSetScopeCommand(args[0])).Execute();
        var game_scope = Hwdtech.IoC.Resolve<object>("Scopes.New", main_scope);
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", game_scope).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Game.Queue.GetCommand", (object[] args) => ((Queue<SaceShips.Lib.Interfaces.ICommand>)args[0]).Dequeue()).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Game.ExceptionHandler", (object[] args) => test_command_error.Object).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Game.Run", (object[] args) => { return continue_game_command_execution(args[0]); }).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", main_scope).Execute();
        var sw = new Stopwatch();
        sw.Start();
        var test_queue = Hwdtech.IoC.Resolve<object>("SpaceShip.Lib.GameQueue");
        var game_exe_command_test = Hwdtech.IoC.Resolve<SaceShips.Lib.Interfaces.ICommand>("SpaceShip.Lib.GameExeCommand", test_queue, game_scope);
        game_exe_command_test.action();
        test_command_error.Verify(p => p.action());
    }

    [Fact]
    public void get_error_Game_ExeCommand_while_running_frome_scope_test()
    {
        new Hwdtech.Ioc.InitScopeBasedIoCImplementationCommand().Execute();
        int game_time = 1000000;
        var game_timer = new Stopwatch();
        var empty_command = new Mock<SaceShips.Lib.Interfaces.ICommand>();
        empty_command.Setup(p => p.action());
        var test_command = new Mock<SaceShips.Lib.Interfaces.ICommand>();
        test_command.Setup(p => p.action()).Verifiable();
        var test_command_error = new Mock<SaceShips.Lib.Interfaces.ICommand>();
        test_command_error.Setup(p => p.action()).Verifiable();
        var continue_game_command_execution = (object x) =>
        {
            game_timer.Stop();
            if (game_timer.ElapsedTicks < game_time)
            {
                game_timer.Start();
                return x;
            }
            else
            {
                game_timer.Reset();
                return empty_command.Object;
            }
        };
        var scope_error_key = (new ArgumentException("Unknown IoC dependency key ${key}. Make sure that ${key} has been registered before try to resolve the dependnecy")).Message;
        var error_container = new Dictionary<Object, SaceShips.Lib.Interfaces.ICommand>() { {scope_error_key, test_command_error.Object } };
        var exception_hendler_with_scope_error = new DefaultExceptionHandler(error_container);
        var main_scope = Hwdtech.IoC.Resolve<object>("Scopes.New", Hwdtech.IoC.Resolve<object>("Scopes.Root"));
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", main_scope).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.GameExeCommand", (object[] args) => new GameExeCommand(args[0], args[1])).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.GameQueue", (object[] args) => new Queue<SaceShips.Lib.Interfaces.ICommand>()).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.SetScope", (object[] args) => new StandartSetScopeCommand(args[0])).Execute();
        var game_scope = Hwdtech.IoC.Resolve<object>("Scopes.New", main_scope);
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", game_scope).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Game.Queue.GetCommand", (object[] args) => ((Queue<SaceShips.Lib.Interfaces.ICommand>)args[0]).Dequeue()).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Game.ExceptionHandler", (object[] args) => exception_hendler_with_scope_error.execute(args[0])).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Get.Result.FromDefaultExceptionHandler", (object[] args) => ((Dictionary<Object, SaceShips.Lib.Interfaces.ICommand>)args[0])[((System.Exception)args[1]).Message]).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", main_scope).Execute();
        var sw = new Stopwatch();
        sw.Start();
        var test_queue = Hwdtech.IoC.Resolve<Queue<SaceShips.Lib.Interfaces.ICommand>>("SpaceShip.Lib.GameQueue");
        test_queue.Enqueue(test_command.Object);
        var game_exe_command_test = Hwdtech.IoC.Resolve<SaceShips.Lib.Interfaces.ICommand>("SpaceShip.Lib.GameExeCommand", test_queue, game_scope);
        game_exe_command_test.action();
        
    }
}
