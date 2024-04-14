using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public Vector2Int RoomCoordinate { get; set; }
    [SerializeField] private GameObject floorPF;
    [Range(0,1)] [SerializeField] private float wallThickness = 1.0f;

    private RoomBlueprint roomBP;
    private ConnectionsHandler dc;

    private void Awake()
    {
        dc = GetComponent<ConnectionsHandler>();
        roomBP = new(GameManager.Instance.GridManager.GetSize(), wallThickness);
        dc.InitializeConnections(roomBP);
    }
    private void Start()
    {
        GameObject floor = Instantiate(floorPF, transform);
        floor.transform.localScale = roomBP.Scale;
        dc.InstantiateConnections();
    }

    public void PositionConnections()
    {
        dc.PositionConnections();
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
