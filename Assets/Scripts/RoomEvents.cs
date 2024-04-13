public static class RoomEvents
{
    public delegate void RoomCountDelegate(int count);
    public static event RoomCountDelegate OnRoomCountDetermined;
    public static void RaiseRoomCountDetermined(int count)
    {
        OnRoomCountDetermined?.Invoke(count);
    }
}

