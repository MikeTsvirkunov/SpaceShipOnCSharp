using SaceShips.Lib.Interfaces;
namespace SaceShips.Lib.Classes;

public class Fraction
{
    private int up;
    public int down; 

    public Fraction(int a, int b)
    {
        up = a;
        down = b;
    }

    public Fraction(double num, double eps = 0.0000001)
    {
        int a = 1; 
        int b = 1;
        int mn = 2;
        int iter = 0;
        up = a; 
        down = b;
        double c = 1;
        while ((num - c) < 0) {
            b++;
            c = (double)a / b;
        }

        if ((num - c) < eps){
            up = a; down = b;
            return;
        }
        b--;
        c = (double)a / b;

        if ((num - c) > -eps){
            up = a; down = b;
            return;
        }

        while (iter < 20000){
            int cc = a*mn;
            int zz = b*mn;
            iter++;
            while ((num-c) < 0) {
                zz++;
                c = (double)cc / zz;
            } 
            if ((num-c) < eps)
            {
                up = cc; down = zz;
                return;
            }
            zz--;
            c = (double)cc / zz;
            if ((num - c) > -eps)
            {
                up = cc; down = zz;
                return;
            }
            mn++;
        }
    }

    public static Fraction operator +(Fraction a, Fraction b)
    {
        int n_up = a.up * b.down + a.down * b.up;
        int n_down = a.down * b.down;
        return new Fraction(n_up, n_down);
    }

    public static Fraction operator -(Fraction a, Fraction b)
    {
        int n_up = a.up * b.down - a.down * b.up;
        int n_down = a.down * b.down;
        return new Fraction(n_up, n_down);
    }

    public static Fraction operator *(Fraction a, Fraction b)
    {
        return new Fraction(a.up * b.up, a.down * b.down);
    }

    public static bool operator ==(Fraction a, Fraction b)
    {
        return a.up * b.down == b.up * a. down;
    }

    public static bool operator !=(Fraction a, Fraction b)
    {
        return a.up * b.down != b.up * a. down;
    }

    public override bool Equals(object? obj)
    {
        if (obj is Fraction objectType) return objectType.up == this.up & objectType.down == this.down;
        return false;
    }

    public override int GetHashCode()
    {
        return Tuple.Create(up, down).GetHashCode(); ;
    }

    public int GetUp(){
        return up;
    }

    public int GetDown(){
        return down;
    }
}
