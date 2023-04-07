using SaceShips.Lib.Interfaces;
namespace SaceShips.Lib.Classes;
using System.Collections.Generic;

public interface IMoveCommandEndable
{
    IUObject uobject { get; }

    IEnumerable<string> properties { get; }

}
