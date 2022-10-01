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
        public const string ParamExistError = "Parameter doesn't exist";

        public UIObject(Dictionary<string, dynamic> p) {
            this.parametrs = new ConcurrentDictionary<string, dynamic>(p);
        }

        public ConcurrentDictionary<string, dynamic> GetAllParams() { return parametrs; }
        public dynamic GetParam(string key) { return parametrs[key]; }
        public void SetParam(string key, dynamic value) { parametrs[key] = value; }
        public bool ParamExist(string key) { return this.parametrs.ContainsKey(key); }
    }


    /*  public class UIObject: ConcurrentDictionary<string, dynamic>
      {

      }*/

}
