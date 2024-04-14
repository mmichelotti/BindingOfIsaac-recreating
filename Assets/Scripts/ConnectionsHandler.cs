using UnityEngine;
using System.Collections.Generic;



public class ConnectionsHandler : MonoBehaviour
{
    [SerializeField] private GameObject doorPrefab;
    [SerializeField] private GameObject wallPrefab;
    [SerializeField] [Range(0, 1)] private float doorSize = 0.25f;
    private readonly Dictionary<Direction, (Connection door, Connection wall)> directionToConnections = new();
    private RoomBlueprint RoomBlueprint { get; set; }

    #region Public
    public RoomBlueprint InitializeConnections(RoomBlueprint roomBP) => RoomBlueprint = roomBP; 
    public void InstantiateConnections()
    {
        GameObject doors = InitEmptyGO("Doors", transform);
        GameObject walls = InitEmptyGO("Walls", transform);

        foreach (Direction dir in System.Enum.GetValues(typeof(Direction)))
        {
            Connection door = new (doorPrefab, doors.transform);
            Connection wall = new (wallPrefab, walls.transform);

            float wallLength = (((int)dir) % 2 == 0) ? RoomBlueprint.Scale.x : RoomBlueprint.Scale.y;
            float doorLenght = Mathf.Min(RoomBlueprint.Scale.x, RoomBlueprint.Scale.y) * doorSize;

            wall.GameObject.transform.localScale = new Vector3(wallLength, RoomBlueprint.EdgeThickness);
            door.GameObject.transform.localScale = new Vector3(doorLenght, RoomBlueprint.EdgeThickness);

            directionToConnections[dir] = (door, wall);
            CloseDoor(dir);
            OpenWall(dir);
        }
    }

    public void PositionConnections()
    {
        Vector2 spacing = new(RoomBlueprint.CenterOffset.x - RoomBlueprint.EdgeOffset, RoomBlueprint.CenterOffset.y - RoomBlueprint.EdgeOffset);
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


