using System.Collections.Generic;
using UnityEngine;

public class PoolUtility
{

}
public class TileFactory : MonoBehaviour
{
    [SerializeField]
    private Tile tilePF;
    private readonly Queue<Tile> tileQueue = new();

    public void PrepareTilesPooling()
    {

        GameObject parent = new("Tiles");
        int tilesCount = GameManager.Instance.SpawnManager.TilesAmount;
        for (int i = 0; i < tilesCount; i++)
        {
            Tile newTile = Instantiate(tilePF, parent.transform);
            tileQueue.Enqueue(newTile);
            Deactivate(newTile);
        }
    }

    public Tile ActivateTile(Vector3 wsPos, string name = "")
    {
        Tile tile = tileQueue.Dequeue();
        Activate(tile);
        tile.transform.position = wsPos;
        tile.name = $"{name}Tile";
        tileQueue.Enqueue(tile);
        return tile;
    }

    public void Activate(Tile tile) => tile.gameObject.SetActive(true);
    public void Deactivate(Tile tile) => tile.gameObject.SetActive(false);
}
