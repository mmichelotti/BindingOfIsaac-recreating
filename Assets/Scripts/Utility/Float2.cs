using System;
using System.Runtime.InteropServices;
namespace UnityEngine
{
    static class MethodImplOptionsEx
    {
        public const short AggressiveInlining = 256;
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct Float2 : IEquatable<Float2>, IFormattable
    {
        #region base
        public float x, y;
        public Float2(float x, float y) => (this.x, this.y) = (x, y);
        #endregion
        #region constants
        public const float kEpsilon = 0.00001F;
        public const float kEpsilonNormalSqrt = 1e-15f;
        #endregion
        #region properties
        public static Float2 Zero => new(0, 0);
        public static Float2 One => new(1, 1);
        public static Float2 Up => new(0, 1);
        public static Float2 Down => new(0, -1);
        public static Float2 Left => new(-1, 0);
        public static Float2 Right => new(1, 0);

        public float Magnitude => (float)Math.Sqrt(x * x + y * y);
        public float SqrMagnitude => x * x + y * y;
        #endregion
        #region interfaces
        public override string ToString() => ToString(null, null);
        public string ToString(string format) => ToString(format, null);
        public string ToString(string format, IFormatProvider formatProvider) => String.Format("({0}, {1})", x.ToString(format, formatProvider), y.ToString(format, formatProvider));
        public override int GetHashCode() => x.GetHashCode() ^ (y.GetHashCode() << 2);
        public override bool Equals(object other) => other is Float2 v && v.Equals(other);
        public bool Equals(Float2 other) => x == other.x && y == other.y;
        #endregion
        #region methods
        public static Float2 Min(Float2 lhs, Float2 rhs) => new(Mathf.Min(lhs.x, rhs.x), Mathf.Min(lhs.y, rhs.y));
        public static Float2 Max(Float2 lhs, Float2 rhs) => new(Mathf.Max(lhs.x, rhs.x), Mathf.Max(lhs.y, rhs.y));
        public static float Dot(Float2 lhs, Float2 rhs) { return lhs.x * rhs.x + lhs.y * rhs.y; }
        public static float Distance(Float2 a, Float2 b)
        {
            float diff_x = a.x - b.x;
            float diff_y = a.y - b.y;
            return (float)Math.Sqrt(diff_x * diff_x + diff_y * diff_y);
        }
        public void Normalize()
        {
            float mag = Magnitude;
            if (mag > kEpsilon)
                this = this / mag;
            else
                this = Zero;
        }
        #endregion
        #region operators
        public float this[int index]
        {

            readonly get
            {
                return index switch
                {
                    0 => x,
                    1 => y,
                    _ => throw new IndexOutOfRangeException("Invalid Float2 index!"),
                };
            }


            set
            {
                switch (index)
                {
                    case 0: x = value; break;
                    case 1: y = value; break;
                    default:
                        throw new IndexOutOfRangeException("Invalid Float2 index!");
                }
            }
        }
        public static Float2 operator +(Float2 a, Float2 b) =>    new (a.x + b.x, a.y + b.y);
        public static Float2 operator +(Float2 a, float b) =>     new(a.x + b, a.y + b);
        public static Float2 operator +(float a, Float2 b) =>     new(a + b.x, a + b.y);
        public static Float2 operator -(Float2 a, float b) =>     new(a.x - b, a.y - b);
        public static Float2 operator -(float a, Float2 b) =>     new(a - b.x, a - b.y);
        public static Float2 operator -(Float2 a, Float2 b) =>    new (a.x - b.x, a.y - b.y);
        public static Float2 operator *(Float2 a, Float2 b) =>    new(a.x * b.x, a.y * b.y);
        public static Float2 operator /(Float2 a, Float2 b) =>    new(a.x / b.x, a.y / b.y);
        public static Float2 operator -(Float2 a) =>              new(-a.x, -a.y);
        public static Float2 operator *(Float2 a, float d) =>     new(a.x * d, a.y * d);
        public static Float2 operator *(float d, Float2 a) =>     new(a.x * d, a.y * d);
        public static Float2 operator /(Float2 a, float d) =>     new(a.x / d, a.y / d);
        public static bool operator !=(Float2 lhs, Float2 rhs) => !(lhs == rhs);

        public static implicit operator Float2(Vector3 v) =>      new(v.x, v.y);
        public static implicit operator Vector3(Float2 v) =>      new(v.x, v.y, 0);

        public static bool operator ==(Float2 lhs, Float2 rhs)
        {
            // Returns false in the presence of NaN values.
            float diff_x = lhs.x - rhs.x;
            float diff_y = lhs.y - rhs.y;
            return (diff_x * diff_x + diff_y * diff_y) < kEpsilon * kEpsilon;
        }
        #endregion
    }
}
