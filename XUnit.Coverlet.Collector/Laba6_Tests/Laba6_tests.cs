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
    // [Fact]
    // public void test_start_thread()
    // {
    //     new Hwdtech.Ioc.InitScopeBasedIoCImplementationCommand().Execute();
    //     Hwdtech.IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", Hwdtech.IoC.Resolve<object>("Scopes.New", Hwdtech.IoC.Resolve<object>("Scopes.Root"))).Execute();
    //     Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Get.ServerThreadStrategy", (object[] args) => new ServerThreadStrategy((SaceShips.Lib.Interfaces.IStartegy)args[0], (BlockingCollection<SaceShips.Lib.Interfaces.ICommand>)args[1])).Execute();
    //     Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Get.BlockingQueueOfICommand", (object[] args) => new BlockingCollection<SaceShips.Lib.Interfaces.ICommand>()).Execute();
    //     Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.StartServerThreadCommand", (object[] args) => new StartServerThreadCommand((ServerThreadStrategy)args[0])).Execute();
    //     Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.HardStopServerThreadCommand", (object[] args) => new HardStopServerThreadCommand((ServerThreadStrategy)args[0])).Execute();
    //     Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.SoftStopServerThreadCommand", (object[] args) => new SoftStopServerThreadCommand((ServerThreadStrategy)args[0], (BlockingCollection<SaceShips.Lib.Interfaces.ICommand>)args[1])).Execute();


    //     var ExeStrategy = new Mock<SaceShips.Lib.Interfaces.IStartegy>();
    //     var ExeCommand = new Mock<SaceShips.Lib.Interfaces.ICommand>();
    //     var TestCommand = new Mock<SaceShips.Lib.Interfaces.ICommand>();

    //     TestCommand.Setup(p => p.action()).Verifiable();

    //     var x = Hwdtech.IoC.Resolve<BlockingCollection<SaceShips.Lib.Interfaces.ICommand>>("SpaceShip.Lib.Get.BlockingQueueOfICommand");
    //     ExeCommand.Setup(p => p.action()).Callback(() => { TestCommand.Object.action(); }).Verifiable();
    //     ExeStrategy.Setup(p => p.execute(TestCommand.Object)).Returns(ExeCommand.Object).Verifiable();
    
    //     var thread_test = Hwdtech.IoC.Resolve<SaceShips.Lib.Interfaces.IStartegy>("SpaceShip.Lib.Get.ServerThreadStrategy", ExeStrategy.Object, x);

    //     x.Add(TestCommand.Object);
    //     x.Add(TestCommand.Object);
    //     x.Add(TestCommand.Object);
        
    //     Hwdtech.IoC.Resolve<SaceShips.Lib.Interfaces.ICommand>("SpaceShip.Lib.StartServerThreadCommand", thread_test).action();
    //     TestCommand.Verify(p => p.action(), Times.Exactly(3));
    // }

    [Fact]
    public void test_replace_function()
    {
        new Hwdtech.Ioc.InitScopeBasedIoCImplementationCommand().Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", Hwdtech.IoC.Resolve<object>("Scopes.New", Hwdtech.IoC.Resolve<object>("Scopes.Root"))).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Get.ServerThreadStrategy", (object[] args) => new ServerThreadStrategy((SaceShips.Lib.Interfaces.IStartegy)args[0], (BlockingCollection<SaceShips.Lib.Interfaces.ICommand>)args[1])).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Get.BlockingQueueOfICommand", (object[] args) => new BlockingCollection<SaceShips.Lib.Interfaces.ICommand>()).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.StartServerThreadCommand", (object[] args) => new StartServerThreadCommand((ServerThreadStrategy)args[0])).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.HardStopServerThreadCommand", (object[] args) => new HardStopServerThreadCommand((ServerThreadStrategy)args[0])).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.SoftStopServerThreadCommand", (object[] args) => new SoftStopServerThreadCommand((ServerThreadStrategy)args[0], (BlockingCollection<SaceShips.Lib.Interfaces.ICommand>)args[1])).Execute();


        var ExeStrategy1 = new Mock<SaceShips.Lib.Interfaces.IStartegy>();
        var ExeStrategy2 = new Mock<SaceShips.Lib.Interfaces.IStartegy>();
        var ExeCommand = new Mock<SaceShips.Lib.Interfaces.ICommand>();
        var TestCommand = new Mock<SaceShips.Lib.Interfaces.ICommand>();
        var TestReplaceFuncCommand = new Mock<SaceShips.Lib.Interfaces.ICommand>();

        TestCommand.Setup(p => p.action()).Verifiable();
        ExeCommand.Setup(p => p.action()).Callback(() => { TestCommand.Object.action(); }).Verifiable();
        ExeStrategy1.Setup(p => p.execute(TestCommand.Object)).Returns(ExeCommand.Object).Verifiable();
        ExeStrategy1.Setup(p => p.execute(TestReplaceFuncCommand.Object)).Returns(TestReplaceFuncCommand.Object).Verifiable();
        ExeStrategy2.Setup(p => p.execute(TestCommand.Object)).Returns(ExeCommand.Object).Verifiable();

        var x = Hwdtech.IoC.Resolve<BlockingCollection<SaceShips.Lib.Interfaces.ICommand>>("SpaceShip.Lib.Get.BlockingQueueOfICommand");

        var thread_test = Hwdtech.IoC.Resolve<SaceShips.Lib.Interfaces.IStartegy>("SpaceShip.Lib.Get.ServerThreadStrategy", ExeStrategy1.Object, x);

        TestReplaceFuncCommand.Setup(p => p.action()).Callback(() => { ((ServerThreadStrategy)thread_test).ChangeMethod(ExeStrategy2.Object); }).Verifiable();
        
        x.Add(TestCommand.Object);
        x.Add(TestCommand.Object);
        x.Add(TestReplaceFuncCommand.Object);
        x.Add(TestCommand.Object);
        x.Add(TestCommand.Object);

        Hwdtech.IoC.Resolve<SaceShips.Lib.Interfaces.ICommand>("SpaceShip.Lib.StartServerThreadCommand", thread_test).action();
        ExeStrategy1.Verify(p => p.execute(TestCommand.Object), Times.Exactly(2));
        ExeStrategy2.Verify(p => p.execute(TestCommand.Object), Times.Exactly(2));
    }

    [Fact]
    public void test_start_walking_walking_strategy_added()
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
        TestCommand.Setup(p => p.action()).Callback(() => mre.Set()).Verifiable();

        var queue = Hwdtech.IoC.Resolve<BlockingCollection<SaceShips.Lib.Interfaces.ICommand>>("SpaceShip.Lib.Get.BlockingQueueOfICommand");

        Func<object, object> f = (object x) => { 
            return x;};
        var thread_test = Hwdtech.IoC.Resolve<SaceShips.Lib.Interfaces.IStartegy>("SpaceShip.Lib.Get.ServerThreadStrategy", Hwdtech.IoC.Resolve<SaceShips.Lib.Interfaces.IStartegy>("SpaceShip.Lib.WalkerInQueueStrategy", f), queue);
        var soft_stop_cmd = Hwdtech.IoC.Resolve<SaceShips.Lib.Interfaces.ICommand>("SpaceShip.Lib.SoftStopServerThreadCommand", thread_test, queue);

        queue.Add(TestCommand.Object);
        Hwdtech.IoC.Resolve<SaceShips.Lib.Interfaces.ICommand>("SpaceShip.Lib.StartServerThreadCommand", thread_test).action();
        Assert.True(mre.WaitOne(10000));
        TestCommand.Verify(p=>p.action(), Times.Once());
        Assert.Equal(queue.Count(), 0);
    }

    [Fact]
    public void test_hard_stop_walking_strategy_added()
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
            return x;
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
    public void test_soft_stop_walking_strategy_added()
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

        var TestCommand = new Mock<SaceShips.Lib.Interfaces.ICommand>();
        var TestCommand_after_soft = new Mock<SaceShips.Lib.Interfaces.ICommand>();
        var TestCommand_that_should_not_be_action = new Mock<SaceShips.Lib.Interfaces.ICommand>();

        var queue = Hwdtech.IoC.Resolve<BlockingCollection<SaceShips.Lib.Interfaces.ICommand>>("SpaceShip.Lib.Get.BlockingQueueOfICommand");

        Func<object, object> f = (object x) =>
        {
            return x;
        };
        var thread_test = Hwdtech.IoC.Resolve<SaceShips.Lib.Interfaces.IStartegy>("SpaceShip.Lib.Get.ServerThreadStrategy", Hwdtech.IoC.Resolve<SaceShips.Lib.Interfaces.IStartegy>("SpaceShip.Lib.WalkerInQueueStrategy", f), queue);
        var soft_stop_cmd = Hwdtech.IoC.Resolve<SaceShips.Lib.Interfaces.ICommand>("SpaceShip.Lib.SoftStopServerThreadCommand", thread_test, queue);

        TestCommand.Setup(p => p.action()).Callback(() => mre.Set()).Verifiable();
        TestCommand_that_should_not_be_action.Setup(p => p.action()).Verifiable();
        TestCommand_after_soft.Setup(p => p.action()).Callback(() => {queue.Add(TestCommand_that_should_not_be_action.Object); mre1.Set();}).Verifiable();
        
        queue.Add(TestCommand.Object);
        queue.Add(soft_stop_cmd);
        queue.Add(TestCommand_after_soft.Object);
        Hwdtech.IoC.Resolve<SaceShips.Lib.Interfaces.ICommand>("SpaceShip.Lib.StartServerThreadCommand", thread_test).action();
        Assert.True(mre.WaitOne(10000));
        Assert.True(mre1.WaitOne(10000));
        TestCommand.Verify(p => p.action(), Times.Once());
        TestCommand_after_soft.Verify(p => p.action(), Times.Once());
        TestCommand_that_should_not_be_action.Verify(p => p.action(), Times.Never());
        Assert.Equal(queue.Count(), 1);
        Assert.Equal(queue.Take(), TestCommand_that_should_not_be_action.Object);
    }
}
