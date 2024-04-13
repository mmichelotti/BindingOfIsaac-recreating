// RoomFactory.cs
using UnityEngine;
using System.Collections.Generic;
public class RoomFactory : MonoBehaviour
{
    [SerializeField] private GameObject roomPrefab;
    private readonly List<Room> roomPool = new();
    private GameObject roomContainer;
    private int currentIndex = 0;
    private void Awake()
    {
        RoomEvents.OnRoomCountDetermined += PreInstantiateRooms;
        roomContainer = new GameObject("Rooms");
    }
    private void OnDestroy()
    {
        RoomEvents.OnRoomCountDetermined -= PreInstantiateRooms;
    }

    private void PreInstantiateRooms(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Room newRoom = Instantiate(roomPrefab, roomContainer.transform).GetComponent<Room>();
            roomPool.Add(newRoom);
            Deactivate(newRoom);
        }
    }
    public Room SetRoomAt(Vector2Int pos, Vector3 WSpos, string name = "")
    {
        Room room = GetPooledRoom();

        room.transform.position = WSpos;
        Activate(room);
        room.RoomIndex = pos;
        room.name = $"{name}Room";
        return room;
    }

    private Room GetPooledRoom()
    {
        Room room = roomPool[currentIndex];
        currentIndex = (currentIndex + 1) % roomPool.Count; // Wrap index if it goes beyond the pool size
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

}
