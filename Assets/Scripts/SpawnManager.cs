using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    private GridManager GridManager;

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
            foreach (Vector2Int offset in DirectionUtility.OrientationOf.Values)
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
            GridManager.ConnectTiles();
            //DEBUGGING DIRECTIONS
            /*
            Debug.Log($"First Room : (25,25) Furthest Room : {GridManager.FurthestRoom.Pivot}");
            Debug.Log(DirectionUtility.GetDirection(new(25, 25), GridManager.FurthestRoom.Pivot));
            Debug.Log(DirectionUtility.GetOffset(new(25, 25), GridManager.FurthestRoom.Pivot));
            Debug.Log("Opposite");
            Debug.Log(DirectionUtility.GetDirection(new(25, 25), GridManager.FurthestRoom.Pivot).GetOpposite());
            */
            enabled = false;
        }
    }

    private void GenerateTiles()
    {
        GridManager.ClearTiles();
        tileQueue.Clear();
        tilesCount = 0;
        generationComplete = false;

        firstTile = GridManager.FirstTile;
        StartGenerationAt(firstTile);
    }

    private void TryGenerateTileAt(Vector2Int pos)
    {
        if (!ShouldGenerateTile(pos)) return;

        StartGenerationAt(pos);
    }

    private void StartGenerationAt(Vector2Int pos)
    {
        tileQueue.Enqueue(pos);
        tilesCount++;
        GridManager.RegisterTileAt(pos);
    }


    //To Do - First tile always have 4 neighbours (room)
    private bool ShouldGenerateTile(Vector2Int pos) =>
        tilesCount < TilesAmount
        && (pos == firstTile || Random.value < probabilityOfSuccess) // First tile is exception
        && GridManager.Grid.IsWithinGrid(pos)
        && GridManager.CountNeighbors(pos) < 2; //Limit the neighbours in order to craete more corridor-like shape
}
