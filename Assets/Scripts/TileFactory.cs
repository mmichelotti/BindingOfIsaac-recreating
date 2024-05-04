using System.Collections.Generic;
using UnityEngine;

public class TileFactory : MonoBehaviour
{
    [SerializeField]
    private Tile tilePF;
    private readonly List<Tile> tilePool = new();
    private int currentIndex = 0;

    public void PrepareTilesPooling()
    {

        GameObject parent = new("Tiles");
        int tilesCount = GameManager.Instance.SpawnManager.TilesAmount;
        for (int i = 0; i < tilesCount; i++)
        {
            Tile newTile = Instantiate(tilePF, parent.transform);
            tilePool.Add(newTile);
            Deactivate(newTile);
        }
    }

    public Tile ActivateTile(Vector3 wsPos, string name = "")
    {
        Tile tile = GetPooledTile();
        Activate(tile);
        tile.transform.position = wsPos;
        tile.name = $"{name}Tile";
        return tile;
    }

    private Tile GetPooledTile()
    {
        Tile tile = tilePool[currentIndex];
        currentIndex = (currentIndex + 1) % tilePool.Count; // Wrap index if it goes beyond the pool size
        return tile;
    }

    public void Activate(Tile tile) => tile.gameObject.SetActive(true);

    public void Deactivate(Tile tile) => tile.gameObject.SetActive(false);
}
