using SaceShips.Lib.Interfaces;
namespace SaceShips.Lib.Classes;
using System.Collections.Generic;
using System.Linq;

public class Iterator
{
    public object position { get; protected set; }
    object step_function;
    public Iterator(object start, object step_function){
        this.position = start;
        this.step_function = step_function;
    }
    public object next(){

        this.position = Hwdtech.IoC.Resolve<System.Object>("SpaceShip.Lib.Iterator.Step", this.step_function, this.position);
        return this.position;
    }
}
