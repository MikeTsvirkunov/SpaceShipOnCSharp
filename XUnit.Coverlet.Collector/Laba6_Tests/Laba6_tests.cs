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
            SaceShips.Lib.Interfaces.ICommand z;
            ((BlockingCollection<SaceShips.Lib.Interfaces.ICommand>)x).TryTake(out z);
            return z;
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
        Assert.Equal(queue.Count(), 1);
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
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.EmptyCommand", (object[] args) => new EmptyCommand()).Execute();


        ManualResetEvent mre = new ManualResetEvent(false);
        ManualResetEvent mre1 = new ManualResetEvent(false);

        var InitScopeInThreadCommand = new Mock<SaceShips.Lib.Interfaces.ICommand>();

        InitScopeInThreadCommand.Setup(p => p.action()).Callback(() =>
        {
            new Hwdtech.Ioc.InitScopeBasedIoCImplementationCommand().Execute();
            Hwdtech.IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", Hwdtech.IoC.Resolve<object>("Scopes.New", Hwdtech.IoC.Resolve<object>("Scopes.Root"))).Execute();
            Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.HardStopServerThreadCommand", (object[] args) => new HardStopServerThreadCommand((ServerThreadStrategy)args[0])).Execute();
            Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.SoftStopServerThreadCommand", (object[] args) => new SoftStopServerThreadCommand((ServerThreadStrategy)args[0], (BlockingCollection<SaceShips.Lib.Interfaces.ICommand>)args[1])).Execute();
            Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.EmptyCommand", (object[] args) => new EmptyCommand()).Execute();
        });

        var TestCommand = new Mock<SaceShips.Lib.Interfaces.ICommand>();
        var TestCommand_after_soft = new Mock<SaceShips.Lib.Interfaces.ICommand>();
        var TestCommand_that_should_be_action2 = new Mock<SaceShips.Lib.Interfaces.ICommand>();

        var queue = Hwdtech.IoC.Resolve<BlockingCollection<SaceShips.Lib.Interfaces.ICommand>>("SpaceShip.Lib.Get.BlockingQueueOfICommand");

        Func<object, object> f = (object x) =>
        {
            // InitScopeInThreadCommand.Object.action();
            // SaceShips.Lib.Interfaces.ICommand z = Hwdtech.IoC.Resolve<SaceShips.Lib.Interfaces.ICommand>("SpaceShip.Lib.EmptyCommand");
            // ((BlockingCollection<SaceShips.Lib.Interfaces.ICommand>)x).TryTake(out z);

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
        Hwdtech.IoC.Resolve<SaceShips.Lib.Interfaces.ICommand>("SpaceShip.Lib.StartServerThreadCommand", thread_test).action();
        Assert.True(mre.WaitOne(10000));
        Assert.True(mre1.WaitOne(10000));
        TestCommand.Verify(p => p.action(), Times.Once());
        TestCommand_after_soft.Verify(p => p.action(), Times.Once());
        TestCommand_that_should_be_action2.Verify(p => p.action(), Times.Once());
        Assert.Equal(queue.Count(), 0);
    }


    // [Fact]
    // public void test_replace_command()
    // {
    //     new Hwdtech.Ioc.InitScopeBasedIoCImplementationCommand().Execute();
    //     Hwdtech.IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", Hwdtech.IoC.Resolve<object>("Scopes.New", Hwdtech.IoC.Resolve<object>("Scopes.Root"))).Execute();
    //     Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Get.ServerThreadStrategy", (object[] args) => new ServerThreadStrategy((SaceShips.Lib.Interfaces.IStartegy)args[0], (BlockingCollection<SaceShips.Lib.Interfaces.ICommand>)args[1])).Execute();
    //     Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Get.BlockingQueueOfICommand", (object[] args) => new BlockingCollection<SaceShips.Lib.Interfaces.ICommand>()).Execute();
    //     Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.StartServerThreadCommand", (object[] args) => new StartServerThreadCommand((ServerThreadStrategy)args[0])).Execute();
    //     Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.WalkingStrategyInThreadReplaceCommand", (object[] args) => new WalkingStrategyInThreadReplaceCommand((IMethodChangeable)args[0], (IStartegy)args[1])).Execute();

    //     ManualResetEvent mre = new ManualResetEvent(false);

    //     var TestCommand1 = new Mock<SaceShips.Lib.Interfaces.ICommand>();
    //     var TestCommand2 = new Mock<SaceShips.Lib.Interfaces.ICommand>();

    //     var ExeStrategy1 = new Mock<SaceShips.Lib.Interfaces.IStartegy>();
    //     var ExeStrategy2 = new Mock<SaceShips.Lib.Interfaces.IStartegy>();

    //     var queue = Hwdtech.IoC.Resolve<BlockingCollection<SaceShips.Lib.Interfaces.ICommand>>("SpaceShip.Lib.Get.BlockingQueueOfICommand");
    //     var thread_test = Hwdtech.IoC.Resolve<SaceShips.Lib.Interfaces.IStartegy>("SpaceShip.Lib.Get.ServerThreadStrategy", ExeStrategy1.Object, queue);
    //     var replacer = Hwdtech.IoC.Resolve<SaceShips.Lib.Interfaces.ICommand>("SpaceShip.Lib.WalkingStrategyInThreadReplaceCommand", thread_test, ExeStrategy2.Object);

    //     TestCommand1.Setup(p => p.action()).Verifiable();
    //     TestCommand2.Setup(p => p.action()).Callback(() => mre.Set()).Verifiable();
    //     ExeStrategy1.Setup(p => p.execute(TestCommand1.Object)).Returns(TestCommand1.Object).Verifiable();
    //     ExeStrategy1.Setup(p => p.execute(replacer)).Returns(replacer).Verifiable();
    //     ExeStrategy2.Setup(p => p.execute(TestCommand2.Object)).Returns(TestCommand2.Object).Verifiable();

    //     queue.Add(TestCommand1.Object);
    //     queue.Add(replacer);
    //     queue.Add(TestCommand2.Object);
    //     Hwdtech.IoC.Resolve<SaceShips.Lib.Interfaces.ICommand>("SpaceShip.Lib.StartServerThreadCommand", thread_test).action();
    //     Assert.True(mre.WaitOne(10000));
    //     TestCommand1.Verify(p => p.action(), Times.Once());
    //     TestCommand2.Verify(p => p.action(), Times.Once());
    //     ExeStrategy1.Verify(p => p.execute(TestCommand1.Object), Times.Once());
    //     ExeStrategy2.Verify(p => p.execute(TestCommand2.Object), Times.Once());
    //     ExeStrategy1.Verify(p => p.execute(replacer), Times.Once());
    //     Assert.Equal(queue.Count(), 0);
    // }
}
