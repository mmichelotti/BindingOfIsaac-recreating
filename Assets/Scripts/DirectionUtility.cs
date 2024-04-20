using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    Up,
    Right,
    Down,
    Left
}

public enum SpawnPosition
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

    public static Dictionary<Direction, Quaternion> DirectionToOrientation { get; } = new()
        {
            { Direction.Up, Quaternion.Euler(0, 0, 0) },
            { Direction.Right, Quaternion.Euler(0, 0, 90) },
            { Direction.Down, Quaternion.Euler(0, 0, 180) },
            { Direction.Left, Quaternion.Euler(0, 0, 270) }
        };

    public static Dictionary<SpawnPosition, Vector2> PositionToCoordinate { get; } = new()
        {
            { SpawnPosition.Center, new(.5f, .5f) },
            { SpawnPosition.UpLeft, new(0, 1) },
            { SpawnPosition.Up, new(.5f, 1) },
            { SpawnPosition.UpRight, new(1, 1) },
            { SpawnPosition.Right, new(1, .5f) },
            { SpawnPosition.DownRight, new(1, 0) },
            { SpawnPosition.Down, new(.5f, 0) },
            { SpawnPosition.DownLeft, new(0, 0) },
            { SpawnPosition.Left, new(0, .5f) }
        };

    public static Direction GetOppositeDirection(Direction dir)
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

    /* //deprecated, less legible
    public static Direction GetOppositeDirection(Direction dir) => (Direction)(((int)dir + 2) % 4);
    */
}
