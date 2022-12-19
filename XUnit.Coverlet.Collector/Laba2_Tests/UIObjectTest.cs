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

        CmdExample.Setup(p => p.action());
        ComeBackCommmandStrategy.Setup(p => p.execute(It.IsAny<object[]>())).Returns(CmdExample.Object);
        ComeBackCommmandStrategy.Setup(p => p.execute()).Returns(new Queue<SaceShips.Lib.Interfaces.ICommand>());

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

        // var x = new Dictionary<string, object>();
        // x["angleSpeed"] = new Fraction(90, 1);
        // x["angle"] = new Fraction(45, 1);

        var speed_changeable = new Mock<ISpeedChangeable>();
        // var spaceship = new Mock<SaceShips.Lib.Interfaces.IRotateable>();
        // spaceship.SetupGet(p => p.angle).Returns((Fraction)x["angleSpeed"]).Verifiable();
        // spaceship.SetupGet(p => p.angleSpeed).Returns((Fraction)x["angle"]).Verifiable();

        var UObject = new Mock<IUObject>();
        // UObject.Setup(k => k.set_property("angleSpeed", It.IsAny<Fraction>())).Callback((string key, Fraction speed) => x[key] = speed).Returns(x["angleSpeed"]);
        // UObject.Setup(x => x.set_property("angleSpeed")).Returns<Fraction>(new Fraction(90, 1)).Verifiable();

        speed_changeable.SetupGet(x => x.obj).Returns(UObject.Object);
        speed_changeable.SetupGet(x => x.speed_change).Returns(new Fraction(90, 1)).Verifiable();
        var go_round = new StartRotateCommand(speed_changeable.Object);

        // var go_round = new StartRotateCommand(spaceship.Object);
        go_round.action();
        speed_changeable.VerifyAll();
    }
}