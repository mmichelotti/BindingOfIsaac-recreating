using UnityEngine;
using System.Collections.Generic;

[SelectionBase]
public class Tile : MonoBehaviour
{
    public Vector2Int Coordinates { get; set; }

    [SerializeField]
    private GameObject floorPF;

    [Range(0, 1)]
    [SerializeField]
    private float wallThickness = 1.0f;

    [SerializeReference]
    public Room Room;

    private TileBlueprint normalTile;
    private ConnectionsHandler ch;
    private GameObject floor;

    private void Awake()
    {
        ch = GetComponent<ConnectionsHandler>();
        normalTile = new(GameManager.Instance.GridManager.Grid.Size, wallThickness);
    }

    private void Start()
    {
        floor = Instantiate(floorPF, transform);
        floor.transform.localScale = normalTile.Scale;
        ch.InstantiateConnections();

    }

    public void PositionConnections()
    {
        ch.SetConnectionsTransform(normalTile);
    }

    public void OpenConnections(Direction dir)
    {
        ch.OpenDoor(dir);
        ch.OpenWall(dir);
    }

    public void CloseConnections(Direction dir)
    {
        ch.CloseDoor(dir);
        ch.CloseWall(dir);
    }

    public void DebugRoomColor()
    {
        floor.GetComponent<SpriteRenderer>().color = Room.Color;
    }
}
