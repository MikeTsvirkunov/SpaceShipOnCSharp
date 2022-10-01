using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Concurrent;
using SpaceShip;

namespace SpaceShipTest
{
    [TestClass]
    public class TestMovableObj
    {
        [TestMethod]
        public void Obj_w_speed_and_coord()
        {
            int[] expectation_coords = new int[2] { 5, 8 };
            ConcurrentDictionary<string, dynamic> spaceship_parametrs = new ConcurrentDictionary<string, dynamic>();
            spaceship_parametrs.TryAdd("coords", new int[2] { 1, 2 });
            /*spaceship_parametrs.TryAdd("speed", new Speed(xls: 12, yls: 5));*/
            /*MoveableObject object_w_speed_and_coords = new MoveableObject(spaceship_parametrs);*/
        }
    }
}
