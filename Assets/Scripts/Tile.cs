using UnityEngine;

public class Tile : MonoBehaviour
{
    public Vector2Int RoomCoordinate { get; set; }

    [SerializeField]
    private GameObject floorPF;

    [Range(0, 1)]
    [SerializeField]
    private float wallThickness = 1.0f;

    private RoomBlueprint normalRoom;
    private ConnectionsHandler dc;

    private void Awake()
    {
        dc = GetComponent<ConnectionsHandler>();
        normalRoom = new(GameManager.Instance.GridManager.Grid.Size, wallThickness);
    }

    private void Start()
    {
        GameObject floor = Instantiate(floorPF, transform);
        floor.transform.localScale = normalRoom.Scale;
        dc.InstantiateConnections();
    }

    public void PositionConnections()
    {
        dc.SetConnections(normalRoom);
    }

    public void OpenConnections(Direction dir)
    {
        dc.OpenDoor(dir);
        dc.OpenWall(dir);
    }

    public void CloseConnections(Direction dir)
    {
        dc.CloseDoor(dir);
        dc.CloseWall(dir);
    }
}
