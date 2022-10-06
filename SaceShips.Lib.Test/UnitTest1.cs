using Xunit;
using SaceShips.Lib;
using Moq;

using System.Collections.Generic;

namespace SaceShips.Lib.Test;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        Mock<UIObject> spaceship_w_speed_and_coords = new Mock<UIObject>();
        int[] expectation_coords = new int[2] { 5, 8 };
            
        //UIObject spaceship_w_speed_and_coords = new UIObject(new Dictionary<string, dynamic> { { "coord", new double[2] { 12, 5 } }, { "speed", new double[2] { -7, 3 } } });

        //FrontMove x = new FrontMove(spaceship_w_speed_and_coords);
        //x.move();

        //Assert.AreEqual(expectation_coords[0], spaceship_w_speed_and_coords.GetParam("coord")[0], "Coord fell short of expectations by X");
        //Assert.AreEqual(expectation_coords[1], spaceship_w_speed_and_coords.GetParam("coord")[1], "Coord fell short of expectations by Y");
    }
}