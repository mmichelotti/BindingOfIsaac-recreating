using UnityEngine;

public readonly struct Orientation
{
    private readonly Vector2Int offset;
    private readonly Quaternion rotation;

    public static Orientation Zero => new(Vector2Int.zero, Quaternion.Euler(0, 0, 0));
    public static Orientation Up => new(Vector2Int.up, Quaternion.Euler(0, 0, 0));
    public static Orientation Right => new(Vector2Int.right, Quaternion.Euler(0, 0, 90));
    public static Orientation Down => new(Vector2Int.down, Quaternion.Euler(0, 0, 180));
    public static Orientation Left => new(Vector2Int.left, Quaternion.Euler(0, 0, 270));

    public static implicit operator Vector2Int(Orientation o) => o.offset;
    public static implicit operator Quaternion(Orientation o) => o.rotation;

    public static Vector2Int operator +(Vector2Int a, Orientation b) => a + b.offset;
    public static Vector2Int operator +(Orientation a, Vector2Int b) => a.offset + b;
    public static Vector2Int operator *(Vector2Int a, Orientation b) => a * b.offset;
    public static Vector2Int operator *(Orientation a, Vector2Int b) => a.offset * b;

    public static Vector2 operator +(Vector2 a, Orientation b) => a + b.offset;
    public static Vector2 operator +(Orientation a, Vector2 b) => a.offset + b;
    public static Vector2 operator *(Vector2 a, Orientation b) => a * b.offset;
    public static Vector2 operator *(Orientation a, Vector2 b) => a.offset * b;

    public static Vector2Int operator *(int a, Orientation b) => a * b.offset;
    public static Vector2Int operator *(Orientation a, int b) => a.offset * b;

    public Orientation(Vector2Int offset, Quaternion rotation) => (this.offset, this.rotation) = (offset, rotation);

    public readonly void Deconstruct(out Vector2Int offset, out Quaternion rotation) => (offset,rotation) = (this.offset, this.rotation);

    //SINGLE COMPONENT DECONSTRUCTOR DOENST WORK SO A VIABLE OPTION WAS TO HAVE A CAST
}