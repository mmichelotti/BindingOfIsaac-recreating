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
        gameObject.SetActive(false);
    }
    public void Initialize(Vector2Int pos, string name = "")
    {
        Coordinates = pos;
        transform.position = GameManager.Instance.GridManager.Grid.CoordinateToPosition(pos);
        this.name = $"{name}Tile";
    }
    public void PositionConnections()
    {
        ch.SetConnectionsTransform(normalTile);
    }

    public void OpenConnections(Directions dir)
    {
        ch.OpenDoor(dir);
        ch.OpenWall(dir);
    }

    public void CloseConnections(Directions dir)
    {
        ch.CloseDoor(dir);
        ch.CloseWall(dir);
    }

    public void DebugRoomColor()
    {
        floor.GetComponent<SpriteRenderer>().color = Room.Color;
    }
}
