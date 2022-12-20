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
    public void Init_Score_Env()
    {
        // Added std Functions to Scope
        new Hwdtech.Ioc.InitScopeBasedIoCImplementationCommand().Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", Hwdtech.IoC.Resolve<object>("Scopes.New", Hwdtech.IoC.Resolve<object>("Scopes.Root"))).Execute();

        var spaceship_w_speed_and_angle_parametrs = new Mock<IRotateable>();
        var CmdExample = new Mock<SaceShips.Lib.Interfaces.ICommand>();
        var ComeBackCommmandStrategy = new Mock<IStartegy>();
        var ComeBackQueue = new Mock<IStartegy>();
        var queue = new Queue<SaceShips.Lib.Interfaces.ICommand>();
        CmdExample.Setup(p => p.action());
        ComeBackCommmandStrategy.Setup(p => p.execute(It.IsAny<object[]>())).Returns(CmdExample.Object);
        ComeBackQueue.Setup(p => p.execute()).Returns(queue);

        // Return rotate
        var ComeBackRotateCommmandStrategy = new Mock<IStartegy>();
        var k = new RotateMove(spaceship_w_speed_and_angle_parametrs.Object);
        ComeBackRotateCommmandStrategy.Setup(p => p.execute(It.IsAny<object[]>())).Callback((object a) => k = new RotateMove((SaceShips.Lib.Interfaces.IRotateable)a)).Returns(k);

        // Set to queue
        var SetToQueueStrategy = new Mock<IStartegy>();
        ComeBackRotateCommmandStrategy.Setup(p => p.execute(It.IsAny<object[]>())).Callback((object[] args) => ((Queue<SaceShips.Lib.Interfaces.ICommand>)args[0]).Enqueue((SaceShips.Lib.Interfaces.ICommand)args[1]));


        // Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Comands.Rotate", (object[] args) => ComeBackRotateCommmandStrategy.Object.execute(args[0])).Execute();

        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Comands.Set", (object[] args) => ComeBackCommmandStrategy.Object.execute(args)).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Comands.Rotate", (object[] args) => ComeBackCommmandStrategy.Object.execute(args)).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Vars.Queue", (object[] args) => ComeBackQueue.Object.execute()).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Comands.Enqueue", (object[] args) => ComeBackCommmandStrategy.Object.execute(args)).Execute();
    }

    [Fact]
    public void StartCommand_Exe()
    {
        Init_Score_Env();

        var x = new Dictionary<string, object>();

        var speed_changeable = new Mock<ISpeedChangeable>();
        var UObject = new Mock<IUObject>();
        var RotComStart = new Mock<IRotateCommandStartable>();
        // var spaceship = new Mock<SaceShips.Lib.Interfaces.IRotateable>();
        // spaceship.SetupGet(p => p.angle).Returns((Fraction)x["angleSpeed"]).Verifiable();
        // spaceship.SetupGet(p => p.angleSpeed).Returns((Fraction)x["angle"]).Verifiable();
        // RotComStart.SetupGet(p => p.angleSpeed).Returns((Fraction)x["angleSpeed"]).Verifiable();
        // RotComStart.SetupGet(p => p.angleSpeed).Returns((Fraction)x["angleSpeed"]).Verifiable();
        // var speed_change = new Mock<SaceShips.Lib.Interfaces.ICommand>();
        // speed_change.Setup(k = k.action()).Callback()
        // UObject.Setup(k => k.get_property("angleSpeed")).Returns(new Fraction(90, 1));
        // UObject.Setup(x => x.set_property("angleSpeed")).Returns<Fraction>(new Fraction(90, 1)).Verifiable();

        speed_changeable.SetupGet(x => x.obj).Returns(UObject.Object).Verifiable();
        speed_changeable.SetupGet(x => x.speed_change).Returns(new Fraction(90, 1)).Verifiable();
        RotComStart.SetupGet(x => x.angleSpeed).Returns(It.IsAny<Fraction>()).Verifiable();
        RotComStart.SetupGet(x => x.obj).Returns(It.IsAny<IUObject>).Verifiable();
        RotComStart.SetupGet(x => x.queue).Returns(It.IsAny<Queue<SaceShips.Lib.Interfaces.ICommand>>).Verifiable();
        var go_round = new StartRotateCommand(speed_changeable.Object);

        // var go_round = new StartRotateCommand(spaceship.Object);
        go_round.action();
        speed_changeable.VerifyAll();
    }

    [Fact]
    public void StartCommand_Exe_WO_Object()
    {
        Init_Score_Env();

        var x = new Dictionary<string, object>();

        var speed_changeable = new Mock<ISpeedChangeable>();
        var UObject = new Mock<IUObject>();
        var RotComStart = new Mock<IRotateCommandStartable>();
        // var spaceship = new Mock<SaceShips.Lib.Interfaces.IRotateable>();
        // spaceship.SetupGet(p => p.angle).Returns((Fraction)x["angleSpeed"]).Verifiable();
        // spaceship.SetupGet(p => p.angleSpeed).Returns((Fraction)x["angle"]).Verifiable();
        // RotComStart.SetupGet(p => p.angleSpeed).Returns((Fraction)x["angleSpeed"]).Verifiable();
        // RotComStart.SetupGet(p => p.angleSpeed).Returns((Fraction)x["angleSpeed"]).Verifiable();
        // var speed_change = new Mock<SaceShips.Lib.Interfaces.ICommand>();
        // speed_change.Setup(k = k.action()).Callback()
        // UObject.Setup(k => k.get_property("angleSpeed")).Returns(new Fraction(90, 1));
        // UObject.Setup(x => x.set_property("angleSpeed")).Returns<Fraction>(new Fraction(90, 1)).Verifiable();

        speed_changeable.SetupGet(x => x.obj).Throws(new Exception()).Verifiable();
        speed_changeable.SetupGet(x => x.speed_change).Returns(new Fraction(90, 1)).Verifiable();
        RotComStart.SetupGet(x => x.angleSpeed).Returns(It.IsAny<Fraction>()).Verifiable();
        RotComStart.SetupGet(x => x.obj).Returns(It.IsAny<IUObject>).Verifiable();
        RotComStart.SetupGet(x => x.queue).Returns(It.IsAny<Queue<SaceShips.Lib.Interfaces.ICommand>>).Verifiable();
        var go_round = new StartRotateCommand(speed_changeable.Object);

        // var go_round = new StartRotateCommand(spaceship.Object);
        Assert.Throws<System.Exception>(() => go_round.action());
    }

    [Fact]
    public void StartCommand_Exe_WO_Speed()
    {
        Init_Score_Env();

        var x = new Dictionary<string, object>();

        var speed_changeable = new Mock<ISpeedChangeable>();
        var UObject = new Mock<IUObject>();
        var RotComStart = new Mock<IRotateCommandStartable>();
        // var spaceship = new Mock<SaceShips.Lib.Interfaces.IRotateable>();
        // spaceship.SetupGet(p => p.angle).Returns((Fraction)x["angleSpeed"]).Verifiable();
        // spaceship.SetupGet(p => p.angleSpeed).Returns((Fraction)x["angle"]).Verifiable();
        // RotComStart.SetupGet(p => p.angleSpeed).Returns((Fraction)x["angleSpeed"]).Verifiable();
        // RotComStart.SetupGet(p => p.angleSpeed).Returns((Fraction)x["angleSpeed"]).Verifiable();
        // var speed_change = new Mock<SaceShips.Lib.Interfaces.ICommand>();
        // speed_change.Setup(k = k.action()).Callback()
        // UObject.Setup(k => k.get_property("angleSpeed")).Returns(new Fraction(90, 1));
        // UObject.Setup(x => x.set_property("angleSpeed")).Returns<Fraction>(new Fraction(90, 1)).Verifiable();

        speed_changeable.SetupGet(x => x.obj).Returns(UObject.Object).Verifiable();
        speed_changeable.SetupGet(x => x.speed_change).Throws(new Exception()).Verifiable();
        RotComStart.SetupGet(x => x.angleSpeed).Returns(It.IsAny<Fraction>()).Verifiable();
        RotComStart.SetupGet(x => x.obj).Returns(It.IsAny<IUObject>).Verifiable();
        RotComStart.SetupGet(x => x.queue).Returns(It.IsAny<Queue<SaceShips.Lib.Interfaces.ICommand>>).Verifiable();
        var go_round = new StartRotateCommand(speed_changeable.Object);

        // var go_round = new StartRotateCommand(spaceship.Object);
        Assert.Throws<System.Exception>(() => go_round.action());
    }

    [Fact]
    public void StartCommand_Exe_WO_All()
    {
        Init_Score_Env();

        var x = new Dictionary<string, object>();

        var speed_changeable = new Mock<ISpeedChangeable>();
        var UObject = new Mock<IUObject>();
        var RotComStart = new Mock<IRotateCommandStartable>();
        // var spaceship = new Mock<SaceShips.Lib.Interfaces.IRotateable>();
        // spaceship.SetupGet(p => p.angle).Returns((Fraction)x["angleSpeed"]).Verifiable();
        // spaceship.SetupGet(p => p.angleSpeed).Returns((Fraction)x["angle"]).Verifiable();
        // RotComStart.SetupGet(p => p.angleSpeed).Returns((Fraction)x["angleSpeed"]).Verifiable();
        // RotComStart.SetupGet(p => p.angleSpeed).Returns((Fraction)x["angleSpeed"]).Verifiable();
        // var speed_change = new Mock<SaceShips.Lib.Interfaces.ICommand>();
        // speed_change.Setup(k = k.action()).Callback()
        // UObject.Setup(k => k.get_property("angleSpeed")).Returns(new Fraction(90, 1));
        // UObject.Setup(x => x.set_property("angleSpeed")).Returns<Fraction>(new Fraction(90, 1)).Verifiable();

        speed_changeable.SetupGet(x => x.obj).Throws(new Exception()).Verifiable();
        speed_changeable.SetupGet(x => x.speed_change).Throws(new Exception()).Verifiable();
        RotComStart.SetupGet(x => x.angleSpeed).Returns(It.IsAny<Fraction>()).Verifiable();
        RotComStart.SetupGet(x => x.obj).Returns(It.IsAny<IUObject>).Verifiable();
        RotComStart.SetupGet(x => x.queue).Returns(It.IsAny<Queue<SaceShips.Lib.Interfaces.ICommand>>).Verifiable();
        var go_round = new StartRotateCommand(speed_changeable.Object);

        // var go_round = new StartRotateCommand(spaceship.Object);
        Assert.Throws<System.Exception>(() => go_round.action());
    }
}