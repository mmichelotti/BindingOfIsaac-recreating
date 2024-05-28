using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    #region variables
    [field: SerializeField] public int TilesAmount { get; set; } = 30;
    [SerializeField, Range(0, 1)] private float probabilityOfSuccess = 0.65f;

    private readonly Queue<Vector2> tileQueue = new();
    private uint tilesCount;
    private bool generationComplete;
    private Vector2 firstTile;
    #endregion
    private GridManager GridManager => GameManager.Instance.GridManager;

    private void Update()
    {
        if (tileQueue.Count > 0 && tilesCount < TilesAmount && !generationComplete)
        {
            Vector2 pos = tileQueue.Dequeue();
            foreach (var offset in DirectionUtility.OrientationOf.Values) TryGenerateTileAt(pos + offset);
        }
        else if (tilesCount < TilesAmount) GenerateTiles();
        else if (!generationComplete)
        {
            generationComplete = true;
            GridManager.Execute();
            GridManager.DebugRoomStatus();
            enabled = false;
        }
    }

    #region methods
    private void GenerateTiles()
    {
        tilesCount = 0;
        GridManager.ClearTiles();
        tileQueue.Clear();
        generationComplete = false;

        firstTile = GridManager.FirstTile;
        StartGenerationAt(firstTile);
    }

    private void TryGenerateTileAt(Vector2 pos)
    {
        if (!ShouldGenerateTile(pos)) return;

        StartGenerationAt(pos);
    }

    private void StartGenerationAt(Vector2 pos)
    {
        tileQueue.Enqueue(pos);
        tilesCount++;
        GridManager.RegisterTileAt(pos);
    }

    //To Do - First tile always have 4 neighbours (room)
    private bool ShouldGenerateTile(Vector2 pos) =>
        tilesCount < TilesAmount
        && (pos == firstTile || Random.value < probabilityOfSuccess) // First tile is exception
        && GridManager.Grid.IsWithinGrid(pos)
        && GridManager.CountNeighbors(pos) < 2; //Limit the neighbours in order to craete more corridor-like shape
    #endregion
}
