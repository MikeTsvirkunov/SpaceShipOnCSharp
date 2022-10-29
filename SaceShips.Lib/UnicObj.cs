using System.Collections.Concurrent;
using System.Collections.Generic;


namespace SaceShips.Lib;


public class MoveableObject: IMoveable{
    public Vector frontSpeed {get; set;}
    public Vector coord {get; set;}
    public MoveableObject(Vector fs, Vector c){
        coord = c;
        frontSpeed = fs;
    }
}


public class RotateableObject: IRotateable{
    public Fraction angleSpeed {get; set;}
    public Fraction angle {get; set;}
    public RotateableObject(Fraction s, Fraction a){
        angleSpeed = s;
        angle = a;
    }
}

// reportgenerator -reports:"./TestResults/7134df18-0228-4551-9313-db1d475a377a/coverage.cobertura.xml" -targetdir:"coveragereport" -reporttypes:Html
