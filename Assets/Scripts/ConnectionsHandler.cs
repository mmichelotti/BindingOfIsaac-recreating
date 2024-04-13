using UnityEngine;
using System.Collections.Generic;


public struct RoomStructure
{
    public Vector2 Scale;
    public Vector2 CenterOffset;
    public float WallThickness;
    public float EdgeOffset;
    public RoomStructure(Vector2 scale, float wallDepth)
    {
        Scale = scale;
        CenterOffset = scale / 2;
        WallThickness = wallDepth;
        EdgeOffset = wallDepth / 2;
    }
}
public class ConnectionsHandler : MonoBehaviour
{
    [SerializeField] private GameObject doorPrefab;
    [SerializeField] private GameObject wallPrefab;
    private readonly Dictionary<Direction, (Connection door, Connection wall)> directionToConnections = new();
    RoomStructure room = new();

    #region Public
    public void InitializeConnections(Vector2 roomScale, float wallsDepth)
    {
        room = new(roomScale, wallsDepth);
        GameObject doors = InitEmptyGO("Doors", transform);
        GameObject walls = InitEmptyGO("Walls", transform);

        foreach (Direction dir in System.Enum.GetValues(typeof(Direction)))
        {
            Connection door = new (doorPrefab, doors.transform);
            Connection wall = new (wallPrefab, walls.transform);

            float wallLength = (((int)dir) % 2 == 0) ? room.Scale.x : room.Scale.y;
            float doorLenght = room.Scale.x / 8;
            wall.GameObject.transform.localScale = new Vector3(wallLength, room.WallThickness);
            door.GameObject.transform.localScale = new Vector3(doorLenght, room.WallThickness);

            directionToConnections[dir] = (door, wall);
            CloseDoor(dir);
            OpenWall(dir);
        }
    }
    public void PositionConnections()
    {
        Vector2 spacing = new(room.CenterOffset.x - room.EdgeOffset, room.CenterOffset.y - room.EdgeOffset);
        foreach (var entry in directionToConnections)
        {
            Direction dir = entry.Key;
            var (door, wall) = entry.Value;

            door.Initialize($"{dir}_Door", GetPositionAt(dir, spacing), GetRotationAt(dir));
            wall.Initialize($"{dir}_Wall", GetPositionAt(dir, spacing), GetRotationAt(dir));
        }
    }

    public void OpenDoor(Direction dir)
    {
        directionToConnections[dir].door.SetActive(true);
    }

    public void CloseDoor(Direction dir)
    {
        directionToConnections[dir].door.SetActive(false);
    }

    public void OpenWall(Direction dir)
    {
        directionToConnections[dir].wall.SetActive(true);
    }

    public void CloseWall(Direction dir)
    {
        directionToConnections[dir].wall.SetActive(false);
    }

    #endregion

    #region Private

    private static GameObject InitEmptyGO(string name, Transform transform)
    {
        GameObject go = new(name);
        go.transform.parent = transform;
        return go;
    }

    private Vector3 GetPositionAt(Direction dir, Vector2 spacing)
    {
        Vector2 offset = DirectionUtility.DirectionToVector[dir] * spacing;
        return new Vector3(offset.x, offset.y, 0) + transform.position;
    }

    private static Quaternion GetRotationAt(Direction dir)
    {
        return DirectionUtility.DirectionToOrientation[dir];
    }
    #endregion
}


