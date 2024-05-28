using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(TileFactory))]
public class GridManager : MonoBehaviour
{
    #region variables
    [SerializeField] Directions startingPosition;
    [SerializeField,Range(0,1)]  private float chanceForDoor = 0.8f;

    private TileFactory tileFactory;
    private readonly Dictionary<Vector2, Tile> tileAtPosition = new();
    private readonly List<Room> rooms = new();
    #endregion


    #region properties
    [field:SerializeField] public MazeGrid Grid { get; private set; }
    public Vector2 FirstTile { get; private set; }
    public Room FurthestRoom
    {
        get
        {
            float dist = 0f;
            Room from = tileAtPosition[FirstTile].Room;
            Room mostDistant = from;
            foreach (Room room in rooms)
            {
                float currentDist = Vector2.Distance(from.Position, room.Position);
                if (currentDist > dist)
                {
                    dist = currentDist;
                    mostDistant = room;
                }
            }
            return mostDistant;
        }
    }
    #endregion

    public Room GetFurthestRoomAt(Directions dir)
    {
        float dist = 0f;
        Room from = tileAtPosition[FirstTile].Room;
        Room mostDistant = from;
        foreach (Room room in rooms)
        {
            float currentDist = Vector2.Distance(from.Position, room.Position);
            if (currentDist > dist && room.DirectionFromCenter == dir)
            {
                dist = currentDist;
                mostDistant = room;
            }
        }
        return mostDistant;
    }

    #region unity events
    private void Awake()
    {
        tileFactory = GetComponent<TileFactory>();
    }

    private void Start()
    {
        FirstTile = Grid.GetCoordinatesAt(startingPosition);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        for (int x = 0; x < Grid.Length; x++)
        {
            for (int y = 0; y < Grid.Length; y++)
            {
                Vector3 pos = Grid.CoordinateToPosition(new Vector2(x, y));
                Gizmos.DrawWireCube(pos, new Vector3(Grid.Size.x, Grid.Size.y));
            }
        }
    }
    #endregion
    #region methods
    public void Execute()
    {
        ConnectTilesAt(tileAtPosition[FirstTile]);
        RegisterDirectionsFrom(tileAtPosition[FirstTile]);
    }

    

    public void RegisterTileAt(Vector2 pos)
    {
        if (!tileAtPosition.ContainsKey(pos))
        {
            Tile tile = tileFactory.CurrentTile;
            tile.Initialize(pos);
            tileAtPosition.Add(pos, tile);
        }
    }
    public void ClearTiles()
    {
        foreach (var tile in tileAtPosition.Values) tileFactory.Deactivate(tile);
        tileAtPosition.Clear();
    }


    private readonly HashSet<Vector2> visitedTiles = new();
    public void ConnectTilesAt(Tile origin)
    {
        Vector2 pos = origin.Position;
        visitedTiles.Add(pos);

        if (origin.Room is null)
        {
            origin.Room = new();
            origin.Room.Tiles.Add(origin);
            rooms.Add(origin.Room);
        }
        foreach (var (dir, offset) in DirectionUtility.OrientationOf)
        {
            Vector2 newPos = pos + offset;
            //if (visitedTiles.Contains(newPos)) continue; //outer rooms
            if (tileAtPosition.TryGetValue(newPos, out Tile adjacentTile))
            {
                tileAtPosition[pos].PositionConnections();
                if (visitedTiles.Contains(newPos)) continue;

                Directions oppositeDirection = dir.GetOpposite();
                if (Random.value < chanceForDoor)
                {
                    tileAtPosition[pos].OpenConnections(dir);
                    adjacentTile.OpenConnections(oppositeDirection);
                }
                else
                {
                    adjacentTile.Room = origin.Room;
                    origin.Room.Tiles.Add(adjacentTile);
                    tileAtPosition[pos].CloseConnections(dir);
                    adjacentTile.CloseConnections(oppositeDirection);
                }
                ConnectTilesAt(adjacentTile);
            }
        }
    }
    public void RegisterDirectionsFrom(IDirectionable origin)
    {
        //can calculate directions from different classes that derive from Point
        foreach (IDirectionable room in rooms) room.SetDirectionFrom(origin.Position);
    }
    public void DebugRoomStatus()
    {
        //Furthest room (should be the Boss room, accessible only with a key)
        FurthestRoom.Color = Color.black;
        Debug.Log($"Most distant room is {FurthestRoom.DirectionFromCenter}");

        //Opposite Furhtest room (should be the key room, that gives access to the boss room)
        Room temp = GetFurthestRoomAt(FurthestRoom.DirectionFromCenter.GetOpposite());
        temp.Color = Color.white;
        Debug.Log($"Most distant OPPOSITE room is {temp.DirectionFromCenter}");

        //Color all rooms to debug
        foreach (var tile in tileAtPosition.Values)
        {
            tile.DebugRoomColor();
        }
    }


    public int CountNeighbors(Vector2 pos)
    {
        // class Base { }
        // class Derived : Base { }

        //System.Func<Vector2, bool> func;
        //System.Func<Derived, Base> action;
        //
        //Derived Test(Base derived) { return null; }
        //
        //action = Test;

        //bool ShouldTakeThisValue(Vector2 dir)
        //{
        //    return tileAtPosition.ContainsKey(pos + dir);
        //}
        //
        //func = ShouldTakeThisValue;
        //func = dir => tileAtPosition.ContainsKey(pos + dir);

        //IEnumerable<Vector2> values = DirectionUtility.DirectionToVector.Values;
        //IEnumerable<Vector2> where = values.Where(dir => tileAtPosition.ContainsKey(pos + dir));
        //int count = where.Count();

        return DirectionUtility.OrientationOf.Values
            //.Where(func)
            .Where(offset => tileAtPosition.ContainsKey(pos + offset))
            .Count();
    }
    #endregion
}
