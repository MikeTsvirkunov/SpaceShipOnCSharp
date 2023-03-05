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
    public void test_start_thread_and_hardstop_it()
    {
        Init_Score_Env_Laba6();
        var TestExeStrategy = new Mock<SaceShips.Lib.Interfaces.IStartegy>();
        var TestExeCommand = new Mock<SaceShips.Lib.Interfaces.ICommand>();
        var EmptyCommand = new Mock<SaceShips.Lib.Interfaces.ICommand>();
        EmptyCommand.Setup(p => p.action()).Verifiable();
        var x = Hwdtech.IoC.Resolve<BlockingCollection<SaceShips.Lib.Interfaces.ICommand>>("SpaceShip.Lib.Get.BlockingQueueOfICommand");
        TestExeCommand.Setup(p => p.action()).Callback(() => { EmptyCommand.Object.action(); }).Verifiable();
        TestExeStrategy.Setup(p => p.execute(EmptyCommand.Object)).Returns(TestExeCommand.Object).Verifiable();
    
        var thread_test = Hwdtech.IoC.Resolve<SaceShips.Lib.Interfaces.IStartegy>("SpaceShip.Lib.Get.ServerThreadStrategy", TestExeStrategy.Object, x);
        x.Add(EmptyCommand.Object);
        x.Add(Hwdtech.IoC.Resolve<SaceShips.Lib.Interfaces.ICommand>("SpaceShip.Lib.HardStopServerThreadCommand", thread_test));
        
        Hwdtech.IoC.Resolve<SaceShips.Lib.Interfaces.ICommand>("SpaceShip.Lib.StartServerThreadCommand", thread_test).action();
        EmptyCommand.Verify();
        // TestExeStrategy.Verify();
        // TestExeCommand.Verify();
    }
    
    [Fact]
    public void test_change_method_of_thread()
    {
        // Init_Score_Env_Laba6();
        // var TestExeStrategy = new Mock<SaceShips.Lib.Interfaces.IStartegy>();
        // var TestExeCommand = new Mock<SaceShips.Lib.Interfaces`.ICommand>();

        // var TestExeStrategy2 = new Mock<SaceShips.Lib.Interfaces.IStartegy>();
        // var TestExeCommand2 = new Mock<SaceShips.Lib.Interfaces.ICommand>();

        // var EmptyCommand = new Mock<SaceShips.Lib.Interfaces.ICommand>();
        // EmptyCommand.Setup(p => p.action()).Verifiable();
        // var EmptyCommand2 = new Mock<SaceShips.Lib.Interfaces.ICommand>();
        // EmptyCommand2.Setup(p => p.action()).Verifiable();

        // var x = Hwdtech.IoC.Resolve<Queue<SaceShips.Lib.Interfaces.ICommand>>("SpaceShip.Lib.Get.QueueOfICommand");
        // x.Enqueue(EmptyCommand.Object);

        // TestExeCommand.Setup(p => p.action()).Callback(() => { foreach (var item in x) { item.action(); } }).Verifiable();
        // TestExeStrategy.Setup(p => p.execute(x)).Returns(TestExeCommand.Object).Verifiable();

        // var thread_test = Hwdtech.IoC.Resolve<SaceShips.Lib.Interfaces.IStartegy>("SpaceShip.Lib.Get.ServerThreadStrategy", TestExeStrategy.Object);
        
        // TestExeCommand2.Setup(p => p.action()).Callback(() => { foreach (var item in x) { item.action(); } }).Verifiable();
        // TestExeStrategy2.Setup(p => p.execute(x)).Returns(TestExeCommand.Object).Verifiable();

        // var ChangeThreadMethodCommand = new Mock<SaceShips.Lib.Interfaces.ICommand>();
        // ChangeThreadMethodCommand.Setup(p => p.action()).Callback(() => { ((IMethodChangeable)thread_test).ChangeMethod(TestExeStrategy2.Object); }).Verifiable();

        // x.Enqueue(ChangeThreadMethodCommand.Object);
        // x.Enqueue(EmptyCommand2.Object);
        // x.Enqueue(Hwdtech.IoC.Resolve<SaceShips.Lib.Interfaces.ICommand>("SpaceShip.Lib.HardStopServerThreadCommand"));

        // Hwdtech.IoC.Resolve<SaceShips.Lib.Interfaces.ICommand>("SpaceShip.Lib.StartServerThreadCommand", thread_test.execute(x)).action();
        // EmptyCommand.Verify();
        // EmptyCommand2.Verify();
        // ChangeThreadMethodCommand.Verify();
        // TestExeStrategy.Verify();
        // TestExeCommand.Verify();
    }
}
