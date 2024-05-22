using System.Collections.Generic;
using UnityEngine;

[System.Flags]
public enum Directions
{
    Center = 0b0000,
    Up = 0b0001,
    Right = 0b0010,
    Down = 0b0100,
    Left = 0b1000,
}

public static class DirectionUtility
{

    public static IReadOnlyDictionary<Directions, Vector2Int> DirectionToVector { get; } = new Dictionary<Directions, Vector2Int>()
    {
        { Directions.Up,    Vector2Int.up },
        { Directions.Right, Vector2Int.right },
        { Directions.Down,  Vector2Int.down },
        { Directions.Left,  Vector2Int.left }
    };

    public static Vector2Int MultipleDirectionsToVector(Directions dir)
    {
        Vector2Int result = Vector2Int.zero;

        if ((dir & Directions.Up) != 0) result += Vector2Int.up;
        if ((dir & Directions.Right) != 0) result += Vector2Int.right;
        if ((dir & Directions.Down) != 0) result += Vector2Int.down;
        if ((dir & Directions.Left) != 0) result += Vector2Int.left;

        return result;
    }

    public static Vector2 DirectionToMatrix(Directions dir)
    {
        Vector2 temp = MultipleDirectionsToVector(dir);
        return (temp / 2f) + new Vector2(.5f,.5f);
    }

    public static Quaternion GetRotation(this Directions dir)
    {
        return dir switch
        {
            Directions.Up => Quaternion.Euler(0, 0, 0),
            Directions.Right => Quaternion.Euler(0, 0, 90),
            Directions.Down => Quaternion.Euler(0, 0, 180),
            Directions.Left => Quaternion.Euler(0, 0, 270),
            _ => throw new System.NotImplementedException(),
        };
    }

    public static Directions GetOpposite(this Directions dir)
    {
        return dir switch
        {
            Directions.Up => Directions.Down,
            Directions.Right => Directions.Left,
            Directions.Down => Directions.Up,
            Directions.Left => Directions.Right,
            _ => throw new System.NotImplementedException(),
        };
    }

    public static Directions GetDirection(Vector2 origin, Vector2 target)
    {
        Vector2 difference = (target - origin).normalized;
        float x = difference.x;
        float y = difference.y;


        if (Mathf.Abs(x) > Mathf.Abs(y)) return x > 0 ? Directions.Right : Directions.Left;
        else return y > 0 ? Directions.Up : Directions.Down;
    }

}
