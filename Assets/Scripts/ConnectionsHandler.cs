using System.Collections.Generic;
using UnityEngine;

public class ConnectionsHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject doorPrefab;

    [SerializeField]
    private GameObject wallPrefab;

    [SerializeField]
    [Range(0, 1)]
    private float doorSize = 0.25f;

    private readonly Dictionary<Direction,(GameObject door, GameObject wall)> directionToConnections = new();

    public void InstantiateConnections()
    {
        GameObject doors = InitEmptyGO("Doors", transform);
        GameObject walls = InitEmptyGO("Walls", transform);

        foreach (Direction dir in System.Enum.GetValues(typeof(Direction)))
        {
            GameObject door = Instantiate(doorPrefab, doors.transform);
            GameObject wall = Instantiate(wallPrefab, walls.transform);
            directionToConnections[dir] = (door, wall);
            CloseDoor(dir);
            OpenWall(dir);
        }
    }

    public void SetConnectionsTransform(TileBlueprint rBP)
    {
        float doorLength = Mathf.Min(rBP.Scale.x, rBP.Scale.y) * doorSize;
        foreach (var (dir, (door, wall)) in directionToConnections)
        {
            float wallLength = ((int)dir % 2 == 0) ? rBP.Scale.x : rBP.Scale.y;
            Vector3 wallScale = new(wallLength, rBP.EdgeThickness);
            Vector3 doorScale = new(doorLength, rBP.EdgeThickness);
            Vector3 position = CalculateOffset(dir, rBP.Spacing);
            Quaternion rotation = GetRotationAt(dir);

            wall.transform.localScale = wallScale;
            wall.name = $"{dir}_Wall";
            wall.transform.SetPositionAndRotation(position, rotation);

            door.transform.localScale = doorScale;
            door.name = $"{dir}_Door";
            door.transform.SetPositionAndRotation(position, rotation);
        }
    }

    public void OpenDoor(Direction dir) => directionToConnections[dir].door.SetActive(true);

    public void OpenWall(Direction dir) => directionToConnections[dir].wall.SetActive(true);

    public void CloseDoor(Direction dir) => directionToConnections[dir].door.SetActive(false);

    public void CloseWall(Direction dir) => directionToConnections[dir].wall.SetActive(false);

    private static GameObject InitEmptyGO(string name, Transform transform)
    {
        var go = new GameObject(name);
        go.transform.parent = transform;
        return go;
    }

    private Vector3 CalculateOffset(Direction dir, Vector2 spacing)
    {
        Vector2 offset = DirectionUtility.DirectionToVector[dir] * spacing;
        return new Vector3(offset.x, offset.y) + transform.position;
    }

    private static Quaternion GetRotationAt(Direction dir) => DirectionUtility.DirectionToRotation[dir];
}
