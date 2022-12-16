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
        CmdExample.Setup(p => p.action());

        var ComeBackCommmandStrategy = new Mock<IStartegy>();
        ComeBackCommmandStrategy.Setup(p => p.execute(It.IsAny<object[]>())).Returns(CmdExample.Object);

        // Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register","SpaceShip.Lib.Comands.Execute", (object[] args) => ComeBackCommmandStrategy.Object.execute()).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Vars.Queue", (object[] args) => new Queue<SaceShips.Lib.Interfaces.ICommand>()).Execute();

        
    }
}