using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [Range(0, 1)]
    [SerializeField] private float connectionDepth = 0.25f;

    //glielo deve passare la griglia
    [SerializeField]
    private Vector2 scale = new(20, 10);
    public Vector2Int RoomIndex { get; set; }
    private ConnectionsHandler dc;

    private void Awake()
    {
        dc = GetComponent<ConnectionsHandler>();
        dc.InitializeConnections(scale, connectionDepth);
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
