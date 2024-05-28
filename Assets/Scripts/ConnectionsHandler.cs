using System.Collections.Generic;
using UnityEngine;

public class ConnectionsHandler : MonoBehaviour
{
    #region variables
    [SerializeField] private GameObject doorPrefab;
    [SerializeField] private GameObject wallPrefab;
    [SerializeField, Range(0, 1)]private float doorSize = 0.25f;

    private readonly Dictionary<Directions,(GameObject door, GameObject wall)> directionToConnections = new();
    #endregion
    #region methods
    public void InstantiateConnections()
    {
        GameObject doors = InitEmptyGO("Doors", transform);
        GameObject walls = InitEmptyGO("Walls", transform);

        foreach (Directions dir in DirectionUtility.OrientationOf.Keys)
        {
            GameObject door = Instantiate(doorPrefab, doors.transform);
            GameObject wall = Instantiate(wallPrefab, walls.transform);
            directionToConnections[dir] = (door, wall);
            CloseDoor(dir);
            OpenWall(dir);
        }
    }
    public void SetConnectionsTransform(TileBlueprint tBP)
    {
        float doorLength = Mathf.Min(tBP.Scale.x, tBP.Scale.y) * doorSize;
        foreach (var (dir, (door, wall)) in directionToConnections)
        {
            float wallLength = ((int)dir % 2 == 0) ? tBP.Scale.x : tBP.Scale.y;
            Vector3 wallScale = new(wallLength, tBP.EdgeThickness);
            Vector3 doorScale = new(doorLength, tBP.EdgeThickness);
            Vector3 position = CalculateOffset(dir, tBP.Spacing);

            wall.transform.localScale = wallScale;
            wall.name = $"{dir}_Wall";
            wall.transform.SetPositionAndRotation(position, dir.GetRotation());

            door.transform.localScale = doorScale;
            door.name = $"{dir}_Door";
            door.transform.SetPositionAndRotation(position, dir.GetRotation());
        }
    }

    private static GameObject InitEmptyGO(string name, Transform transform)
    {
        var go = new GameObject(name);
        go.transform.parent = transform;
        return go;
    }
    private Vector3 CalculateOffset(Directions dir, Float2 spacing) => (dir.GetOffset() * spacing) + (Float3)transform.position;
    public void OpenDoor(Directions dir) => directionToConnections[dir].door.SetActive(true);
    public void OpenWall(Directions dir) => directionToConnections[dir].wall.SetActive(true);
    public void CloseDoor(Directions dir) => directionToConnections[dir].door.SetActive(false);
    public void CloseWall(Directions dir) => directionToConnections[dir].wall.SetActive(false);
    #endregion
}
