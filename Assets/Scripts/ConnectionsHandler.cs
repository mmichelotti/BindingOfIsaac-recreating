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

    private readonly Dictionary<Directions,(GameObject door, GameObject wall)> directionToConnections = new();

    public void InstantiateConnections()
    {
        GameObject doors = InitEmptyGO("Doors", transform);
        GameObject walls = InitEmptyGO("Walls", transform);

        foreach (Directions dir in DirectionUtility.OffsetOf.Keys)
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
            Quaternion rotation = DirectionUtility.OffsetOf[dir];

            wall.transform.localScale = wallScale;
            wall.name = $"{dir}_Wall";
            wall.transform.SetPositionAndRotation(position, rotation);

            door.transform.localScale = doorScale;
            door.name = $"{dir}_Door";
            door.transform.SetPositionAndRotation(position, rotation);
        }
    }

    public void OpenDoor(Directions dir) => directionToConnections[dir].door.SetActive(true);

    public void OpenWall(Directions dir) => directionToConnections[dir].wall.SetActive(true);

    public void CloseDoor(Directions dir) => directionToConnections[dir].door.SetActive(false);

    public void CloseWall(Directions dir) => directionToConnections[dir].wall.SetActive(false);

    private static GameObject InitEmptyGO(string name, Transform transform)
    {
        var go = new GameObject(name);
        go.transform.parent = transform;
        return go;
    }

    private Vector3 CalculateOffset(Directions dir, Vector2 spacing)
    {
        Vector2Int offset = DirectionUtility.OffsetOf[dir];
        Vector2 wsOffset = offset * spacing;
        return new Vector3(wsOffset.x, wsOffset.y) + transform.position;
    }
}
