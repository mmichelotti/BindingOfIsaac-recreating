using System;
public struct Vector4Int : IEquatable<Vector4Int>, IFormattable
{
    #region variables
    public int x;
    public int y;
    public int z;
    public int w;
    #endregion

    #region constructors
    public Vector4Int(int x, int y, int z, int w)
    {
        this.x = x;
        this.y = y;
        this.z = z;
        this.w = w;
    }
    public Vector4Int(int n)
    {
        this.x = n;
        this.y = n;
        this.z = n;
        this.w = n;
    }
    #endregion

    #region properties
    public static Vector4Int Zero => new (0);
    public static Vector4Int Forward => new(0, 0, 1, 0);
    public static Vector4Int Backward => new(0, 0, -1, 0);
    public static Vector4Int Right => new(1, 0, 0, 0);
    public static Vector4Int Left => new(-1, 0, 0, 0);
    public static Vector4Int Up => new(0, 1, 0, 0);
    public static Vector4Int Down => new(0, -1, 0, 0);
    public float Magnitude => (float)Math.Sqrt(this&this);  //ahhaha porca madonna che schifo illeggibile
    #endregion

    #region interfaces
    public readonly bool Equals(Vector4Int other)
        => this.x == other.x 
        && this.y == other.y 
        && this.z == other.z 
        && this.w == other.w;

    public override readonly bool Equals(object obj) => 
        obj is Vector4Int other && Equals(other);

    public readonly override int GetHashCode() =>
         HashCode.Combine(this.x, this.y, this.z, this.w);

    public string ToString(string format)
    {
        return ToString(format, null);
    }

    //? format provider?
    public string ToString(string format, IFormatProvider formatProvider)
    {
        return String.Format
            ("({0}, {1}, {2}, {3})", 
            this.x.ToString(format, formatProvider), 
            this.y.ToString(format, formatProvider),
            this.z.ToString(format, formatProvider),
            this.w.ToString(format, formatProvider)
            );
    }
    #endregion

    #region methods and operators
    public static float Dot(Vector4Int a, Vector4Int b) => a.x * b.x + a.y * b.y + a.z * b.z + a.w * b.w;

    public static Vector4Int operator +(Vector4Int a, Vector4Int b) => new (a.x + b.x, a.y + b.y, a.z + b.z, a.w + b.w);
    public static Vector4Int operator +(Vector4Int a, int n) => new(a.x + n, a.y + n, a.z + n, a.w + n);
    public static Vector4Int operator +(int n, Vector4Int a) => new(a.x + n, a.y + n, a.z + n, a.w + n);

    public static Vector4Int operator *(Vector4Int a, Vector4Int b) => new(a.x * b.x, a.y * b.y, a.z * b.z, a.w * b.w);
    public static Vector4Int operator *(Vector4Int a, int n) => new(a.x * n, a.y * n, a.z * n, a.w * n);
    public static Vector4Int operator *(int n, Vector4Int a) => new(a.x * n, a.y * n, a.z * n, a.w * n);

    public static Vector4Int operator /(Vector4Int a, Vector4Int b) => new(a.x / b.x, a.y / b.y, a.z / b.z, a.w / b.w);
    public static Vector4Int operator /(Vector4Int a, int n) => new(a.x / n, a.y / n, a.z / n, a.w / n);
    public static Vector4Int operator /(int n, Vector4Int a) => new(a.x / n, a.y / n, a.z / n, a.w / n);

    public static bool operator ==(Vector4Int a, Vector4Int b) => a.Equals(b);
    public static bool operator !=(Vector4Int a, Vector4Int b) => !a.Equals(b);

    public static bool operator >(Vector4Int a, Vector4Int b) => a.Magnitude > b.Magnitude;
    public static bool operator <(Vector4Int a, Vector4Int b) => a.Magnitude < b.Magnitude;
    public static bool operator >=(Vector4Int a, Vector4Int b) => a.Magnitude >= b.Magnitude;
    public static bool operator <=(Vector4Int a, Vector4Int b) => a.Magnitude <= b.Magnitude;

    public static Vector4Int operator ++(Vector4Int a) => new(a.x + 1, a.y + 1, a.z + 1, a.w + 1);
    public static Vector4Int operator --(Vector4Int a) => new(a.x - 1, a.y - 1, a.z - 1, a.w - 1);

    public static Vector4Int operator ^(Vector4Int a, int n) => new((int)Math.Pow(a.x, n), (int)Math.Pow(a.y, n), (int)Math.Pow(a.z, n), (int)Math.Pow(a.w, n));

    public static Vector4Int operator !(Vector4Int a) => a * -1;

    public static float operator &(Vector4Int a, Vector4Int b) => Dot(a,b);

    #endregion
}
