using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Concurrent;
using System.Collections;
using System.Collections.Generic;
using SpaceShip;

namespace SpaceShipTest
{
    [TestClass]
    public class TestMovableObj
    {
        [TestMethod]
        public void obj_with_speed_and_coord()
        {
            int[] expectation_coords = new int[2] { 5, 8 };
            
            MoveableObject spaceship_w_speed_and_coords = new MoveableObject(new Dictionary<string, dynamic> { { "coord", new int[2] { 12, 5 } }, { "speed", new int[2] { -7, 3 } } });

            spaceship_w_speed_and_coords.frontmove();

            Assert.AreEqual(expectation_coords[0], spaceship_w_speed_and_coords.GetParam("coord")[0], "Coord fell short of expectations by X");
            Assert.AreEqual(expectation_coords[1], spaceship_w_speed_and_coords.GetParam("coord")[1], "Coord fell short of expectations by Y");
        }

        [TestMethod]
        public void obj_with_speed_and_without_coord()
        {
            MoveableObject spaceship_w_speed_and_coords = new MoveableObject(new Dictionary<string, dynamic> { { "speed", new int[2] { -7, 3 } } });

            try
            {
                spaceship_w_speed_and_coords.frontmove();
            }
            catch (System.ArgumentException e)
            {
                StringAssert.Contains(e.Message, MoveableObject.CoordExistError);
                return;
            }
            Assert.Fail("The expected exception was not thrown.");
        }

        [TestMethod]
        public void obj_with_coord_and_without_speed()
        {
            MoveableObject spaceship_w_speed_and_coords = new MoveableObject(new Dictionary<string, dynamic> { { "coord", new int[2] { -7, 3 } } });

            try
            {
                spaceship_w_speed_and_coords.frontmove();
            }
            catch (System.ArgumentException e)
            {
                StringAssert.Contains(e.Message, MoveableObject.SpeedExistError);
                return;
            }
            Assert.Fail("The expected exception was not thrown.");
        }
    }
}
