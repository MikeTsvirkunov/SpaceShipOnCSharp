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
    public void Exe_of_StartCommand()
    {
        // Added std Functions to Scope
        new Hwdtech.Ioc.InitScopeBasedIoCImplementationCommand().Execute();

        var spsh_obj = new Dictionary<string, object>();
        var UObject = new Mock<IUObject>();
        // UObject.Setup(p => p.set_property(It.IsAny<string>(), It.IsAny<object>())).Callback((string key, object value) => {spsh_obj[key] = value;});
        UObject.Setup(p => p.get_property("angle")).Returns(new Fraction(90, 1));

        var spaceship_w_speed_and_angle_parametrs = new Mock<IRotateable>();

        // spaceship_w_speed_and_angle_parametrs.SetupSet(p => p.angleSpeed = It.IsAny<Fraction>()).Callback<Fraction>(value => spsh_obj["angleSpeed"] = value);
        // spaceship_w_speed_and_angle_parametrs.SetupSet(p => p.angle = It.IsAny<Fraction>()).Callback<Fraction>(value => spsh_obj["angle"] = value);
        // spaceship_w_speed_and_angle_parametrs.Object.angle = new Fraction(90, 1);
        // spaceship_w_speed_and_angle_parametrs.Object.angleSpeed = new Fraction(45, 1);
        // spaceship_w_speed_and_angle_parametrs.SetupGet(p => p.angle).Returns((Fraction)spsh_obj["angle"]).Verifiable();
        // spaceship_w_speed_and_angle_parametrs.SetupGet(p => p.angleSpeed).Returns((Fraction)spsh_obj["angleSpeed"]).Verifiable();
        // new RotateMove(spaceship_w_speed_and_angle_parametrs.Object).action();
        // spaceship_w_speed_and_angle_parametrs.VerifySet(m => m.angle = new Fraction(135, 1));
        // spaceship_w_speed_and_angle_parametrs.Verify();
        ICollection.Resolve<ICommand>("IoC.Register",)
    }
}