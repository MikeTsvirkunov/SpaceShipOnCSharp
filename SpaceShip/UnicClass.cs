using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceShip
{
    public class UIObject
    {
        // Dictionary with obj params
        private ConcurrentDictionary<string, dynamic> parametrs;

        public UIObject(ConcurrentDictionary<string, dynamic> p) => parametrs = p;

        public ConcurrentDictionary<string, dynamic> GetAllParams() { return parametrs; }
        public dynamic GetParam(string key) { return parametrs[key]; }
        public void SetParam(string key, dynamic value) { parametrs[key] = value; }
    }


    public class Speed
    {
        private int x_line_speed, y_line_speed, normalizer, angle;

        public Speed(int xls, int yls, int n=1, int a=0)
        {
            x_line_speed = xls;
            y_line_speed = yls;
            normalizer = n;
            angle = a;
        }

        public int XLSpeed
        {
            get { return x_line_speed; }
            set { x_line_speed = value; }
        }

        public int YLSpeed
        {
            get { return y_line_speed; }
            set { y_line_speed = value; }
        }

        public int Norm
        {
            get { return normalizer; }
            set { normalizer = value; }
        }

        public int Angle
        {
            get { return angle; }
            set { angle = value; }
        }

        public double RealSpeed
        {
            get 
            {
                double x = (double)x_line_speed;
                double y = (double)y_line_speed;
                return normalizer * Math.Sqrt(Math.Pow(x * Math.Cos(Math.PI * angle / 180) - (y * Math.Sin(Math.PI * angle / 180)), 2) + 
                                              Math.Pow(y * Math.Cos(Math.PI * angle / 180) - x * Math.Sin(Math.PI * angle / 180), 2)); 
            }
        }

        
    }


    /*  public class UIObject: ConcurrentDictionary<string, dynamic>
      {

      }*/

}
