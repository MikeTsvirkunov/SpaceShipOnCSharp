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
        var EmptyCommand1 = new Mock<SaceShips.Lib.Interfaces.ICommand>();
        
        EmptyCommand1.Setup(p => p.action()).Verifiable();
        var x = Hwdtech.IoC.Resolve<BlockingCollection<SaceShips.Lib.Interfaces.ICommand>>("SpaceShip.Lib.Get.BlockingQueueOfICommand");
        x.Add(EmptyCommand1.Object);
        x.Add(EmptyCommand1.Object);
        ExeStrategy.Setup(p => p.execute(x)).Returns(ExeCommand.Object).Verifiable();
        ExeCommand.Setup(p => p.action()).Callback(() => { while (x.Count() > 0) { ((SaceShips.Lib.Interfaces.ICommand)(x.Take())).action(); }}).Verifiable();
    
        var thread_test = Hwdtech.IoC.Resolve<SaceShips.Lib.Interfaces.IStartegy>("SpaceShip.Lib.Get.ServerThreadStrategy", ExeStrategy.Object, x);
        
        Hwdtech.IoC.Resolve<SaceShips.Lib.Interfaces.ICommand>("SpaceShip.Lib.StartServerThreadCommand", thread_test).action();
        EmptyCommand1.Verify(p => p.action(), Times.Exactly(2));
        // TestExeStrategy.Verify();
        // TestExeCommand.Verify();
    }

    [Fact]
    public void test_hard_stop_thread()
    {
        Init_Score_Env_Laba6();
        var ExeStrategy = new Mock<SaceShips.Lib.Interfaces.IStartegy>();
        var ExeCommand = new Mock<SaceShips.Lib.Interfaces.ICommand>();
        var TestCMD = new Mock<SaceShips.Lib.Interfaces.ICommand>();

        TestCMD.Setup(p => p.action()).Verifiable();
        
        var y = Hwdtech.IoC.Resolve<BlockingCollection<SaceShips.Lib.Interfaces.ICommand>>("SpaceShip.Lib.Get.BlockingQueueOfICommand");
    
        ExeStrategy.Setup(p => p.execute(y)).Returns(ExeCommand.Object).Verifiable();
        ExeCommand.Setup(p => p.action()).Callback(() => { while (y.Count() > 0) { ((SaceShips.Lib.Interfaces.ICommand)(y.Take())).action(); } }).Verifiable();

        var thread_test2 = Hwdtech.IoC.Resolve<SaceShips.Lib.Interfaces.IStartegy>("SpaceShip.Lib.Get.ServerThreadStrategy", ExeStrategy.Object, y);

        y.Add(TestCMD.Object);
        y.Add(TestCMD.Object);
        y.Add(Hwdtech.IoC.Resolve<SaceShips.Lib.Interfaces.ICommand>("SpaceShip.Lib.HardStopServerThreadCommand", thread_test2));
        y.Add(TestCMD.Object);
        y.Add(TestCMD.Object);

        Hwdtech.IoC.Resolve<SaceShips.Lib.Interfaces.ICommand>("SpaceShip.Lib.StartServerThreadCommand", thread_test2).action();
        ExeStrategy.Verify(p => p.execute(y), Times.Once());
        ExeCommand.Verify(p => p.action(), Times.Once());
        Assert.Equal(y.Count(), 2);
        TestCMD.Verify(p => p.action(), Times.Exactly(2));
    }
}
