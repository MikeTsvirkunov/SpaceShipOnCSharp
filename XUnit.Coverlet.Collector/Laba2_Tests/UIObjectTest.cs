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
        // Create Scope
        new Hwdtech.Ioc.InitScopeBasedIoCImplementationCommand().Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", Hwdtech.IoC.Resolve<object>("Scopes.New", Hwdtech.IoC.Resolve<object>("Scopes.Root"))).Execute();

        // Rotatable object
        var spaceship_w_speed_and_angle_parametrs = new Mock<IRotateable>();
        
        // Universal Command
        var CmdExample = new Mock<SaceShips.Lib.Interfaces.ICommand>();
        CmdExample.Setup(p => p.action());

        // Strategies
        var ComeBackCommmandStrategy = new Mock<IStartegy>();
        var ComeBackQueue = new Mock<IStartegy>();
        var ComeBackChangeAngleSpeedStrategy = new Mock<IStartegy>();
        ComeBackCommmandStrategy.Setup(p => p.execute(It.IsAny<object[]>())).Returns(CmdExample.Object);
        ComeBackQueue.Setup(p => p.execute()).Returns(new Queue<SaceShips.Lib.Interfaces.ICommand>());
        ComeBackChangeAngleSpeedStrategy.Setup(p => p.execute(It.IsAny<IUObject>(), It.IsAny<Fraction>));

        // Return rotate
        var ComeBackRotateCommmandStrategy = new Mock<IStartegy>();
        var k = new RotateMove(spaceship_w_speed_and_angle_parametrs.Object);
        ComeBackRotateCommmandStrategy.Setup(p => p.execute(It.IsAny<object[]>())).Returns(new RotateMove(spaceship_w_speed_and_angle_parametrs.Object));


        // Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Comands.Rotate", (object[] args) => ComeBackRotateCommmandStrategy.Object.execute(args[0])).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Comands.Set", (object[] args) => ComeBackCommmandStrategy.Object.execute(args)).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Comands.Rotate", (object[] args) => ComeBackRotateCommmandStrategy.Object.execute(args)).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Vars.Queue", (object[] args) => ComeBackQueue.Object.execute()).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Comands.ChangeAngleSpeed", (object[] args) => ComeBackChangeAngleSpeedStrategy.Object.execute(args)).Execute();

    }

    [Fact]
    public void StartCommand_Exe()
    {
        Init_Score_Env();

        var dict = new Dictionary<string, object>();

        var UObject = new Mock<IUObject>();
        UObject.Setup(x => x.set_property("angleSpeed", It.IsAny<Fraction>())).Callback<string, object>((string a, object z) => dict["angleSpeed"] = z);

        var speed_changeable = new Mock<ISpeedChangeable>();
        speed_changeable.SetupGet(x => x.obj).Returns(UObject.Object).Verifiable();
        speed_changeable.SetupGet(x => x.speed_change).Returns(new Fraction(90, 1)).Verifiable();

        var RotComStart = new Mock<IRotateCommandStartable>();
        RotComStart.SetupGet(x => x.angleSpeed).Returns(It.IsAny<Fraction>()).Verifiable();
        RotComStart.SetupGet(x => x.obj).Returns(UObject.Object).Verifiable();
        RotComStart.SetupGet(x => x.queue).Returns(new Queue<SaceShips.Lib.Interfaces.ICommand>()).Verifiable();

        var go_round = new StartRotateCommand(RotComStart.Object);

        go_round.action();
        RotComStart.Verify();
    }

    [Fact]
    public void StartCommand_Exe_WO_Object()
    {
        Init_Score_Env();

        var x = new Dictionary<string, object>();

        var speed_changeable = new Mock<ISpeedChangeable>();
        var UObject = new Mock<IUObject>();
        var RotComStart = new Mock<IRotateCommandStartable>();
        RotComStart.SetupGet(x => x.angleSpeed).Throws(new Exception()).Verifiable();
        RotComStart.SetupGet(x => x.obj).Returns(It.IsAny<IUObject>).Verifiable();
        RotComStart.SetupGet(x => x.queue).Returns(It.IsAny<Queue<SaceShips.Lib.Interfaces.ICommand>>).Verifiable();
        var go_round = new StartRotateCommand(RotComStart.Object);

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
        RotComStart.SetupGet(x => x.angleSpeed).Throws(new Exception()).Verifiable();
        RotComStart.SetupGet(x => x.obj).Returns(It.IsAny<IUObject>).Verifiable();
        RotComStart.SetupGet(x => x.queue).Returns(It.IsAny<Queue<SaceShips.Lib.Interfaces.ICommand>>).Verifiable();
        var go_round = new StartRotateCommand(RotComStart.Object);

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
        speed_changeable.SetupGet(x => x.obj).Throws(new Exception()).Verifiable();
        speed_changeable.SetupGet(x => x.speed_change).Throws(new Exception()).Verifiable();
        RotComStart.SetupGet(x => x.angleSpeed).Returns(It.IsAny<Fraction>()).Verifiable();
        RotComStart.SetupGet(x => x.obj).Throws(new Exception()).Verifiable();
        RotComStart.SetupGet(x => x.queue).Returns(It.IsAny<Queue<SaceShips.Lib.Interfaces.ICommand>>).Verifiable();
        var go_round = new StartRotateCommand(RotComStart.Object);

        Assert.Throws<System.Exception>(() => go_round.action());
    }
}
