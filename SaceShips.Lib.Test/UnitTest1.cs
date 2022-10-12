using Xunit;
using SaceShips.Lib;
using Moq;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System;
using System.Diagnostics;
namespace SaceShips.Lib.Test;

public class UnitTest1
{
    [Fact]
    public void Test_InterfaceObject()
    {
        var spaceobj= new Mock<InterfaceObject>();
        spaceobj.Setup(p => p.GetAllParams()).Returns(It.IsAny<ConcurrentDictionary<string, dynamic>>());
        spaceobj.Setup(p => p.GetParam(It.IsAny<string>())).Returns(It.IsAny<dynamic>());
        spaceobj.Setup(p => p.ParamExist(It.IsAny<string>())).Returns(It.IsAny<dynamic>());
    }

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
    
    // [Fact]
    // public void Test_Movement_for_Object_wo_speed()
    // {
    //     var spaceship_w_speed_and_coords = new Mock<InterfaceObject>();
    //     spaceship_w_speed_and_coords.Setup(p => p.GetParam("coord")).Returns(new double[2] {It.IsAny<double>(), It.IsAny<double>()});
    //     spaceship_w_speed_and_coords.Setup(p => p.ParamExist("speed")).Returns(false);
    //     spaceship_w_speed_and_coords.Setup(p => p.ParamExist("coord")).Returns(true);
    //     try{
    //         FrontMove move_spaceship_w_speed_and_coords = new FrontMove(spaceship_w_speed_and_coords.Object);
    //         move_spaceship_w_speed_and_coords.move();
    //         // Assert.Fail();
    //     }
    //     catch(ArgumentException e){
    //         Assert.AreEqual(e, "Object without speed");
    //     }
    // }
}