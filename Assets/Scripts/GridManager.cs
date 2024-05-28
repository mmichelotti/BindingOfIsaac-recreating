using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(TileFactory))]
public class GridManager : MonoBehaviour
{
    [SerializeField] Directions startingPosition;

    [Range(0,1)] [SerializeField] private float chanceForDoor = 0.8f;

    private TileFactory tileFactory;
    private readonly Dictionary<Float2, Tile> tileAtPosition = new();
    private readonly List<Room> rooms = new();

    [field:SerializeField] public MazeGrid Grid { get; private set; }
    public Float2 FirstTile { get; private set; }

    public Room FurthestRoom
    {
        get
        {
            float dist = 0f;
            Room from = tileAtPosition[FirstTile].Room;
            Room mostDistant = from;
            foreach (Room room in rooms)
            {
                float currentDist = Float2.Distance(from.Position, room.Position);
                if (currentDist > dist)
                {
                    dist = currentDist;
                    mostDistant = room;
                }
            }
            return mostDistant;
        }
    }

    public Room GetFurthestRoomAt(Directions dir)
    {
        float dist = 0f;
        Room from = tileAtPosition[FirstTile].Room;
        Room mostDistant = from;
        foreach (Room room in rooms)
        {
            float currentDist = Float2.Distance(from.Position, room.Position);
            if (currentDist > dist && room.DirectionFromCenter == dir)
            {
                dist = currentDist;
                mostDistant = room;
            }
        }
        return mostDistant;
    }


    private void Awake()
    {
        tileFactory = GetComponent<TileFactory>();
    }

    private void Start()
    {
        FirstTile = Grid.GetCoordinatesAt(startingPosition);
    }
    public void Execute()
    {
        ConnectTilesAt(tileAtPosition[FirstTile]);
        RegisterDirectionsFrom(tileAtPosition[FirstTile]);
    }

    //can calculate directions from different classes that derive from Point
    //would have preferred an Interface but interface doesnt allow me to implement properties without re-declaring them in the classes
    public void RegisterDirectionsFrom(Point origin)
    {
        foreach (Point room in rooms) room.SetDirectionFrom(origin.Position);
    }
    public bool RegisterTileAt(Float2 pos)
    {
        if (!tileAtPosition.ContainsKey(pos))
        {
            Tile tile = tileFactory.CurrentTile;
            tile.Initialize(pos);
            tileAtPosition.Add(pos, tile);
            return true;
        }

        return false;
    }

    public bool ClearTileAt(Float2 pos)
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
        foreach (var tile in tileAtPosition.Values) tileFactory.Deactivate(tile);

        tileAtPosition.Clear();
    }

    private readonly HashSet<Float2> visitedTiles = new();


    public void ConnectTilesAt(Tile origin)
    {
        Float2 pos = origin.Position;
        visitedTiles.Add(pos);
        // tile.Room ??= new();
        if (origin.Room is null)
        {
            origin.Room = new();
            origin.Room.Tiles.Add(origin);
            rooms.Add(origin.Room);
        }
        foreach (var (dir, offset) in DirectionUtility.OrientationOf)
        {
            Float2 newPos = pos + offset;
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
    public int CountNeighbors(Float2 pos)
    {
        // class Base { }
        // class Derived : Base { }

        //System.Func<Float2, bool> func;
        //System.Func<Derived, Base> action;
        //
        //Derived Test(Base derived) { return null; }
        //
        //action = Test;

        //bool ShouldTakeThisValue(Float2 dir)
        //{
        //    return tileAtPosition.ContainsKey(pos + dir);
        //}
        //
        //func = ShouldTakeThisValue;
        //func = dir => tileAtPosition.ContainsKey(pos + dir);

        //IEnumerable<Float2> values = DirectionUtility.DirectionToVector.Values;
        //IEnumerable<Float2> where = values.Where(dir => tileAtPosition.ContainsKey(pos + dir));
        //int count = where.Count();

        return DirectionUtility.OrientationOf.Values
            //.Where(func)
            .Where(offset => tileAtPosition.ContainsKey(pos + offset))
            .Count();
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        for (int x = 0; x < Grid.Length; x++)
        {            for (int y = 0; y < Grid.Length; y++)
            {
                Vector3 pos = Grid.CoordinateToPosition(new Float2(x, y));
                Gizmos.DrawWireCube(pos, new Vector3(Grid.Size.x, Grid.Size.y, 0));
            }
        }
    }
}
