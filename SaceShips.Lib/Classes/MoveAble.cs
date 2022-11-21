using System.Collections.Concurrent;
using System.Collections.Generic;
using SaceShips.Lib.Interfaces;
namespace SaceShips.Lib.Classes;


public class MoveableObject : IMoveable
{
    public Vector frontSpeed { get; set; }
    public Vector coord { get; set; }
    public MoveableObject(Vector fs, Vector c)
    {
        coord = c;
        frontSpeed = fs;
    }
}

