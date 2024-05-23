using System.Collections.Generic;
using UnityEngine;

public class TileFactory : MonoBehaviour
{
    [SerializeField]
    private Tile tilePF;
    private readonly Queue<Tile> tileQueue = new();

    private int TilesCount => GameManager.Instance.SpawnManager.TilesAmount;
    public Tile CurrentTile
    {
        get
        {
            Tile tile = tileQueue.Cycle();
            Activate(tile);
            return tile;
        }
    }

    private void Start()
    {
        GameObject go = new("Tiles");
        tileQueue.PreparePooling(tilePF, TilesCount, go.transform);
    }


    public void Activate(Tile tile) => tile.gameObject.SetActive(true);
    public void Deactivate(Tile tile) => tile.gameObject.SetActive(false);
}
