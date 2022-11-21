using SaceShips.Lib.Classes;
namespace SaceShips.Lib.Interfaces;



public class RotateableObject : IRotateable
{
    public Fraction angleSpeed { get; set; }
    public Fraction angle { get; set; }
    public RotateableObject(Fraction s, Fraction a)
    {
        angleSpeed = s;
        angle = a;
    }
}