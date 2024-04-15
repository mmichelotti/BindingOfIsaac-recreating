using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RoomFactory))]
public class GridManager : MonoBehaviour
{
    [SerializeField] private float chanceForDoor = 0.8f;
    public Grid Grid;
    
    private RoomFactory rf;
    private readonly Dictionary<Vector2Int, Room> roomsAtPosition = new();
    private void Awake()
    {
        rf = GetComponent<RoomFactory>();
    }
    private void Start()
    {
        rf.PrepareRoomsPooling();
    }


    public bool RegisterRoomAt(Vector2Int pos)
    {
        if (!roomsAtPosition.ContainsKey(pos))
        {
            Room room = rf.ActivateRoom(Grid.GetWSPositionAt(pos));
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
        foreach (var room in roomsAtPosition.Values) rf.Deactivate(room);
        roomsAtPosition.Clear();
    }

    public void OpenDoors()
    {
        foreach (Vector2Int pos in roomsAtPosition.Keys)
        {
            OpenDoorsAt(pos);
        }
    }

    public void OpenDoorsAt(Vector2Int pos)
    {

        foreach (var dir in DirectionUtility.DirectionToVector)
        {
            Vector2Int newPos = pos + dir.Value;
            if (roomsAtPosition.TryGetValue(newPos, out Room adjacentRoom))
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

    public bool HasLessThenXNeighbours(Vector2Int pos, int amount) => CountNeighbours(pos) < amount;
    private int CountNeighbours(Vector2Int pos)
    {
        int neighbours = 0;
        foreach (Vector2Int dir in DirectionUtility.DirectionToVector.Values)
        {
            if (roomsAtPosition.ContainsKey(pos + dir)) neighbours++;
        }
        return neighbours;
    }
}
