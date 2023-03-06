using Xunit;
using SaceShips.Lib.Classes;
using SaceShips.Lib.Interfaces;
using Moq;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System;
using System.Diagnostics;
using Hwdtech;
namespace XUnit.Coverlet.Collector;
using System.Linq;
using System.Threading;

public class ServerThreadStrategyTest
{
    [Fact]
    public void Init_Score_Env_Laba6()
    {
        new Hwdtech.Ioc.InitScopeBasedIoCImplementationCommand().Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", Hwdtech.IoC.Resolve<object>("Scopes.New", Hwdtech.IoC.Resolve<object>("Scopes.Root"))).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Get.ServerThreadStrategy", (object[] args) => new ServerThreadStrategy((SaceShips.Lib.Interfaces.IStartegy)args[0], (BlockingCollection<SaceShips.Lib.Interfaces.ICommand>)args[1])).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Get.BlockingQueueOfICommand", (object[] args) => new BlockingCollection<SaceShips.Lib.Interfaces.ICommand>()).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.StartServerThreadCommand", (object[] args) => new StartServerThreadCommand((ServerThreadStrategy)args[0])).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.HardStopServerThreadCommand", (object[] args) => new HardStopServerThreadCommand((ServerThreadStrategy)args[0])).Execute();
    }

    [Fact]
    public void test_start_thread()
    {
        Init_Score_Env_Laba6();

        var ExeStrategy = new Mock<SaceShips.Lib.Interfaces.IStartegy>();
        var ExeCommand = new Mock<SaceShips.Lib.Interfaces.ICommand>();
        var TestCommand = new Mock<SaceShips.Lib.Interfaces.ICommand>();

        TestCommand.Setup(p => p.action()).Verifiable();

        var x = Hwdtech.IoC.Resolve<BlockingCollection<SaceShips.Lib.Interfaces.ICommand>>("SpaceShip.Lib.Get.BlockingQueueOfICommand");
        ExeCommand.Setup(p => p.action()).Callback(() => { TestCommand.Object.action(); }).Verifiable();
        ExeStrategy.Setup(p => p.execute(TestCommand.Object)).Returns(ExeCommand.Object).Verifiable();
    
        var thread_test = Hwdtech.IoC.Resolve<SaceShips.Lib.Interfaces.IStartegy>("SpaceShip.Lib.Get.ServerThreadStrategy", ExeStrategy.Object, x);

        x.Add(TestCommand.Object);
        x.Add(TestCommand.Object);
        x.Add(TestCommand.Object);
        
        Hwdtech.IoC.Resolve<SaceShips.Lib.Interfaces.ICommand>("SpaceShip.Lib.StartServerThreadCommand", thread_test).action();
        TestCommand.Verify(p => p.action(), Times.Exactly(3));
    }

    [Fact]
    public void test_hard_stop_thread()
    {
        Init_Score_Env_Laba6();

        var ExeStrategy = new Mock<SaceShips.Lib.Interfaces.IStartegy>();
        var ExeCommand = new Mock<SaceShips.Lib.Interfaces.ICommand>();
        var ExeCommand_when_pushed_stop_cmd = new Mock<SaceShips.Lib.Interfaces.ICommand>();
        var TestCommand_that_should_be_activated = new Mock<SaceShips.Lib.Interfaces.ICommand>();
        var TestCommand_that_should_not_be_activated = new Mock<SaceShips.Lib.Interfaces.ICommand>();

        var queue = Hwdtech.IoC.Resolve<BlockingCollection<SaceShips.Lib.Interfaces.ICommand>>("SpaceShip.Lib.Get.BlockingQueueOfICommand");
        var thread_test = Hwdtech.IoC.Resolve<SaceShips.Lib.Interfaces.IStartegy>("SpaceShip.Lib.Get.ServerThreadStrategy", ExeStrategy.Object, queue);
        var stop_cmd = Hwdtech.IoC.Resolve<SaceShips.Lib.Interfaces.ICommand>("SpaceShip.Lib.HardStopServerThreadCommand", thread_test);

        TestCommand_that_should_be_activated.Setup(p => p.action()).Verifiable();
        TestCommand_that_should_not_be_activated.Setup(p => p.action()).Verifiable();

        ExeCommand.Setup(p => p.action()).Callback(() => { TestCommand_that_should_be_activated.Object.action(); }).Verifiable();
        ExeCommand_when_pushed_stop_cmd.Setup(p => p.action()).Callback(() => { stop_cmd.action(); }).Verifiable();

        ExeStrategy.Setup(p => p.execute(TestCommand_that_should_be_activated.Object)).Returns(ExeCommand.Object).Verifiable();
        ExeStrategy.Setup(p => p.execute(stop_cmd)).Returns(ExeCommand_when_pushed_stop_cmd.Object).Verifiable();

        queue.Add(TestCommand_that_should_be_activated.Object);
        queue.Add(TestCommand_that_should_be_activated.Object);
        queue.Add(stop_cmd);
        queue.Add(TestCommand_that_should_not_be_activated.Object);
        queue.Add(TestCommand_that_should_not_be_activated.Object);
        queue.Add(TestCommand_that_should_not_be_activated.Object);

        Hwdtech.IoC.Resolve<SaceShips.Lib.Interfaces.ICommand>("SpaceShip.Lib.StartServerThreadCommand", thread_test).action();
        TestCommand_that_should_be_activated.Verify(p => p.action(), Times.Exactly(2));
        TestCommand_that_should_not_be_activated.Verify(p => p.action(), Times.Never());
    }
}
