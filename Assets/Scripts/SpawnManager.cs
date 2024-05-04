using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    private GridManager GridManager;

    [SerializeField]
    SpawnPosition startingPosition;

    [field: SerializeField]
    public int TilesAmount { get; set; } = 30;

    [Range(0, 1)]
    [SerializeField]
    private float probabilityOfSuccess = 0.65f;

    private readonly Queue<Vector2Int> tileQueue = new();

    private uint tilesCount;
    private bool generationComplete;
    private Vector2Int firstTile;

    private void Start()
    {
        GridManager = GameManager.Instance.GridManager;
    }

    private void Update()
    {
        if (tileQueue.Count > 0 && tilesCount < TilesAmount && !generationComplete)
        {
            Vector2Int pos = tileQueue.Dequeue();
            foreach (Vector2Int dir in DirectionUtility.DirectionToVector.Values)
            {
                TryGenerateRoomAt(pos + dir);
            }
        }
        else if (tilesCount < TilesAmount)
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
        tileQueue.Clear();
        tilesCount = 0;
        generationComplete = false;

        firstTile = GridManager.Grid.GetCoordinatesAt(startingPosition);
        StartGenerationAt(firstTile);
    }

    private void TryGenerateRoomAt(Vector2Int pos)
    {
        if (!ShouldGenerateRoom(pos)) return;

        StartGenerationAt(pos);
    }

    private void StartGenerationAt(Vector2Int pos)
    {
        tileQueue.Enqueue(pos);
        tilesCount++;
        GridManager.RegisterRoomAt(pos);
    }


    //To Do - First room always have 4 neighbours
    private bool ShouldGenerateRoom(Vector2Int pos) =>
        tilesCount < TilesAmount
        && (pos == firstTile || Random.value < probabilityOfSuccess) // First room is exception
        && GridManager.Grid.IsWithinGrid(pos)
        && GridManager.CountNeighbors(pos) < 2; //Limit the neighbours in order to craete more corridor-like shape
}
