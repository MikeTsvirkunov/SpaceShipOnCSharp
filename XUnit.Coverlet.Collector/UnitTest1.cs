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

public class UnitTest1
{
    [Fact]
    public void Test_Movement_for_Full_Object()
    {
        var spaceship_w_speed_and_coord_parametrs = new Mock<IMoveable>();
        spaceship_w_speed_and_coord_parametrs.SetupProperty(p => p.coord, new Vector(12, 5));
        spaceship_w_speed_and_coord_parametrs.SetupProperty(p => p.frontSpeed, new Vector(-5, 2));
        new FrontMove(spaceship_w_speed_and_coord_parametrs.Object).action();
        var expected = new Vector(7, 7);
        Assert.Equal(expected[0], spaceship_w_speed_and_coord_parametrs.Object.coord[0]);
    }
}