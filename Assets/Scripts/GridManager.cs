using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(TileFactory))]
public class GridManager : MonoBehaviour
{
    [SerializeField]
    SpawnPosition startingPosition;

    [field:SerializeField] public MazeGrid Grid { get; private set; }

    [Range(0,1)] [SerializeField] private float chanceForDoor = 0.8f;

    private TileFactory tileFactory;

    private readonly Dictionary<Vector2Int, Tile> tileAtPosition = new();
    private List<Room> rooms = new();

    public Vector2Int FirstTile { get; private set; }

    private void Awake()
    {
        tileFactory = GetComponent<TileFactory>();
    }

    private void Start()
    {
        FirstTile = Grid.GetCoordinatesAt(startingPosition);
        tileFactory.PrepareTilesPooling();
    }

    public bool RegisterTileAt(Vector2Int pos)
    {
        if (!tileAtPosition.ContainsKey(pos))
        {
            Tile tile = tileFactory.ActivateTile(Grid.CoordinateToPosition(pos));
            tile.Coordinates = pos;
            tileAtPosition.Add(pos, tile);
            return true;
        }

        return false;
    }

    public bool ClearTileAt(Vector2Int pos)
    {
        if (tileAtPosition.ContainsKey(pos))
        {
            tileFactory.Deactivate(tileAtPosition[pos]);
            tileAtPosition.Remove(pos);
            return true;
        }

        return false;
    }

    public void ClearTiles()
    {
        foreach (var tile in tileAtPosition.Values)
            tileFactory.Deactivate(tile);

        tileAtPosition.Clear();
    }

    public void OpenDoors()
    {
        OpenDoors(tileAtPosition[FirstTile]);
    }

    private readonly HashSet<Vector2Int> visitedTiles = new();

    public void OpenDoors(Tile tile)
    {
        Vector2Int pos = tile.Coordinates;
        visitedTiles.Add(pos);
        // tile.Room ??= new();
        if (tile.Room is null)
        {
            tile.Room = new();
            tile.Room.Tiles.Add(tile);
            rooms.Add(tile.Room);
        }
        foreach (var dir in DirectionUtility.DirectionToVector)
        {
            Vector2Int newPos = pos + dir.Value;

            if (visitedTiles.Contains(newPos))
            {
                continue;
            }

            if (tileAtPosition.TryGetValue(newPos, out Tile adjacentTile))
            {
                tileAtPosition[pos].PositionConnections();
                Direction oppositeDirection = DirectionUtility.GetOppositeDirection(dir.Key);

                if (Random.value < chanceForDoor)
                {
                    tileAtPosition[pos].OpenConnections(dir.Key);
                    adjacentTile.OpenConnections(oppositeDirection);
                }
                else
                {
                    adjacentTile.Room = tile.Room;
                    tile.Room.Tiles.Add(adjacentTile);
                    tileAtPosition[pos].CloseConnections(dir.Key);
                    adjacentTile.CloseConnections(oppositeDirection);
                }
                OpenDoors(adjacentTile);
            }
        }
        tile.DebugRoomColor();
    }

    // class Base { }
    // class Derived : Base { }

    public int CountNeighbors(Vector2Int pos)
    {
        //System.Func<Vector2Int, bool> func;
        //System.Func<Derived, Base> action;
        //
        //Derived Test(Base derived) { return null; }
        //
        //action = Test;

        //bool ShouldTakeThisValue(Vector2Int dir)
        //{
        //    return tileAtPosition.ContainsKey(pos + dir);
        //}
        //
        //func = ShouldTakeThisValue;
        //func = dir => tileAtPosition.ContainsKey(pos + dir);

        return DirectionUtility.DirectionToVector.Values
            //.Where(func)
            .Where(dir => tileAtPosition.ContainsKey(pos + dir))
            .Count();
    }
}
