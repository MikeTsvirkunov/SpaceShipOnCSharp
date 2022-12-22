// using Xunit;
// using SaceShips.Lib.Classes;
// using SaceShips.Lib.Interfaces;
// using Moq;
// using System.Collections.Generic;
// using System.Collections.Concurrent;
// using System;
// using System.Diagnostics;
// using Hwdtech;
// // using Hwdtech.IoC;
// // using Hwdtech.ScopeBasedIoCImplementation;
// namespace XUnit.Coverlet.Collector;
// public class UIObjectTest
// {
//     [Fact]
//     public void Init_Score_Env()
//     {
//         // Create Scope
//         new Hwdtech.Ioc.InitScopeBasedIoCImplementationCommand().Execute();
//         Hwdtech.IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", Hwdtech.IoC.Resolve<object>("Scopes.New", Hwdtech.IoC.Resolve<object>("Scopes.Root"))).Execute();

//         // Rotatable object
//         var spaceship_w_speed_and_angle_parametrs = new Mock<IRotateable>();

//         // Universal Command
//         var CmdExample = new Mock<SaceShips.Lib.Interfaces.ICommand>();
//         CmdExample.Setup(p => p.action());

//         // Strategies
//         var ComeBackCommmandStrategy = new Mock<IStartegy>();
//         var ComeBackQueue = new Mock<IStartegy>();
//         var ComeBackChangeAngleSpeedStrategy = new Mock<IStartegy>();
//         ComeBackCommmandStrategy.Setup(p => p.execute(It.IsAny<object[]>())).Returns(CmdExample.Object);
//         ComeBackQueue.Setup(p => p.execute()).Returns(new Queue<SaceShips.Lib.Interfaces.ICommand>());
//         ComeBackChangeAngleSpeedStrategy.Setup(p => p.execute(It.IsAny<IUObject>(), It.IsAny<Fraction>));

//         // Return rotate
//         var ComeBackRotateCommmandStrategy = new Mock<IStartegy>();
//         var k = new RotateMove(spaceship_w_speed_and_angle_parametrs.Object);
//         ComeBackRotateCommmandStrategy.Setup(p => p.execute(It.IsAny<object[]>())).Returns(new RotateMove(spaceship_w_speed_and_angle_parametrs.Object));

//         Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Comands.Set", (object[] args) => ComeBackCommmandStrategy.Object.execute(args)).Execute();
//         Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Comands.Rotate", (object[] args) => ComeBackRotateCommmandStrategy.Object.execute(args)).Execute();
//         Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Vars.Queue", (object[] args) => ComeBackQueue.Object.execute()).Execute();
//         Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Comands.ChangeAngleSpeed", (object[] args) => ComeBackChangeAngleSpeedStrategy.Object.execute(args)).Execute();

//     }
// }