using UnityEngine;

public struct Orientation
{
    public Vector2Int offset;
    public Quaternion rotation;

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

    public Orientation(Vector2Int offset, Quaternion rotation)
    {
        this.offset = offset;
        this.rotation = rotation;
    }
    //SINGLE COMPONENT DECONSTRUCTOR DOENST WORK SO A VIABLE OPTION WAS TO HAVE A CAST
    /*
    public void Deconstruct(out Vector2Int offset)
    {
        offset = this.offset;
    }
    
    public void Deconstruct(out Quaternion rotation)
    {
        rotation = this.rotation;
    }*/
    public void Deconstruct(out Vector2Int offset, out Quaternion rotation)
    {
        offset = this.offset;
        rotation = this.rotation;
    }
}