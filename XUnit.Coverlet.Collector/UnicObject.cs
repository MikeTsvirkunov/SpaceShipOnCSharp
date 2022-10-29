using Xunit;
using SaceShips.Lib;
using Moq;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System;
using System.Diagnostics;

namespace XUnit.Coverlet.Collector;

public class TestUnicObj
{
    [Fact]
    public void Test_Moveable_obj()
    {
        Vector coord = new Vector(It.IsAny<double>(), It.IsAny<double>());
        Vector speed = new Vector(It.IsAny<double>(), It.IsAny<double>());
        var mv_obj = new MoveableObject(coord, speed);
        Assert.True(mv_obj.coord is Vector);
        Assert.True(mv_obj.frontSpeed is Vector);
    }

    [Fact]
    public void Test_Rotateable_obj()
    {
        var mv_obj = new RotateableObject(new Fraction(It.IsAny<int>(), It.IsAny<int>()), new Fraction(It.IsAny<int>(), It.IsAny<int>()));
        Assert.True(mv_obj.angle.GetType() == typeof(Fraction));
        Assert.True(mv_obj.angleSpeed.GetType() == typeof(Fraction));
    }
}