using System.Collections.Generic;
using UnityEngine;

public class TileFactory : MonoBehaviour
{
    [SerializeField]
    private Tile tilePF;
    private readonly List<Tile> tilePool = new();
    private int currentIndex = 0;
    private Tile CurrentPooledTile => tilePool[currentIndex];

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
        Tile tile = CurrentPooledTile;
        Activate(tile);
        tile.transform.position = wsPos;
        tile.name = $"{name}Tile";
        IncrementIndex();
        return tile;
    }

    private void IncrementIndex() => currentIndex = (currentIndex + 1) % tilePool.Count; // Wrap index if it goes beyond the pool size
    public static void Activate(Tile tile) => tile.gameObject.SetActive(true);
    public static void Deactivate(Tile tile) => tile.gameObject.SetActive(false);
}
