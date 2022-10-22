using System.Collections.Concurrent;
using System.Collections.Generic;


namespace SaceShips.Lib;


public class MovableObject: IMoveable{
    public Vector frontSpeed {get; set;}
    public Vector coord {get; set;}
    public MovableObject(Vector fs, Vector c){
        coord = c;
        frontSpeed = fs;
    }
}


public class RotateableObject: IRotateable{
    public int angleSpeed {get; set;}
    public int angle {get; set;}
    public RotateableObject(int s, int a){
        angleSpeed = s;
        angle = a;
    }
}

