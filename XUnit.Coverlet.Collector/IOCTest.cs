using Xunit;
using SaceShips.Lib.Classes;
using SaceShips.Lib.Interfaces;
using Moq;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System;
using System.Diagnostics;
using SaceShips.Lib.IOC_container;


public class IOCTest
{
    [Fact]
    public void Test_IOC_create()
    {
        var spaceship_w_speed_and_angle_parametrs = new Mock<IIOC>();
        // spaceship_w_speed_and_angle_parametrs.SetupGet(p => p.angle).Returns(new Fraction(45, 1)).Verifiable();
        // spaceship_w_speed_and_angle_parametrs.SetupGet(p => p.angleSpeed).Returns(new Fraction(90, 1)).Verifiable();
        // new RotateMove(spaceship_w_speed_and_angle_parametrs.Object).action();
        // spaceship_w_speed_and_angle_parametrs.VerifySet(m => m.angle = new Fraction(135, 1));
        // spaceship_w_speed_and_angle_parametrs.Verify();
    }
}