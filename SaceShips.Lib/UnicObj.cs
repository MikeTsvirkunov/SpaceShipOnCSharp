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

// reportgenerator -reports:"./TestResults/fbafb7b5-bead-499c-926b-142996deb039/coverage.cobertura.xml" -targetdir:"coveragereport" -reporttypes:Html
