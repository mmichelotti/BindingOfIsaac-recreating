using System.Collections.Generic;
using UnityEngine;
public enum Direction
{
    Up,
    Right,
    Down,
    Left
}
public enum SpawnPositions
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
    public static readonly Dictionary<Direction, Vector2Int> DirectionToVector = new()
    {
        { Direction.Up, new Vector2Int(0, 1) },
        { Direction.Right, new Vector2Int(1, 0) },
        { Direction.Down, new Vector2Int(0, -1) },
        { Direction.Left, new Vector2Int(-1, 0) }
    };
    public static readonly Dictionary<Direction, Quaternion> DirectionToOrientation = new()
    {
        { Direction.Up, Quaternion.Euler(0, 0, 0) },
        { Direction.Right, Quaternion.Euler(0, 0, 90) },
        { Direction.Down, Quaternion.Euler(0, 0, 180) },
        { Direction.Left, Quaternion.Euler(0, 0, 270) }
    };
    public static readonly Dictionary<SpawnPositions, Vector2> PositionToCoordinate = new()
    {
        { SpawnPositions.Center, new(.5f, .5f) },
        { SpawnPositions.UpLeft, new(0, 1) },
        { SpawnPositions.Up,  new(.5f, 1)},
        { SpawnPositions.UpRight, new(1, 1) },
        { SpawnPositions.Right, new(1, .5f) },
        { SpawnPositions.DownRight, new(1, 0) },
        { SpawnPositions.Down,  new(.5f, 0)},
        { SpawnPositions.DownLeft, new(0, 0) },
        { SpawnPositions.Left,  new(0, .5f)}
    };
    public static Direction GetOppositeDirection(Direction dir) => (Direction)(((int)dir + 2) % 4);

    public static Vector3 ConvertCoordinateToPos(Vector2Int pos)
    {
        return Vector3.zero;
    }
}
