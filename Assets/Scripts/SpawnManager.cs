using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    SpawnPosition startingPosition;

    [field: SerializeField]
    public int RoomsAmount { get; set; } = 30;

    [Range(0, 1)]
    [SerializeField]
    private float probabilityOfSuccess = 0.65f;

    private readonly Queue<Vector2Int> roomsQ = new();
    private GridManager GridManager;
    private uint roomCount;
    private bool generationComplete;
    private Vector2Int firstRoom;

    private void Start()
    {
        GridManager = GameManager.Instance.GridManager;
    }

    private void Update()
    {
        if (roomsQ.Count > 0 && roomCount < RoomsAmount && !generationComplete)
        {
            Vector2Int pos = roomsQ.Dequeue();
            foreach (Vector2Int dir in DirectionUtility.DirectionToVector.Values)
                TryGenerateRoomAt(pos + dir);
        }
        else if (roomCount < RoomsAmount)
        {
            GenerateRooms();
        }
        else if (!generationComplete)
        {
            generationComplete = true;
            GridManager.OpenDoors();
            enabled = false;
        }
    }

    private void GenerateRooms()
    {
        GridManager.ClearRooms();
        roomsQ.Clear();
        roomCount = 0;
        generationComplete = false;

        firstRoom = GridManager.Grid.GetCoordinatesAt(startingPosition);
        StartGenerationAt(firstRoom);
    }

    private void TryGenerateRoomAt(Vector2Int pos)
    {
        if (!ShouldGenerateRoom(pos))
            return;

        StartGenerationAt(pos);
    }

    private void StartGenerationAt(Vector2Int pos)
    {
        roomsQ.Enqueue(pos);
        roomCount++;
        GridManager.RegisterRoomAt(pos);
    }


    //To Do - First room always have 4 neighbours
    private bool ShouldGenerateRoom(Vector2Int pos) =>
        roomCount < RoomsAmount
        && (pos == firstRoom || Random.value < probabilityOfSuccess) // First room is exception
        && GridManager.Grid.IsWithinGrid(pos)
        && GridManager.CountNeighbors(pos) < 2; //Limit the neighbours in order to craete more corridor-like shape
}
