using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RoomFactory))]
public class GridManager : MonoBehaviour
{
    private const int LENGTH = 11;
    [SerializeField] private Vector2Int size = new(20, 10);
    [Range(0, 1)]
    [SerializeField] private float chanceForDoor = 0.8f;
    
    private RoomFactory rf;
    private readonly Dictionary<Vector2Int, Room> roomsAtPosition = new();


    #region Public
    public bool RegisterRoomAt(Vector2Int pos)
    {
        if (!roomsAtPosition.ContainsKey(pos))
        {
            Room room = rf.SetRoomAt(pos, GetWSPositionAt(pos));
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

    public Vector2Int GetSpawnPosition(SpawnPositions pos)
    {
        Vector2 vector = DirectionUtility.PositionToCoordinate[pos] * (LENGTH - 1);
        return new((int)vector.x, (int)vector.y);
    }
    public bool HasLessThenXNeighbours(Vector2Int pos, int amount) => CountNeighbours(pos) < amount;
    public bool IsWithinGrid(Vector2Int position) => position.x >= 0 && position.y >= 0 && position.x < LENGTH && position.y < LENGTH;
    #endregion


    #region Private
    private void Awake()
    {
        rf = GetComponent<RoomFactory>();
    }
    private Vector3 GetWSPositionAt(Vector2Int gridIndex) => new (GetHalfPoint(size.x, gridIndex.x), GetHalfPoint(size.y, gridIndex.y));
    private static int GetHalfPoint(int roomDimension, int gridIndex) => roomDimension * (gridIndex - LENGTH / 2);
    private int CountNeighbours(Vector2Int pos)
    {
        int neighbours = 0;
        foreach (Vector2Int dir in DirectionUtility.DirectionToVector.Values)
        {
            if (roomsAtPosition.ContainsKey(pos + dir)) neighbours++;
        }
        return neighbours;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        for (int x = 0; x < LENGTH; x++)
        {
            for (int y = 0; y < LENGTH; y++)
            {
                Vector3 pos = GetWSPositionAt(new Vector2Int(x, y));
                Gizmos.DrawWireCube(pos, new Vector3(size.x, size.y, 1));
            }
        }
    }
    #endregion
}
