using System.Collections.Generic;
using UnityEngine;

public class TileFactory : MonoBehaviour
{
    [SerializeField]
    private Tile roomPF;
    private readonly List<Tile> roomPool = new();
    private int currentIndex = 0;

    public void PrepareRoomsPooling()
    {

        GameObject parent = new("Rooms");
        int roomsCount = GameManager.Instance.SpawnManager.RoomsAmount;
        for (int i = 0; i < roomsCount; i++)
        {
            Tile newRoom = Instantiate(roomPF, parent.transform);
            roomPool.Add(newRoom);
            Deactivate(newRoom);
        }
    }

    public Tile ActivateRoom(Vector3 wsPos, string name = "")
    {
        Tile room = GetPooledRoom();
        Activate(room);
        room.transform.position = wsPos;
        room.name = $"{name}Room";
        return room;
    }

    private Tile GetPooledRoom()
    {
        Tile room = roomPool[currentIndex];
        currentIndex = (currentIndex + 1) % roomPool.Count; // Wrap index if it goes beyond the pool size
        return room;
    }

    public void Activate(Tile room) => room.gameObject.SetActive(true);

    public void Deactivate(Tile room) => room.gameObject.SetActive(false);
}
