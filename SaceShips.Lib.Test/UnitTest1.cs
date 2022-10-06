using Xunit;
using SaceShips.Lib;
using Moq;
using System.Collections.Generic;
using System.Collections.Concurrent;
namespace SaceShips.Lib.Test;

public class UnitTest1
{
    [Fact]
    public void Test_InterfaceObject()
    {
        var spaceship_w_speed_and_coords = new Mock<InterfaceObject>();
        spaceship_w_speed_and_coords.Setup(p => p.GetAllParams()).Returns(It.IsAny<ConcurrentDictionary<string, dynamic>>());
        spaceship_w_speed_and_coords.Setup(p => p.GetParam(It.IsAny<string>())).Returns(It.IsAny<dynamic>());
        spaceship_w_speed_and_coords.Setup(p => p.ParamExist(It.IsAny<string>())).Returns(It.IsAny<dynamic>());
    }

    [Fact]
    public void Test_Movement()
    {
        var spaceship_w_speed_and_coords = new Mock<Movement>();
    }
}