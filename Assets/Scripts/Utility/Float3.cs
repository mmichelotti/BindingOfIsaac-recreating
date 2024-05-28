using System;
using System.Runtime.InteropServices;

namespace UnityEngine
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Float3 : IEquatable<Float3>, IFormattable
    {
        #region base
        public float x, y, z;
        public Float3(float x, float y, float z) => (this.x, this.y, this.z) = (x, y, z);
        public Float3(float x, float y) => (this.x, this.y, this.z) = (x, y, 0);
        public Float3(Float2 v) => (this.x, this.y, this.z) = (v.x, v.y, 0);
        #endregion
        #region constants
        public const float kEpsilon = 0.00001F;
        public const float kEpsilonNormalSqrt = 1e-15f;
        #endregion
        #region properties
        public static Float3 Zero => new(0, 0, 0);
        public static Float3 One => new(1, 1, 1);
        public static Float3 Up => new(0, 1, 0);
        public static Float3 Down => new(0, -1, 0);
        public static Float3 Left => new(-1, 0, 0);
        public static Float3 Right => new(1, 0, 0);
        public static Float3 Forward => new(0, 0, 1);
        public static Float3 Backward => new(0, 0, -1);

        public float Magnitude => (float)Math.Sqrt(x * x + y * y);
        public float SqrMagnitude => x * x + y * y;
        #endregion
        #region interfaces
        public override string ToString() => ToString(null, null);
        public string ToString(string format) => ToString(format, null);
        public string ToString(string format, IFormatProvider formatProvider) => String.Format("({0}, {1})", x.ToString(format, formatProvider), y.ToString(format, formatProvider));
        public override int GetHashCode() => x.GetHashCode() ^ (y.GetHashCode() << 2);
        public override bool Equals(object other) => other is Float3 v && v.Equals(other);
        public bool Equals(Float3 other) => x == other.x && y == other.y;
        #endregion
        #region methods
        public static Float3 Min(Float3 lhs, Float3 rhs) => new(Mathf.Min(lhs.x, rhs.x), Mathf.Min(lhs.y, rhs.y));
        public static Float3 Max(Float3 lhs, Float3 rhs) => new(Mathf.Max(lhs.x, rhs.x), Mathf.Max(lhs.y, rhs.y));
        public static float Dot(Float3 lhs, Float3 rhs) { return lhs.x * rhs.x + lhs.y * rhs.y; }
        public static float Distance(Float3 a, Float3 b)
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
                    _ => throw new IndexOutOfRangeException("Invalid Float3 index!"),
                };
            }


            set
            {
                switch (index)
                {
                    case 0: x = value; break;
                    case 1: y = value; break;
                    default:
                        throw new IndexOutOfRangeException("Invalid Float3 index!");
                }
            }
        }
        public static Float3 operator +(Float3 a, Float3 b) =>    new (a.x + b.x, a.y + b.y, a.z + b.z);
        public static Float3 operator +(Float3 a, float b) =>     new(a.x + b, a.y + b, a.z + b);
        public static Float3 operator +(float a, Float3 b) =>     new(a + b.x, a + b.y, a + b.z);

        public static Float3 operator +(Float3 a, Float2 b) => new(a.x + b.x, a.y + b.y, a.z);
        public static Float3 operator +(Float2 a, Float3 b) => new(a.x + b.x, a.y + b.y, b.z);

        public static Float3 operator -(Float3 a, Float3 b) =>    new (a.x - b.x, a.y - b.y);
        public static Float3 operator -(Float3 a, float b) =>     new(a.x - b, a.y - b, a.z - b);
        public static Float3 operator -(float a, Float3 b) =>     new(a - b.x, a - b.y, a - b.z);

        public static Float3 operator *(Float3 a, Float3 b) =>    new(a.x * b.x, a.y * b.y, a.z * b.z);
        public static Float3 operator /(Float3 a, Float3 b) =>    new(a.x / b.x, a.y / b.y, a.z / b.z);
        public static Float3 operator -(Float3 a) =>              new(-a.x, -a.y, -a.z);
        public static Float3 operator *(Float3 a, float f) =>     new(a.x * f, a.y * f, a.z * f);
        public static Float3 operator *(float f, Float3 a) =>     new(a.x * f, a.y * f, a.z * f);
        public static Float3 operator /(Float3 a, float f) =>     new(a.x / f, a.y / f, a.z / f);
        public static bool operator !=(Float3 lhs, Float3 rhs) => !(lhs == rhs);

        public static Float3 operator +(Float3 a, Vector3 b) => new(a.x + b.x, a.y + b.y, a.z + b.z);
        public static Float3 operator +(Vector3 a, Float3 b) => new(a.x + b.x, a.y + b.y, a.z + b.z);



        public static implicit operator Float3(Float2 v) =>       new(v.x, v.y, 0);
        public static implicit operator Float3(Vector3 v) =>      new(v.x, v.y, v.z);
        public static implicit operator Vector3(Float3 v) =>      new(v.x, v.y, v.z);

        public static bool operator ==(Float3 lhs, Float3 rhs)
        {
            // Returns false in the presence of NaN values.
            float diff_x = lhs.x - rhs.x;
            float diff_y = lhs.y - rhs.y;
            return (diff_x * diff_x + diff_y * diff_y) < kEpsilon * kEpsilon;
        }
        #endregion
    }
}
