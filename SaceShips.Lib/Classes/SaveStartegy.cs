using SaceShips.Lib.Interfaces;
namespace SaceShips.Lib.Classes;
using System.Collections.Generic;
using System.Linq;
using System;

public class SaveStrategy : IStartegy
{
    private Tree ErrorRegister;
    private IStartegy strategy;
    public SaveStrategy(object strategy, object register)
    {
        this.ErrorRegister = (Tree)register;
        this.strategy = (IStartegy)strategy;
        
    }
    public object execute(params object[] args)
    {
        try{
            return (object)this.strategy.execute(args);
            // return (object) KeyValuePair<string, object>{, this.strategy.execute(args)};
        }
        catch(Exception ex){
            return (object)ErrorRegister.get_solution(new List<object>() { (object)strategy, (object)ex });
        }
    }
}
