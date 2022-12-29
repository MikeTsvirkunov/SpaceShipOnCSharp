using SaceShips.Lib.Classes;
namespace SaceShips.Lib.Interfaces;
using System.Collections.Generic;

public interface ISource
{
    Dictionary<object, object?> nexts { get; set; }
    object? step_forward(object args);
}
