
using System.Collections.Generic;

namespace SaceShips.Lib.IOC_container;
public interface IIOC
{
    IDictionary<string, IStrategy>  store {get; set;}
    
    T Execute<T>(string key, params object[] args);
}