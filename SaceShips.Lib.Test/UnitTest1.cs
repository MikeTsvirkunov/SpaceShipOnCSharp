using Xunit;
using SaceShips.Lib;
using Moq;
using System.Collections.Generic;
using System.Collections.Concurrent;
namespace SaceShips.Lib.Test;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        var spaceship_w_speed_and_coords = new Mock<InterfaceObject>();
        int[] expectation_coords = new int[2] { 5, 8 };
        spaceship_w_speed_and_coords.Setup(p => p.GetAllParams()).Returns(It.IsAny<ConcurrentDictionary<string, dynamic>>);
        
    }
}