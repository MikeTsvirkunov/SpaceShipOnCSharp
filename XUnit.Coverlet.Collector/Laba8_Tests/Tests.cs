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
            else return empty_command.Object;
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
        // Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Game.ExceptionHandler", (object[] args) => new Stopwatch()).Execute();
        var sw = new Stopwatch();
        sw.Start();

        var test_queue = Hwdtech.IoC.Resolve<object>("SpaceShip.Lib.GameQueue");
        var game_exe_command_test = Hwdtech.IoC.Resolve<SaceShips.Lib.Interfaces.ICommand>("SpaceShip.Lib.GameExeCommand", test_queue, game_scope);
        game_exe_command_test.action();
        // sw.Stop();
        // sw.Reset();
        // Assert.Equal(sw.ElapsedTicks, 0);

        // var test_command_run = new Mock<SaceShips.Lib.Interfaces.ICommand>();
        // test_command_run.Setup(p => p.action()).Verifiable();
        // Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.GameExeCommand.TimeCounter", (object[] args) => new Stopwatch()).Execute();
        // Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.GameExeCommand.TimeCounter.Start", (object[] args) => new StartStandartGameExeCommandTimerCommand((Stopwatch)args[0])).Execute();
        // Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.GameExeCommand.Run", (object[] args) => {test_command_run.Object.action(); return (object)((Boolean)args[0] & (((Stopwatch)args[1]).ElapsedTicks < 0));}).Execute();
        // Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Take.FromGameQueue", (object[] args) => ((Queue<SaceShips.Lib.Interfaces.ICommand>)args[0]).Peek()).Execute();
        // Hwdtech.IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", main_scope).Execute();
        // var test_queue = Hwdtech.IoC.Resolve<object>("SpaceShip.Lib.GameQueue");
        // var queue_walker = Hwdtech.IoC.Resolve<SaceShips.Lib.Interfaces.IStartegy>("SpaceShip.Lib.Get.GameExeCommandStrategy");
        // var game_exe_command_test = Hwdtech.IoC.Resolve<SaceShips.Lib.Interfaces.ICommand>("SpaceShip.Lib.GameExeCommand", test_queue, queue_walker, game_scope);
        // game_exe_command_test.action();
        // test_command_run.Verify(p => p.action());
    }
}
