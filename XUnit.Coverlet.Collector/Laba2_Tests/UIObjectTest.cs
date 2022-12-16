using Xunit;
using SaceShips.Lib.Classes;
using SaceShips.Lib.Interfaces;
using Moq;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System;
using System.Diagnostics;
using Hwdtech;
// using Hwdtech.IoC;
// using Hwdtech.ScopeBasedIoCImplementation;
namespace XUnit.Coverlet.Collector;
public class UIObjectTest
{
    [Fact]
    public void Init_Score_Env()
    {
        // Added std Functions to Scope
        new Hwdtech.Ioc.InitScopeBasedIoCImplementationCommand().Execute();

        var CmdExample = new Mock<SaceShips.Lib.Interfaces.ICommand>();
        var ComeBackCommmandStrategy = new Mock<IStartegy>();
        var ComeBackQueue = new Mock<IStartegy>();

        CmdExample.Setup(p => p.action());
        ComeBackCommmandStrategy.Setup(p => p.execute(It.IsAny<object[]>())).Returns(CmdExample.Object);
        ComeBackCommmandStrategy.Setup(p => p.execute()).Returns(new Queue<SaceShips.Lib.Interfaces.ICommand>());

        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register","SpaceShip.Lib.Comands.Set", (object[] args) => ComeBackCommmandStrategy.Object.execute(args)).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Comands.Rotate", (object[] args) => ComeBackCommmandStrategy.Object.execute(args)).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Comands.Enqueue", (object[] args) => ComeBackCommmandStrategy.Object.execute(args)).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Vars.Queue", (object[] args) => ComeBackQueue.Object.execute()).Execute();
    }

    [Fact]
    public void StartCommand_Exe()
    {
        var x = new Dictionary<string, object>();
        var speed_changeable = new Mock<ISpeedChangeable>();
        var UObject = new Mock<IUObject>();
        // UObject.Setup(x => x.set_property(It.IsAny<string>(), It.IsAny<object>())).Callback((string key));
        // UObject.Setup(x => x.set_property("angleSpeed")).Returns<Fraction>(new Fraction(90, 1)).Verifiable();
        speed_changeable.SetupGet(x => x.obj).Returns(UObject.Object);
        speed_changeable.SetupGet(x => x.speed_change).Returns(new Fraction(90, 1)).Verifiable();
        var go_round = new StartRotateCommand(speed_changeable.Object);
        go_round.action();
    }
}