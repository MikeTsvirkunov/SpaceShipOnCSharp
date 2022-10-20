using Xunit;
using SaceShips.Lib;
using Moq;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System;
using System.Diagnostics;
namespace SaceShips.Lib.Test;

public class FrontMoveUnitTest
{
    [Fact]
    public void Test_Movement_for_Full_Object()
    {
        var parametrix = new ConcurrentDictionary<string, dynamic>(new Dictionary<string, dynamic> { { "coord", new double[2] { 12, 5 } }, { "speed", new double[2] { -7, 3 } } });

        var spaceship_w_speed_and_coord_parametrs = new Mock<InterfaceObject>();
        // spaceship_w_speed_and_coord_parametrs.SetupProperty(x=>x.parametrs, parametrix);
        // spaceship_w_speed_and_coord_parametrs.SetupGet(x=>x.parametrs).Returns(parametrix);
        spaceship_w_speed_and_coord_parametrs.Setup(p => p.GetParam("old_coord")).Returns(new double[2] { 12, 5 });
        spaceship_w_speed_and_coord_parametrs.Setup(p => p.GetParam("speed")).Returns(new double[2] { -7, 3 });
        spaceship_w_speed_and_coord_parametrs.Setup(p => p.ParamExist("speed")).Returns(true);
        spaceship_w_speed_and_coord_parametrs.Setup(p => p.ParamExist("coord")).Returns(true);
        var move_spaceship_w_speed_and_coord_parametrs = new Mock<ICommand>();
        move_spaceship_w_speed_and_coord_parametrs.Setup(p => p.action()).Callback(() =>
        {
            spaceship_w_speed_and_coord_parametrs.Setup(p => p.GetParam("new_coord"))
            .Returns(new double[2] { spaceship_w_speed_and_coord_parametrs.Object.GetParam("old_coord")[0] + spaceship_w_speed_and_coord_parametrs.Object.GetParam("speed")[0], spaceship_w_speed_and_coord_parametrs.Object.GetParam("old_coord")[1] + spaceship_w_speed_and_coord_parametrs.Object.GetParam("speed")[1]});
        });

        move_spaceship_w_speed_and_coord_parametrs.Object.action();
        double[] expected = new double[2] { 5, 8 };
        double[] real = spaceship_w_speed_and_coord_parametrs.Object.GetParam("new_coord");
        Assert.Equal(expected[0], real[0]);
        Assert.Equal(expected[1], real[1]);
    }

    [Fact]
    public void Test_Movement_for_Object_wo_speed()
    {
        var spaceship_w_speed_and_parametrs = new Mock<InterfaceObject>();

        spaceship_w_speed_and_parametrs.Setup(p => p.GetParam("coord")).Returns(new dynamic[2] { It.IsAny<dynamic>(), It.IsAny<dynamic>() });
        spaceship_w_speed_and_parametrs.Setup(p => p.ParamExist("speed")).Returns(false);
        spaceship_w_speed_and_parametrs.Setup(p => p.ParamExist("coord")).Returns(true);
        try
        {
            FrontMove move_spaceship_w_speed_and_parametrs = new FrontMove(spaceship_w_speed_and_parametrs.Object);
            move_spaceship_w_speed_and_parametrs.action();
        }
        catch (ArgumentException e)
        {
            Assert.True(e.Message == "Object without speed");
        }
    }

    [Fact]
    public void Test_Movement_for_Object_wo_coord()
    {
        var spaceship_w_speed = new Mock<InterfaceObject>();
        spaceship_w_speed.Setup(p => p.GetParam("speed")).Returns(new dynamic[2] { It.IsAny<dynamic>(), It.IsAny<dynamic>() });
        spaceship_w_speed.Setup(p => p.ParamExist("speed")).Returns(true);
        spaceship_w_speed.Setup(p => p.ParamExist("coord")).Returns(false);
        try
        {
            FrontMove move_spaceship_w_speed = new FrontMove(spaceship_w_speed.Object);
            move_spaceship_w_speed.action();
        }
        catch (ArgumentException e)
        {
            Assert.True(e.Message == "Object without coord");
        }
    }

    [Fact]
    public void Test_Movement_for_Object_wo_All()
    {
        var spaceship_wo_all = new Mock<InterfaceObject>();
        spaceship_wo_all.Setup(p => p.ParamExist("speed")).Returns(false);
        spaceship_wo_all.Setup(p => p.ParamExist("coord")).Returns(false);
        try
        {
            FrontMove move_spaceship_wo_all = new FrontMove(spaceship_wo_all.Object);
            move_spaceship_wo_all.action();
        }
        catch (ArgumentException e)
        {
            Assert.True(e.Message == "Object without coord" || e.Message == "Object without speed");
        }
    }

    // [Fact]
    // public void Test_Movement_for_Object_that_not_IUObject()
    // {
    //     var spaceship_w_speed_and_parametrs = new Mock<dynamic>();
    //     try{
    //         FrontMove move_spaceship_w_speed_and_parametrs = new FrontMove(spaceship_w_speed_and_parametrs.Object);
    //         move_spaceship_w_speed_and_parametrs.move();
    //     }
    //     catch(ArgumentException e){
    //         Assert.True(e.Message == "Object without coord" || e.Message == "Object without speed");
    //     }
    // }
}