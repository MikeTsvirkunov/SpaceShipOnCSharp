using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace SpaceShip
{
    class MoveableObject : FrontMove
    {
        UIObject obj;

        public MoveableObject(ConcurrentDictionary<string, dynamic> p)
        {
            obj = new UIObject(p);
        }

        public void frontmove()
        {
            obj.SetParam("coord", obj.GetParam("speed") + obj.GetParam("coords"));
        }
    }


    class RotationObject : RotationMove
    {
        UIObject obj;

        public RotationObject(ConcurrentDictionary<string, dynamic> p)
        {
            obj = new UIObject(p);
        }

        public void rotation()
        {
            obj.SetParam("angle", obj.GetParam("angle") + obj.GetParam("angle_speed"));
            obj.SetParam("speed", obj.GetParam("speed").Angle(obj.GetParam("angle")));
        }
    }
}
