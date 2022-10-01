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
        private UIObject obj;

        public const string SpeedExistError = "Speed doesn't exist";
        public const string CoordExistError = "Coord doesn't exist";

        public MoveableObject(Dictionary<string, dynamic> p) => obj = new UIObject(p);

        public dynamic GetParam(string key) { return obj.GetParam(key); }
        public void SetParam(string key, dynamic value) { obj.GetParam(key); }
        public bool ParamExist(string key) { return obj.ParamExist(key); }

        public void frontmove()
        {
            if (!obj.ParamExist("speed")) throw new ArgumentException(SpeedExistError);
            if (!obj.ParamExist("coord")) throw new ArgumentException(CoordExistError);
            obj.SetParam("coord", new int[2] { obj.GetParam("speed")[0] + obj.GetParam("coord")[0], obj.GetParam("speed")[1] + obj.GetParam("coord")[1] });
        }
    }


    public class RotationObject : RotationMove
    {
        private UIObject obj;

        public RotationObject(Dictionary<string, dynamic> p)
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
