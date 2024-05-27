using System;
namespace UnityEngine
{
    public struct Int2 : IEquatable<Int2>, IFormattable
    {
        #region base
        public int x, y;
        public Int2(int x, int y) => (this.x, this.y) = (x, y);
        #endregion
        #region properties

        public static Int2 Zero => new(0, 0);
        public static Int2 One => new(1, 1);
        public static Int2 Up => new(0, 1);
        public static Int2 Down => new(0, -1);
        public static Int2 Left => new(-1, 0);
        public static Int2 Right => new(1, 0);

        public float Magnitude => Mathf.Sqrt((x * x + y * y));
        public int SqrtMagnitude => x * x + y * y;

        #endregion
        #region methods
        public static float Distance(Int2 a, Int2 b)
        {
            float diff_x = a.x - b.x;
            float diff_y = a.y - b.y;

            return (float)Math.Sqrt(diff_x * diff_x + diff_y * diff_y);
        }

        public void Clamp(Int2 min, Int2 max)
        {
            x = Math.Max(min.x, x);
            x = Math.Min(max.x, x);
            y = Math.Max(min.y, y);
            y = Math.Min(max.y, y);
        }
        public static Int2 Min(Int2 lhs, Int2 rhs) => new (Mathf.Min(lhs.x, rhs.x), Mathf.Min(lhs.y, rhs.y)); 
        public static Int2 Max(Int2 lhs, Int2 rhs) =>new (Mathf.Max(lhs.x, rhs.x), Mathf.Max(lhs.y, rhs.y));
        public static Int2 Scale(Int2 a, Int2 b) => new (a.x * b.x, a.y * b.y); 
        public void Scale(Int2 scale) { x *= scale.x; y *= scale.y; }
        public static Int2 FloorToInt(Float2 v) => new (Mathf.FloorToInt(v.x), Mathf.FloorToInt(v.y));
        public static Int2 CeilToInt(Float2 v) => new(Mathf.CeilToInt(v.x), Mathf.CeilToInt(v.y));
        public static Int2 RoundToInt(Float2 v) => new(Mathf.RoundToInt(v.x), Mathf.RoundToInt(v.y));
        #endregion
        #region operators
        public int this[int index]
        {
            
            readonly get
            {
                return index switch
                {
                    0 => x,
                    1 => y,
                    _ => throw new IndexOutOfRangeException(String.Format("Invalid Int2 index addressed: {0}!", index)),
                };
            }

            
            set
            {
                switch (index)
                {
                    case 0: x = value; break;
                    case 1: y = value; break;
                    default:
                        throw new IndexOutOfRangeException(String.Format("Invalid Int2 index addressed: {0}!", index));
                }
            }
        }
        public static implicit operator Float2(Int2 v) => new(v.x, v.y);
        public static explicit operator Vector3Int(Int2 v) => new(v.x, v.y, 0);
        public static Int2 operator -(Int2 v) =>  new (-v.x, -v.y);
        public static Int2 operator +(Int2 a, Int2 b) => new (a.x + b.x, a.y + b.y);
        public static Int2 operator -(Int2 a, Int2 b) => new (a.x - b.x, a.y - b.y);
        public static Int2 operator *(Int2 a, Int2 b) => new (a.x * b.x, a.y * b.y);
        public static Int2 operator *(int a, Int2 b) => new (a * b.x, a * b.y);
        public static Int2 operator *(Int2 a, int b) => new (a.x * b, a.y * b);
        public static Int2 operator /(Int2 a, int b) => new(a.x / b, a.y / b);
        public static bool operator ==(Int2 lhs, Int2 rhs) => lhs.x == rhs.x && lhs.y == rhs.y;
        public static bool operator !=(Int2 lhs, Int2 rhs) => !(lhs == rhs);
        #endregion
        #region interfaces
        public override bool Equals(object other) => other is Int2 v && v.Equals(other);
        public bool Equals(Int2 other) => x == other.x && y == other.y;
        public override int GetHashCode() => HashCode.Combine(x*2, y*2);
        public override string ToString() => ToString(null, null);
        public string ToString(string format) => ToString(format, null);
        public string ToString(string format, IFormatProvider formatProvider) =>
             string.Format("({0}, {1})", x.ToString(format, formatProvider), y.ToString(format, formatProvider));
        #endregion
    }
}