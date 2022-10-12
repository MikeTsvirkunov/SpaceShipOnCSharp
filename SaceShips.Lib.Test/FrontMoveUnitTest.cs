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
        var spaceship_w_speed_and_coords = new Mock<InterfaceObject>();
        spaceship_w_speed_and_coords.Setup(p => p.GetParam("speed")).Returns(new double[2] {It.IsAny<double>(), It.IsAny<double>()});
        spaceship_w_speed_and_coords.Setup(p => p.GetParam("coord")).Returns(new double[2] {It.IsAny<double>(), It.IsAny<double>()});
        spaceship_w_speed_and_coords.Setup(p => p.ParamExist("speed")).Returns(true);
        spaceship_w_speed_and_coords.Setup(p => p.ParamExist("coord")).Returns(true);
        FrontMove move_spaceship_w_speed_and_coords = new FrontMove(spaceship_w_speed_and_coords.Object);
        move_spaceship_w_speed_and_coords.move();
    }
    
    [Fact]
    public void Test_Movement_for_Object_wo_speed()
    {
        var spaceship_w_speed_and_coords = new Mock<InterfaceObject>();
        spaceship_w_speed_and_coords.Setup(p => p.GetParam("coord")).Returns(new double[2] {It.IsAny<double>(), It.IsAny<double>()});
        spaceship_w_speed_and_coords.Setup(p => p.ParamExist("speed")).Returns(false);
        spaceship_w_speed_and_coords.Setup(p => p.ParamExist("coord")).Returns(true);
        try{
            FrontMove move_spaceship_w_speed_and_coords = new FrontMove(spaceship_w_speed_and_coords.Object);
            move_spaceship_w_speed_and_coords.move();
        }
        catch(ArgumentException e){
            Assert.True(e.Message == "Object without speed");
        }
    }

    [Fact]
    public void Test_Movement_for_Object_wo_coord()
    {
        var spaceship_w_speed = new Mock<InterfaceObject>();
        spaceship_w_speed.Setup(p => p.GetParam("speed")).Returns(new double[2] {It.IsAny<double>(), It.IsAny<double>()});
        spaceship_w_speed.Setup(p => p.ParamExist("speed")).Returns(true);
        spaceship_w_speed.Setup(p => p.ParamExist("coord")).Returns(false);
        try{
            FrontMove move_spaceship_w_speed = new FrontMove(spaceship_w_speed.Object);
            move_spaceship_w_speed.move();
        }
        catch(ArgumentException e){
            Assert.True(e.Message == "Object without coord");
        }
    }

    [Fact]
    public void Test_Movement_for_Object_wo_All()
    {
        var spaceship_wo_all = new Mock<InterfaceObject>();
        spaceship_wo_all.Setup(p => p.ParamExist("speed")).Returns(false);
        spaceship_wo_all.Setup(p => p.ParamExist("coord")).Returns(false);
        try{
            FrontMove move_spaceship_wo_all = new FrontMove(spaceship_wo_all.Object);
            move_spaceship_wo_all.move();
        }
        catch(ArgumentException e){
            Assert.True(e.Message == "Object without coord" || e.Message == "Object without speed");
        }
    }

    // [Fact]
    // public void Test_Movement_for_Object_that_not_IUObject()
    // {
    //     var spaceship_w_speed_and_coords = new Mock<dynamic>();
    //     try{
    //         FrontMove move_spaceship_w_speed_and_coords = new FrontMove(spaceship_w_speed_and_coords.Object);
    //         move_spaceship_w_speed_and_coords.move();
    //     }
    //     catch(ArgumentException e){
    //         Assert.True(e.Message == "Object without coord" || e.Message == "Object without speed");
    //     }
    // }
}