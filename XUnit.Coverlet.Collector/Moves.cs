using Xunit;
// using Moq;
using Xunit;
using SaceShips.Lib;
using Moq;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System;
using System.Diagnostics;

namespace XUnit.Coverlet.Collector;

public class FrontMoveTest
{
    [Fact]
    public void Test_Movement_for_Full_Object()
    {
        var spaceship_w_speed_and_coord_parametrs = new Mock<IMoveable>();
        spaceship_w_speed_and_coord_parametrs.SetupProperty(p => p.coord, new Vector(12, 5));
        spaceship_w_speed_and_coord_parametrs.SetupProperty(p => p.frontSpeed, new Vector(-5, 2));
        new FrontMove(spaceship_w_speed_and_coord_parametrs.Object).action();
        var expected = new Vector(7, 7);
        Assert.Equal(spaceship_w_speed_and_coord_parametrs.Object.coord[0], expected[0]);
        Assert.Equal(spaceship_w_speed_and_coord_parametrs.Object.coord[1], expected[1]);
        Assert.True(spaceship_w_speed_and_coord_parametrs.Object.coord == expected);
    }

    [Fact]
    public void Test_Movement_for_Object_without_frontSpeed()
    {
        var spaceship_wo_speed_parametrs = new Mock<IMoveable>();
        spaceship_wo_speed_parametrs.SetupProperty(p => p.coord, new Vector(12, 5));
        try
        {
            new FrontMove(spaceship_wo_speed_parametrs.Object).action();
            Debug.Fail("Unknown Option");
        }
        catch (System.NullReferenceException)
        {

        }
    }

    [Fact]
    public void Test_Movement_for_Object_without_coord()
    {
        var spaceship_wo_coord_parametr = new Mock<IMoveable>();
        spaceship_wo_coord_parametr.SetupProperty(p => p.frontSpeed, new Vector(12, 5));
        try
        {
            new FrontMove(spaceship_wo_coord_parametr.Object).action();
            Debug.Fail("Unknown Option");
        }
        catch (System.NullReferenceException)
        {

        }
    }

    [Fact]
    public void Test_Movement_for_Object_without_All()
    {
        var spaceship_wo_all = new Mock<IMoveable>();
        try
        {
            new FrontMove(spaceship_wo_all.Object).action();
            Debug.Fail("Unknown Option");
        }
        catch (System.NullReferenceException)
        {

        }
    }
}


public class RotateMoveTest
{
    [Fact]
    public void Test_Movement_for_Full_Object()
    {
        var spaceship_w_speed_and_angle_parametrs = new Mock<IRotateable>();
        Fraction angle = new Fraction(5, 1);
        Fraction angleSpeed = new Fraction(-5, 1);
        spaceship_w_speed_and_angle_parametrs.SetupProperty(p => p.angle, angle);
        spaceship_w_speed_and_angle_parametrs.SetupProperty(p => p.angleSpeed, angleSpeed);
        new RotateMove(spaceship_w_speed_and_angle_parametrs.Object).action();
        Fraction expected = new Fraction(0, 1);
        Assert.True(spaceship_w_speed_and_angle_parametrs.Object.angle == expected);
    }

    [Fact]
    public void Test_Movement_for_Object_without_angle()
    {
        var spaceship_wo_angle_parametr = new Mock<IRotateable>();
        spaceship_wo_angle_parametr.SetupProperty(p => p.angleSpeed, It.IsAny<Fraction>());
        try
        {
            new RotateMove(spaceship_wo_angle_parametr.Object).action();
            Debug.Fail("Unknown Option");
        }
        catch (System.NullReferenceException)
        {

        }
    }

    [Fact]
    public void Test_Movement_for_Object_without_angleSpeed()
    {
        var spaceship_wo_angleSpeed_parametr = new Mock<IRotateable>();
        spaceship_wo_angleSpeed_parametr.SetupProperty(p => p.angle, It.IsAny<Fraction>());
        try
        {
            new RotateMove(spaceship_wo_angleSpeed_parametr.Object).action();
            Debug.Fail("Unknown Option");
        }
        catch (System.NullReferenceException)
        {

        }
    }

    [Fact]
    public void Test_Movement_for_Object_without_All()
    {
        var spaceship_wo_all = new Mock<IRotateable>();
        try
        {
            new RotateMove(spaceship_wo_all.Object).action();
            Debug.Fail("Unknown Option");
        }
        catch (System.NullReferenceException)
        {

        }
    }
}