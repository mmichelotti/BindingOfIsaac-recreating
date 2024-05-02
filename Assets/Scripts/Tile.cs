using UnityEngine;
using System.Collections.Generic;

public class Tile : MonoBehaviour
{
    public Vector2Int RoomCoordinate { get; set; }

    [SerializeField]
    private GameObject floorPF;

    [Range(0, 1)]
    [SerializeField]
    private float wallThickness = 1.0f;

    private TileBlueprint normalRoom;
    private ConnectionsHandler ch;
    public Dictionary<Direction, bool> doors = new();
    public Dictionary<Direction, bool> walls = new();

    private void Awake()
    {
        ch = GetComponent<ConnectionsHandler>();
        normalRoom = new(GameManager.Instance.GridManager.Grid.Size, wallThickness);
    }

    private void Start()
    {
        GameObject floor = Instantiate(floorPF, transform);
        floor.transform.localScale = normalRoom.Scale;
        ch.InstantiateConnections();
        foreach (Direction dir in System.Enum.GetValues(typeof(Direction)))
        {
            doors[dir] = false;
            walls[dir] = true;
        }
    }

    public void PositionConnections()
    {
        ch.SetConnectionsTransform(normalRoom);
    }

    public void OpenConnections(Direction dir)
    {
        ch.OpenDoor(dir);
        ch.OpenWall(dir);
        walls[dir] = true;
        doors[dir] = true;
    }

    public void CloseConnections(Direction dir)
    {
        ch.CloseDoor(dir);
        ch.CloseWall(dir);
        doors[dir] = false;
        walls[dir] = false;
    }
    public void DebugWalls()
    {
        foreach (var item in walls)
        {
            string value = item.Value ? "YES" : "NO";
            Debug.Log($"{item.Key} {value}");
        }
    }
}
