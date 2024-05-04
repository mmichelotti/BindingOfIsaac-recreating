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

    private TileBlueprint normalTile;
    private ConnectionsHandler ch;

    private void Awake()
    {
        ch = GetComponent<ConnectionsHandler>();
        normalTile = new(GameManager.Instance.GridManager.Grid.Size, wallThickness);
    }

    private void Start()
    {
        GameObject floor = Instantiate(floorPF, transform);
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
}
