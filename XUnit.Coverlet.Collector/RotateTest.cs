using Xunit;
using SaceShips.Lib.Classes;
using SaceShips.Lib.Interfaces;
using Moq;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System;
using System.Diagnostics;


public class RotateMoveTest
{
    [Fact]
    public void Test_Movement_for_Full_Object()
    {
        var spaceship_w_speed_and_angle_parametrs = new Mock<IRotateable>();
        spaceship_w_speed_and_angle_parametrs.SetupGet(p => p.angle).Returns(new Fraction(45, 1)).Verifiable();
        spaceship_w_speed_and_angle_parametrs.SetupGet(p => p.angleSpeed).Returns(new Fraction(90, 1)).Verifiable();
        new RotateMove(spaceship_w_speed_and_angle_parametrs.Object).action();
        spaceship_w_speed_and_angle_parametrs.VerifySet(m => m.angle = new Fraction(135, 1));
        spaceship_w_speed_and_angle_parametrs.Verify();
    }

    [Fact]
    public void Test_Movement_for_Object_without_angle()
    {
        var spaceship_wo_angle_parametr = new Mock<IRotateable>();
        spaceship_wo_angle_parametr.SetupProperty(p => p.angleSpeed, It.IsAny<Fraction>());
        Assert.Throws<System.NullReferenceException>(() => new RotateMove(spaceship_wo_angle_parametr.Object).action());
    }

    [Fact]
    public void Test_Movement_for_Object_without_angleSpeed()
    {
        var spaceship_wo_angleSpeed_parametr = new Mock<IRotateable>();
        spaceship_wo_angleSpeed_parametr.SetupProperty(p => p.angle, It.IsAny<Fraction>());
        Assert.Throws<System.NullReferenceException>(() => new RotateMove(spaceship_wo_angleSpeed_parametr.Object).action());
    }

    [Fact]
    public void Test_Movement_for_Object_without_All()
    {
        var spaceship_wo_all = new Mock<IRotateable>();
        Assert.Throws<System.NullReferenceException>(() => new RotateMove(spaceship_wo_all.Object).action());
    }
}
