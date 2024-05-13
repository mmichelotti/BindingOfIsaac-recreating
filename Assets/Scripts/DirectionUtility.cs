using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    Up,
    Right,
    Down,
    Left
}
public enum DirectionExtended
{
    Center,
    UpLeft,
    Up,
    UpRight,
    Right,
    DownRight,
    Down,
    DownLeft,
    Left
}


public static class DirectionUtility
{
    public static Dictionary<Direction, Vector2Int> DirectionToVector { get; } = new()
    {
        { Direction.Up, new Vector2Int(0, 1) },
        { Direction.Right, new Vector2Int(1, 0) },
        { Direction.Down, new Vector2Int(0, -1) },
        { Direction.Left, new Vector2Int(-1, 0) }
    };
    public static Dictionary<DirectionExtended, Vector2Int> DirectionToVectorExtended { get; } = new()
    {
        { DirectionExtended.Center, new Vector2Int(0, 0) },
        { DirectionExtended.UpLeft, new Vector2Int(-1, 1) },
        { DirectionExtended.Up, new Vector2Int(0, 1) },
        { DirectionExtended.UpRight, new Vector2Int(1, 1) },
        { DirectionExtended.Right, new Vector2Int(1, 0) },
        { DirectionExtended.DownRight, new Vector2Int(1, -1) },
        { DirectionExtended.Down, new Vector2Int(0, -1) },
        { DirectionExtended.DownLeft, new Vector2Int(-1, -1) },
        { DirectionExtended.Left, new Vector2Int(-1, 0) }
    };
    public static Vector2 DirectionToMatrix(DirectionExtended dir)
    {
        Vector2 temp = DirectionToVectorExtended[dir];
        return (temp / 2f) + new Vector2(.5f,.5f);
    }


    public static Dictionary<Direction, Quaternion> DirectionToRotation { get; } = new()
    {
        { Direction.Up, Quaternion.Euler(0, 0, 0) },
        { Direction.Right, Quaternion.Euler(0, 0, 90) },
        { Direction.Down, Quaternion.Euler(0, 0, 180) },
        { Direction.Left, Quaternion.Euler(0, 0, 270) }
    };

    public static Dictionary<DirectionExtended, Vector2> PositionToMatrix { get; } = new()
    {
        { DirectionExtended.Center, new(.5f, .5f) },
        { DirectionExtended.UpLeft, new(0, 1) },
        { DirectionExtended.Up, new(.5f, 1) },
        { DirectionExtended.UpRight, new(1, 1) },
        { DirectionExtended.Right, new(1, .5f) },
        { DirectionExtended.DownRight, new(1, 0) },
        { DirectionExtended.Down, new(.5f, 0) },
        { DirectionExtended.DownLeft, new(0, 0) },
        { DirectionExtended.Left, new(0, .5f) }
    };



    /* //deprecated, less legible
    public static Direction GetOppositeDirection(Direction dir) => (Direction)(((int)dir + 2) % 4);
    */

    public static Direction Opposite(Direction dir)
    {
        return dir switch
        {
            Direction.Up => Direction.Down,
            Direction.Right => Direction.Left,
            Direction.Down => Direction.Up,
            Direction.Left => Direction.Right,
            _ => throw new System.NotImplementedException(),
        };
    }

    // PORCA MADONNA DIO PORCO DIO CAN 
    // POSSIBILI OUT 1,1 / 1,0 / 1,-1 / 0,1 / 0,0 / 0,-1 / -1,1 / -1,0 / -1,-1
    // COME POSITION TO COORDINATE
    // VA GENERALIZZATO
    public static Direction GetDirection2(Vector2 origin, Vector2 target)
    {
        Vector2 difference = (target - origin).normalized;
        int x = (int)Mathf.Round(difference.x);
        int y = (int)Mathf.Round(difference.y);


        if (Mathf.Abs(x) > Mathf.Abs(y)) return x > 0 ? Direction.Right : Direction.Left;
        else return y > 0 ? Direction.Up : Direction.Down;
    }

}
