using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    private GridManager gridManager;

    [field: SerializeField]
    public int TilesAmount { get; set; } = 30;

    [Range(0, 1)]
    [SerializeField]
    private float probabilityOfSuccess = 0.65f;

    private readonly Queue<Float2> tileQueue = new();

    private uint tilesCount;
    private bool generationComplete;
    private Float2 firstTile;

    private void Start()
    {
        gridManager = GameManager.Instance.GridManager;
    }

    private void Update()
    {
        if (tileQueue.Count > 0 && tilesCount < TilesAmount && !generationComplete)
        {
            Float2 pos = tileQueue.Dequeue();
            foreach (Float2 offset in DirectionUtility.OrientationOf.Values)
            {
                TryGenerateTileAt(pos + offset);
            }
        }
        else if (tilesCount < TilesAmount)
        {
            GenerateTiles();
        }
        else if (!generationComplete)
        {
            generationComplete = true;
            gridManager.Execute();
            gridManager.DebugRoomStatus();
            enabled = false;
        }
    }

    private void GenerateTiles()
    {
        gridManager.ClearTiles();
        tileQueue.Clear();
        tilesCount = 0;
        generationComplete = false;

        firstTile = gridManager.FirstTile;
        StartGenerationAt(firstTile);
    }

    private void TryGenerateTileAt(Float2 pos)
    {
        if (!ShouldGenerateTile(pos)) return;

        StartGenerationAt(pos);
    }

    private void StartGenerationAt(Float2 pos)
    {
        tileQueue.Enqueue(pos);
        tilesCount++;
        gridManager.RegisterTileAt(pos);
    }


    //To Do - First tile always have 4 neighbours (room)
    private bool ShouldGenerateTile(Float2 pos) =>
        tilesCount < TilesAmount
        && (pos == firstTile || Random.value < probabilityOfSuccess) // First tile is exception
        && gridManager.Grid.IsWithinGrid(pos)
        && gridManager.CountNeighbors(pos) < 2; //Limit the neighbours in order to craete more corridor-like shape
}
