using UnityEngine;

public readonly struct Orientation
{
    //SINGLE COMPONENT DECONSTRUCTOR DOENST WORK SO A VIABLE OPTION WAS TO HAVE AN IMPLICIT CAST
    #region attributes
    private readonly Vector2 offset;
    private readonly Quaternion rotation;
    #endregion
    #region constructors
    public Orientation(Vector2 offset, Quaternion rotation) => (this.offset, this.rotation) = (offset, rotation);
    public readonly void Deconstruct(out Vector2 offset, out Quaternion rotation) => (offset,rotation) = (this.offset, this.rotation);
    #endregion
    #region properties
    public static Orientation Zero => new(Vector2.zero, Quaternion.Euler(0, 0, 0));
    public static Orientation Up => new(Vector2.up, Quaternion.Euler(0, 0, 0));
    public static Orientation Right => new(Vector2.right, Quaternion.Euler(0, 0, 90));
    public static Orientation Down => new(Vector2.down, Quaternion.Euler(0, 0, 180));
    public static Orientation Left => new(Vector2.left, Quaternion.Euler(0, 0, 270));
    #endregion
    #region operators
    public static implicit operator Vector2(Orientation o) => o.offset;
    public static implicit operator Quaternion(Orientation o) => o.rotation;
    public static Vector2 operator +(Vector2 a, Orientation b) => a + b.offset;
    public static Vector2 operator +(Orientation a, Vector2 b) => a.offset + b;
    public static Vector2 operator *(Vector2 a, Orientation b) => a * b.offset;
    public static Vector2 operator *(Orientation a, Vector2 b) => a.offset * b;
    public static Vector2 operator *(int a, Orientation b) => a * b.offset;
    public static Vector2 operator *(Orientation a, int b) => a.offset * b;
    #endregion
}
