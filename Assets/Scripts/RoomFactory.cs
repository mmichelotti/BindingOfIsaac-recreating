// RoomFactory.cs
using UnityEngine;
using System.Collections.Generic;
public class RoomFactory : MonoBehaviour
{
    [SerializeField] private Room roomPF;
    private readonly List<Room> roomPool = new();
    private int currentIndex = 0;

    public void PrepareRoomsPooling()
    {
        GameObject parent = new("Rooms");
        int roomsCount = GameManager.Instance.SpawnManager.RoomsAmount;
        for (int i = 0; i < roomsCount; i++)
        {
            Room newRoom = Instantiate(roomPF, parent.transform);
            roomPool.Add(newRoom);
            Deactivate(newRoom);
        }
    }
    public Room ActivateRoom(Vector3 WSpos, string name = "")
    {
        Room room = GetPooledRoom();
        Activate(room);
        room.transform.position = WSpos;
        room.name = $"{name}Room";
        return room;
    }

    public void Activate(Room room)
    {
        room.gameObject.SetActive(true);
    }
    public void Deactivate(Room room)
    {
        room.gameObject.SetActive(false);
    }

    private Room GetPooledRoom()
    {
        Room room = roomPool[currentIndex];
        currentIndex = (currentIndex + 1) % roomPool.Count; // Wrap index if it goes beyond the pool size
        return room;
    }
}
