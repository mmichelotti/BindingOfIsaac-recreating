using UnityEngine;

public readonly struct Orientation
{
    //SINGLE COMPONENT DECONSTRUCTOR DOENST WORK SO A VIABLE OPTION WAS TO HAVE AN IMPLICIT CAST
    #region attributes
    private readonly Float2 offset;
    private readonly Quaternion rotation;
    #endregion
    #region constructors
    public Orientation(Float2 offset, Quaternion rotation) => (this.offset, this.rotation) = (offset, rotation);
    public readonly void Deconstruct(out Float2 offset, out Quaternion rotation) => (offset,rotation) = (this.offset, this.rotation);
    #endregion
    #region properties
    public static Orientation Zero => new(Float2.Zero, Quaternion.Euler(0, 0, 0));
    public static Orientation Up => new(Float2.Up, Quaternion.Euler(0, 0, 0));
    public static Orientation Right => new(Float2.Right, Quaternion.Euler(0, 0, 90));
    public static Orientation Down => new(Float2.Down, Quaternion.Euler(0, 0, 180));
    public static Orientation Left => new(Float2.Left, Quaternion.Euler(0, 0, 270));
    #endregion
    #region operators
    public static implicit operator Float2(Orientation o) => o.offset;
    public static implicit operator Quaternion(Orientation o) => o.rotation;
    public static Float2 operator +(Float2 a, Orientation b) => a + b.offset;
    public static Float2 operator +(Orientation a, Float2 b) => a.offset + b;
    public static Float2 operator *(Float2 a, Orientation b) => a * b.offset;
    public static Float2 operator *(Orientation a, Float2 b) => a.offset * b;
    public static Float2 operator *(int a, Orientation b) => a * b.offset;
    public static Float2 operator *(Orientation a, int b) => a.offset * b;
    #endregion
}
