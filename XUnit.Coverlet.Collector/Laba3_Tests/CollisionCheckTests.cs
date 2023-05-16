using Hwdtech.Ioc;
using Moq;
using Xunit;
using SaceShips.Lib.Classes;
using SaceShips.Lib.Interfaces;
using System.Collections.Generic;
using System;

namespace XUnit.Coverlet.Collector;
public class CollisionCheckTests
{

    public CollisionCheckTests()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", Hwdtech.IoC.Resolve<object>("Scopes.New", Hwdtech.IoC.Resolve<object>("Scopes.Root"))).Execute();

        var getPropStrategy = new GetPropertyStrategy();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SaceShips.GetProperty", (object[] args) => getPropStrategy.execute(args)).Execute();


    }

    [Fact]
    public void CollisionCheckReturnsTrue()
    {
        var obj1 = new Mock<IUObject>();
        var obj2 = new Mock<IUObject>();

        foreach (string prop in new List<string>() { "Position", "Velocity" })
        {
            obj1.Setup(x => x.get_property(prop)).Returns(new Vector(It.IsAny<int>(), It.IsAny<int>()));
            obj2.Setup(x => x.get_property(prop)).Returns(new Vector(It.IsAny<int>(), It.IsAny<int>()));
        }

        var CheckCollisionStrategy = new Mock<IStartegy>();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SaceShips.CheckCollision", (object[] args) => CheckCollisionStrategy.Object.execute(args)).Execute();
        CheckCollisionStrategy.Setup(col => col.execute(It.IsAny<object[]>())).Returns(true).Verifiable();

        var checkCollision = new CollisionCheckCommand(obj1.Object, obj2.Object);

        Assert.Throws<Exception>(() => checkCollision.action());
        CheckCollisionStrategy.Verify();
    }

    [Fact]
    public void CollisionCheckReturnsFalse()
    {
        var obj1 = new Mock<IUObject>();
        var obj2 = new Mock<IUObject>();

        foreach (string prop in new List<string>() { "Position", "Velocity" })
        {
            obj1.Setup(x => x.get_property(prop)).Returns(new Vector(It.IsAny<int>(), It.IsAny<int>()));
            obj2.Setup(x => x.get_property(prop)).Returns(new Vector(It.IsAny<int>(), It.IsAny<int>()));
        }

        var CheckCollisionStrategy = new Mock<IStartegy>();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SaceShips.CheckCollision", (object[] args) => CheckCollisionStrategy.Object.execute(args)).Execute();
        CheckCollisionStrategy.Setup(col => col.execute(It.IsAny<object[]>())).Returns(false).Verifiable();

        var checkCollision = new CollisionCheckCommand(obj1.Object, obj2.Object);
        checkCollision.action();

        CheckCollisionStrategy.Verify();
    }
}
