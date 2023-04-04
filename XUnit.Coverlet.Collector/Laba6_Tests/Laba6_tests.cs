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
    public void test_hard_stop()
    {
        new Hwdtech.Ioc.InitScopeBasedIoCImplementationCommand().Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", Hwdtech.IoC.Resolve<object>("Scopes.New", Hwdtech.IoC.Resolve<object>("Scopes.Root"))).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Get.ServerThreadStrategy", (object[] args) => new ServerThreadStrategy((SaceShips.Lib.Interfaces.IStartegy)args[0], (BlockingCollection<SaceShips.Lib.Interfaces.ICommand>)args[1])).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Get.BlockingQueueOfICommand", (object[] args) => new BlockingCollection<SaceShips.Lib.Interfaces.ICommand>()).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.StartServerThreadCommand", (object[] args) => new StartServerThreadCommand((ServerThreadStrategy)args[0])).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.HardStopServerThreadCommand", (object[] args) => new HardStopServerThreadCommand((ServerThreadStrategy)args[0])).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.WalkerInQueueStrategy", (object[] args) => new WalkerInQueueStrategy((Func<object, object>)args[0])).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.SoftStopServerThreadCommand", (object[] args) => new SoftStopServerThreadCommand((ServerThreadStrategy)args[0], (BlockingCollection<SaceShips.Lib.Interfaces.ICommand>)args[1])).Execute();

        ManualResetEvent mre = new ManualResetEvent(false);

        var TestCommand = new Mock<SaceShips.Lib.Interfaces.ICommand>();
        var TestCommand_that_should_not_be_action = new Mock<SaceShips.Lib.Interfaces.ICommand>();
        TestCommand.Setup(p => p.action()).Callback(() => mre.Set()).Verifiable();
        TestCommand_that_should_not_be_action.Setup(p => p.action()).Verifiable();

        var queue = Hwdtech.IoC.Resolve<BlockingCollection<SaceShips.Lib.Interfaces.ICommand>>("SpaceShip.Lib.Get.BlockingQueueOfICommand");

        Func<object, object> f = (object x) =>
        {
            return ((BlockingCollection<SaceShips.Lib.Interfaces.ICommand>)x).Take();
        };
        var thread_test = Hwdtech.IoC.Resolve<SaceShips.Lib.Interfaces.IStartegy>("SpaceShip.Lib.Get.ServerThreadStrategy", Hwdtech.IoC.Resolve<SaceShips.Lib.Interfaces.IStartegy>("SpaceShip.Lib.WalkerInQueueStrategy", f), queue);
        var hard_stop_cmd = Hwdtech.IoC.Resolve<SaceShips.Lib.Interfaces.ICommand>("SpaceShip.Lib.HardStopServerThreadCommand", thread_test);

        queue.Add(TestCommand.Object);
        queue.Add(hard_stop_cmd);
        queue.Add(TestCommand_that_should_not_be_action.Object);
        Hwdtech.IoC.Resolve<SaceShips.Lib.Interfaces.ICommand>("SpaceShip.Lib.StartServerThreadCommand", thread_test).action();
        Assert.True(mre.WaitOne(10000));
        TestCommand.Verify(p => p.action(), Times.Once());
        TestCommand_that_should_not_be_action.Verify(p => p.action(), Times.Never());
        Assert.True(1 == queue.Count());
        Assert.Equal(queue.Take(), TestCommand_that_should_not_be_action.Object);
    }

    [Fact]
    public void test_soft_stop()
    {
        new Hwdtech.Ioc.InitScopeBasedIoCImplementationCommand().Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", Hwdtech.IoC.Resolve<object>("Scopes.New", Hwdtech.IoC.Resolve<object>("Scopes.Root"))).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Get.ServerThreadStrategy", (object[] args) => new ServerThreadStrategy((SaceShips.Lib.Interfaces.IStartegy)args[0], (BlockingCollection<SaceShips.Lib.Interfaces.ICommand>)args[1])).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Get.BlockingQueueOfICommand", (object[] args) => new BlockingCollection<SaceShips.Lib.Interfaces.ICommand>()).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.StartServerThreadCommand", (object[] args) => new StartServerThreadCommand((ServerThreadStrategy)args[0])).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.HardStopServerThreadCommand", (object[] args) => new HardStopServerThreadCommand((ServerThreadStrategy)args[0])).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.WalkerInQueueStrategy", (object[] args) => new WalkerInQueueStrategy((Func<object, object>)args[0])).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.SoftStopServerThreadCommand", (object[] args) => new SoftStopServerThreadCommand((ServerThreadStrategy)args[0], (BlockingCollection<SaceShips.Lib.Interfaces.ICommand>)args[1])).Execute();


        ManualResetEvent mre = new ManualResetEvent(false);
        ManualResetEvent mre1 = new ManualResetEvent(false);

        var InitScopeInThreadCommand = new Mock<SaceShips.Lib.Interfaces.ICommand>();

        InitScopeInThreadCommand.Setup(p => p.action()).Callback(() =>
        {
            new Hwdtech.Ioc.InitScopeBasedIoCImplementationCommand().Execute();
            Hwdtech.IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", Hwdtech.IoC.Resolve<object>("Scopes.New", Hwdtech.IoC.Resolve<object>("Scopes.Root"))).Execute();
            Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.HardStopServerThreadCommand", (object[] args) => new HardStopServerThreadCommand((ServerThreadStrategy)args[0])).Execute();
            Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.SoftStopServerThreadCommand", (object[] args) => new SoftStopServerThreadCommand((ServerThreadStrategy)args[0], (BlockingCollection<SaceShips.Lib.Interfaces.ICommand>)args[1])).Execute();
        });

        var TestCommand = new Mock<SaceShips.Lib.Interfaces.ICommand>();
        var TestCommand_after_soft = new Mock<SaceShips.Lib.Interfaces.ICommand>();
        var TestCommand_that_should_be_action2 = new Mock<SaceShips.Lib.Interfaces.ICommand>();

        var queue = Hwdtech.IoC.Resolve<BlockingCollection<SaceShips.Lib.Interfaces.ICommand>>("SpaceShip.Lib.Get.BlockingQueueOfICommand");

        Func<object, object> f = (object x) =>
        {
            return ((BlockingCollection<SaceShips.Lib.Interfaces.ICommand>)x).Take();
        };
        var thread_test = Hwdtech.IoC.Resolve<SaceShips.Lib.Interfaces.IStartegy>("SpaceShip.Lib.Get.ServerThreadStrategy", Hwdtech.IoC.Resolve<SaceShips.Lib.Interfaces.IStartegy>("SpaceShip.Lib.WalkerInQueueStrategy", f), queue);
        var soft_stop_cmd = Hwdtech.IoC.Resolve<SaceShips.Lib.Interfaces.ICommand>("SpaceShip.Lib.SoftStopServerThreadCommand", thread_test, queue);

        TestCommand.Setup(p => p.action()).Callback(() => mre.Set()).Verifiable();
        TestCommand_that_should_be_action2.Setup(p => p.action()).Verifiable();
        TestCommand_after_soft.Setup(p => p.action()).Callback(() => {queue.Add(TestCommand_that_should_be_action2.Object); mre1.Set();}).Verifiable();

        queue.Add(InitScopeInThreadCommand.Object);
        queue.Add(TestCommand.Object);
        queue.Add(soft_stop_cmd);
        queue.Add(TestCommand_after_soft.Object);
        queue.Add(soft_stop_cmd);
        Hwdtech.IoC.Resolve<SaceShips.Lib.Interfaces.ICommand>("SpaceShip.Lib.StartServerThreadCommand", thread_test).action();
        Assert.True(mre.WaitOne(10000));
        Assert.True(mre1.WaitOne(10000));
        TestCommand.Verify(p => p.action(), Times.Once());
        TestCommand_after_soft.Verify(p => p.action(), Times.Once());
        TestCommand_that_should_be_action2.Verify(p => p.action(), Times.Once());
        Assert.True(0 == queue.Count());
    }


    [Fact]
    public void test_replace_command()
    {
        new Hwdtech.Ioc.InitScopeBasedIoCImplementationCommand().Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", Hwdtech.IoC.Resolve<object>("Scopes.New", Hwdtech.IoC.Resolve<object>("Scopes.Root"))).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Get.ServerThreadStrategy", (object[] args) => new ServerThreadStrategy((SaceShips.Lib.Interfaces.IStartegy)args[0], (BlockingCollection<SaceShips.Lib.Interfaces.ICommand>)args[1])).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Get.BlockingQueueOfICommand", (object[] args) => new BlockingCollection<SaceShips.Lib.Interfaces.ICommand>()).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.WalkerInQueueStrategy", (object[] args) => new WalkerInQueueStrategy((Func<object, object>)args[0])).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.StartServerThreadCommand", (object[] args) => new StartServerThreadCommand((ServerThreadStrategy)args[0])).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.SoftStopServerThreadCommand", (object[] args) => new SoftStopServerThreadCommand((ServerThreadStrategy)args[0], (BlockingCollection<SaceShips.Lib.Interfaces.ICommand>)args[1])).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.WalkingStrategyInThreadReplaceCommand", (object[] args) => new WalkingStrategyInThreadReplaceCommand((IMethodChangeable)args[0], (IStartegy)args[1])).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.HardStopServerThreadCommand", (object[] args) => new HardStopServerThreadCommand((ServerThreadStrategy)args[0])).Execute();

        var check1 = new Mock<SaceShips.Lib.Interfaces.ICommand>();
        var check2 = new Mock<SaceShips.Lib.Interfaces.ICommand>();
        check1.Setup(p => p.action()).Verifiable();
        check2.Setup(p => p.action()).Verifiable();

        Func<object, object> f1 = (object x) =>
        {
            check1.Object.action();
            return ((BlockingCollection<SaceShips.Lib.Interfaces.ICommand>)x).Take();
        };

        Func<object, object> f2 = (object x) =>
        {
            check2.Object.action();
            return ((BlockingCollection<SaceShips.Lib.Interfaces.ICommand>)x).Take();
        };

        ManualResetEvent mre = new ManualResetEvent(false);

        var TestCommand1 = new Mock<SaceShips.Lib.Interfaces.ICommand>();
        var TestCommand2 = new Mock<SaceShips.Lib.Interfaces.ICommand>();

        var queue = Hwdtech.IoC.Resolve<BlockingCollection<SaceShips.Lib.Interfaces.ICommand>>("SpaceShip.Lib.Get.BlockingQueueOfICommand");
        var exe1 = Hwdtech.IoC.Resolve<SaceShips.Lib.Interfaces.IStartegy>("SpaceShip.Lib.WalkerInQueueStrategy", f1);
        var exe2 = Hwdtech.IoC.Resolve<SaceShips.Lib.Interfaces.IStartegy>("SpaceShip.Lib.WalkerInQueueStrategy", f2);
        var thread_test = Hwdtech.IoC.Resolve<SaceShips.Lib.Interfaces.IStartegy>("SpaceShip.Lib.Get.ServerThreadStrategy", exe1, queue);
        var replacer = Hwdtech.IoC.Resolve<SaceShips.Lib.Interfaces.ICommand>("SpaceShip.Lib.WalkingStrategyInThreadReplaceCommand", thread_test, exe2);
        var hard_stop_cmd = Hwdtech.IoC.Resolve<SaceShips.Lib.Interfaces.ICommand>("SpaceShip.Lib.HardStopServerThreadCommand", thread_test);

        TestCommand1.Setup(p => p.action()).Verifiable();
        TestCommand2.Setup(p => p.action()).Callback(() => mre.Set()).Verifiable();

        queue.Add(TestCommand1.Object);
        queue.Add(replacer);
        queue.Add(TestCommand2.Object);
        queue.Add(hard_stop_cmd);

        Hwdtech.IoC.Resolve<SaceShips.Lib.Interfaces.ICommand>("SpaceShip.Lib.StartServerThreadCommand", thread_test).action();

        Assert.True(mre.WaitOne(10000));
        TestCommand1.Verify(p => p.action(), Times.Once());
        TestCommand2.Verify(p => p.action(), Times.Once());
        check1.Verify(p => p.action(), Times.Exactly(2));
        check2.Verify(p => p.action(), Times.Exactly(2));
        Assert.True(0 == queue.Count());
    }
}
