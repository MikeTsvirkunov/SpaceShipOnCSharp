using Xunit;
// using Moq;
using Xunit;
using SaceShips.Lib.Classes;
using SaceShips.Lib.Interfaces;
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
        spaceship_w_speed_and_coord_parametrs.SetupGet(p => p.coord).Returns(new Vector(12, 5)).Verifiable();
        spaceship_w_speed_and_coord_parametrs.SetupGet(p => p.frontSpeed).Returns(new Vector(-5, 2)).Verifiable();
        new FrontMove(spaceship_w_speed_and_coord_parametrs.Object).action();
        spaceship_w_speed_and_coord_parametrs.VerifySet(m => m.coord = new Vector(7, 7));
        spaceship_w_speed_and_coord_parametrs.Verify();
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
