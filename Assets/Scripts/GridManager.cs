using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(TileFactory))]
public class GridManager : MonoBehaviour
{
    [field:SerializeField] public MazeGrid Grid { get; private set; }

    [Range(0,1)] [SerializeField] private float chanceForDoor = 0.8f;

    private TileFactory rf;

    private readonly Dictionary<Vector2Int, Tile> roomsAtPosition = new();

    private void Awake()
    {
        rf = GetComponent<TileFactory>();
    }

    private void Start()
    {
        rf.PrepareRoomsPooling();
    }

    public bool RegisterRoomAt(Vector2Int pos)
    {
        if (!roomsAtPosition.ContainsKey(pos))
        {
            Tile room = rf.ActivateRoom(Grid.CoordinateToPosition(pos));
            room.RoomCoordinate = pos;
            roomsAtPosition.Add(pos, room);
            return true;
        }

        return false;
    }

    public bool ClearRoomAt(Vector2Int pos)
    {
        if (roomsAtPosition.ContainsKey(pos))
        {
            rf.Deactivate(roomsAtPosition[pos]);
            roomsAtPosition.Remove(pos);
            return true;
        }

        return false;
    }

    public void ClearRooms()
    {
        foreach (var room in roomsAtPosition.Values)
            rf.Deactivate(room);

        roomsAtPosition.Clear();
    }

    public void OpenDoors()
    {
        foreach (var pos in roomsAtPosition)
        {
            OpenDoorsAt(pos.Key);
        }
    }
    public void OpenDoorsAt(Vector2Int pos)
    {
        foreach (var dir in DirectionUtility.DirectionToVector)
        {
            Vector2Int newPos = pos + dir.Value;
            if (roomsAtPosition.TryGetValue(newPos, out Tile adjacentRoom))
            {
                roomsAtPosition[pos].PositionConnections();
                Direction oppositeDirection = DirectionUtility.GetOppositeDirection(dir.Key);

                if (Random.value < chanceForDoor)
                {
                    roomsAtPosition[pos].OpenConnections(dir.Key);
                    adjacentRoom.OpenConnections(oppositeDirection);
                }
                else
                {
                    roomsAtPosition[pos].CloseConnections(dir.Key);
                    adjacentRoom.CloseConnections(oppositeDirection);
                }
            }
        }
    }

    public int CountNeighbors(Vector2Int pos) =>
        (
            from Vector2Int dir in DirectionUtility.DirectionToVector.Values
            where roomsAtPosition.ContainsKey(pos + dir)
            select dir
        ).Count();


}
