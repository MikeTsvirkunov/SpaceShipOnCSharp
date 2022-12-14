namespace SaceShips.Lib.Classes;


public class Vector
{
    private dynamic[] coords;
    public int Size;

    public Vector(params dynamic[] args)
    {
        coords = args;
        Size = args.Length;
    }

    public Vector(int size)
    {
        coords = new dynamic[size];
        Size = size;
    }

    public override string? ToString()
    {
        string str_v = "Vector(";
        for (int i = 0; i < Size; i++)
        {
            str_v += coords[i].ToString();
            if (i != Size - 1) str_v += ", ";
            else str_v += ")";
        }
        return str_v;
    }

    public static Vector operator +(Vector a, Vector b)
    {
        if (a.Size != b.Size) throw new System.ArgumentException();
        for (int i = 0; i < a.Size; i++)
        {
            a.coords[i] += b.coords[i];
        }
        return a;
    }

    public static Vector operator -(Vector a, Vector b)
    {
        if (a.Size != b.Size) throw new System.ArgumentException();
        Vector x = new Vector(a.Size);
        for (int i = 0; i < a.Size; i++)
        {
            x.coords[i] = a.coords[i] - b.coords[i];
        }
        return x;
    }


    public static bool operator ==(Vector a, Vector b)
    {
        if (a.Size != b.Size) return false;
        for (int i = 0; i < a.Size; i++) if (a.coords[i] != b.coords[i]) return false;
        return true;
    }

    public static bool operator !=(Vector a, Vector b)
    {
        return !(a == b);
    }

    public static bool operator >(Vector a, Vector b)
    {
        for (int i = 0; i < Math.Min(a.Size, b.Size); i++)
        {
            if (a.coords[i] > b.coords[i]) return true;
        }
        if (a.Size > b.Size) return true;
        return false;
    }

    public static bool operator <(Vector a, Vector b)
    {
        return !(a > b | a == b);
    }

    public dynamic this[int i]
    {
        get => coords[i];
        set => coords[i] = value;
    }

    public override bool Equals(object? obj)
    {
        if (obj is not null)
        {
            Vector a = (Vector)obj;
            if (a.Size != this.Size) return false;
            for (int i = 0; i < a.Size; i++) if (a.coords[i] != this.coords[i]) return false;
            return true;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(coords, Size);
    }
}
