using Xunit;
using SaceShips.Lib.Classes;
using SaceShips.Lib.Interfaces;
using Moq;
using System.Collections.Generic;
using System;

namespace XUnit.Coverlet.Collector;
public class EndMovingCommandsTest
{
    static EndMovingCommandsTest()
    {
        new Hwdtech.Ioc.InitScopeBasedIoCImplementationCommand().Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", Hwdtech.IoC.Resolve<object>("Scopes.New", Hwdtech.IoC.Resolve<object>("Scopes.Root"))).Execute();

        Mock<SaceShips.Lib.Interfaces.ICommand> mockCommand = new Mock<SaceShips.Lib.Interfaces.ICommand>();
        mockCommand.Setup(m => m.action());

        Mock<IStartegy> mockStrategyDelete = new Mock<IStartegy>();
        mockStrategyDelete.Setup(m => m.execute(It.IsAny<object[]>())).Returns(mockCommand.Object);
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.Object.DeleteProperty", (object[] args) => mockStrategyDelete.Object.execute(args)).Execute();

        Mock<IStartegy> mockStrategyInject = new Mock<IStartegy>();
        mockStrategyInject.Setup(m => m.execute()).Returns(mockCommand.Object);
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.Commands.InjectEmptyCommand", (object[] args) => mockStrategyInject.Object.execute(args)).Execute();
    }

    [Fact]
    public void EndMoveCommandGood()
    {
        Mock<IMoveCommandEndable> stop_obj = new Mock<IMoveCommandEndable>();
        Mock<IUObject> mockUobj = new Mock<IUObject>();
        stop_obj.SetupGet(m => m.uobj).Returns(mockUobj.Object).Verifiable();
        stop_obj.SetupGet(m => m.properties).Returns(new List<string>() { "Velocity" }).Verifiable();
        SaceShips.Lib.Interfaces.ICommand stop_cmd = new EndMoveCommand(stop_obj.Object);
        stop_cmd.action();
    }

    [Fact]
    public void TestNegativeGetUobject()
    {
        Mock<IMoveCommandEndable> stop_obj = new Mock<IMoveCommandEndable>();
        stop_obj.SetupGet(m => m.uobj).Throws<Exception>().Verifiable();
        stop_obj.SetupGet(m => m.properties).Returns(new List<string>() { "Velocity" }).Verifiable();
        SaceShips.Lib.Interfaces.ICommand stop_cmd = new EndMoveCommand(stop_obj.Object);
        Assert.Throws<Exception>(() => stop_cmd.action());
    }

    [Fact]
    public void TestNegativeGetProperties()
    {
        Mock<IMoveCommandEndable> stop_obj = new Mock<IMoveCommandEndable>();
        Mock<IUObject> mockUobj = new Mock<IUObject>();
        stop_obj.SetupGet(m => m.uobj).Returns(mockUobj.Object).Verifiable();
        stop_obj.SetupGet(m => m.properties).Throws<Exception>().Verifiable();
        SaceShips.Lib.Interfaces.ICommand stop_cmd = new EndMoveCommand(stop_obj.Object);
        Assert.Throws<Exception>(() => stop_cmd.action());
    }
}
