using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace SpaceShip
{
    public class MoveableObject : FrontMove
    {
        UIObject obj;

        public MoveableObject(ConcurrentDictionary<string, dynamic> p)
        {
            obj = new UIObject(p);
        }

        public void frontmove()
        {
            obj.SetParam("coord", new int[2] { obj.GetParam("speed").XLSpeed + obj.GetParam("coords")[0], obj.GetParam("speed").YLSpeed + obj.GetParam("coords")[1] });
        }
    }


    public class RotationObject : RotationMove
    {
        UIObject obj;

        public RotationObject(ConcurrentDictionary<string, dynamic> p)
        {
            obj = new UIObject(p);
        }

        public void rotation()
        {
            obj.SetParam("angle", obj.GetParam("angle") + obj.GetParam("angle_speed"));
            obj.GetParam("speed").Angle = obj.GetParam("angle");
        }
    }
}
