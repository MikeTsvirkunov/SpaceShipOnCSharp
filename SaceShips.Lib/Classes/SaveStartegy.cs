using SaceShips.Lib.Interfaces;
namespace SaceShips.Lib.Classes;
using System.Collections.Generic;
using System.Linq;
using System;

public class SaveStrategy : IStartegy
{
    private Tree ErrorRegister;
    private IStartegy strategy;
    public SaveStrategy(IStartegy strategy, Tree register)
    {
        this.ErrorRegister = register;
        this.strategy = strategy;
        
    }
    public object execute(params object[] args)
    {
        try{
            return Hwdtech.IoC.Resolve<object>("SpaceShip.Lib.Strategy.KeyPairStringObject", "success", this.strategy.execute(args));
        }
        catch(Exception ex){
            return Hwdtech.IoC.Resolve<object>("SpaceShip.Lib.Strategy.KeyPairStringObject", "error", ErrorRegister.get_solution(Hwdtech.IoC.Resolve<List<object>>("SpaceShip.Lib.Strategy.ListObject", (object)strategy, (object)ex)));
            // return (object)(new KeyValuePair<string, object>("error", (object)ErrorRegister.get_solution(new List<object>() { (object)strategy, (object)ex })));
        }
    }
}
