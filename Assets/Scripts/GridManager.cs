using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(TileFactory))]
public class GridManager : MonoBehaviour
{
    [field:SerializeField] public MazeGrid Grid { get; private set; }

    [Range(0,1)] [SerializeField] private float chanceForDoor = 0.8f;

    private TileFactory tileFactory;

    private readonly Dictionary<Vector2Int, Tile> tileAtPosition = new();

    private void Awake()
    {
        tileFactory = GetComponent<TileFactory>();
    }

    private void Start()
    {
        tileFactory.PrepareRoomsPooling();
    }

    public bool RegisterRoomAt(Vector2Int pos)
    {
        if (!tileAtPosition.ContainsKey(pos))
        {
            Tile room = tileFactory.ActivateRoom(Grid.CoordinateToPosition(pos));
            room.RoomCoordinate = pos;
            tileAtPosition.Add(pos, room);
            return true;
        }

        return false;
    }

    public bool ClearRoomAt(Vector2Int pos)
    {
        if (tileAtPosition.ContainsKey(pos))
        {
            tileFactory.Deactivate(tileAtPosition[pos]);
            tileAtPosition.Remove(pos);
            return true;
        }

        return false;
    }

    public void ClearRooms()
    {
        foreach (var room in tileAtPosition.Values)
            tileFactory.Deactivate(room);

        tileAtPosition.Clear();
    }

    public void OpenDoors()
    {
        foreach (var pos in tileAtPosition)
        {
            OpenDoorsAt(pos.Key);
        }
    }
    public void OpenDoorsAt(Vector2Int pos)
    {
        foreach (var dir in DirectionUtility.DirectionToVector)
        {
            Vector2Int newPos = pos + dir.Value;
            if (tileAtPosition.TryGetValue(newPos, out Tile adjacentRoom))
            {
                tileAtPosition[pos].PositionConnections();
                Direction oppositeDirection = DirectionUtility.GetOppositeDirection(dir.Key);

                if (Random.value < chanceForDoor)
                {
                    tileAtPosition[pos].OpenConnections(dir.Key);
                    adjacentRoom.OpenConnections(oppositeDirection);
                }
                else
                {
                    tileAtPosition[pos].CloseConnections(dir.Key);
                    adjacentRoom.CloseConnections(oppositeDirection);
                }
            }
        }
    }

    public int CountNeighbors(Vector2Int pos) =>
        (
            from Vector2Int dir in DirectionUtility.DirectionToVector.Values
            where tileAtPosition.ContainsKey(pos + dir)
            select dir
        ).Count();


}
