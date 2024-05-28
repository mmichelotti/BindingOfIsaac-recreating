using UnityEngine;
using System.Collections.Generic;

[SelectionBase]
public class Tile : Point
{
    #region variables
    [SerializeField] private GameObject floorPF;
    [SerializeField, Range(0, 1)] private float wallThickness = 1.0f;
    [SerializeReference] public Room Room;

    private TileBlueprint normalTile;
    private ConnectionsHandler ch;
    private GameObject floor;
    #endregion

    #region initialization
    public void Initialize(Float2 pos, string name = "")
    {
        Position = pos;
        transform.position = GameManager.Instance.GridManager.Grid.CoordinateToPosition(pos);
        this.name = $"{name}Tile";
    }
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
    #endregion
    #region methods
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
    #endregion
}
