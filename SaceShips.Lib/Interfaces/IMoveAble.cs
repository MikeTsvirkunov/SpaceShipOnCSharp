using System.Collections.Concurrent;
using SaceShips.Lib.Classes;
namespace SaceShips.Lib.Interfaces;

public interface IMoveable
{
    Vector frontSpeed { get; set; }
    Vector coord { get; set; }
}